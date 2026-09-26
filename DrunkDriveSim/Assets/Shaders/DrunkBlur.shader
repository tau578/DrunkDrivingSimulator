Shader "DrunkDrive/Drunk Blur"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 1
        _BlurRadiusMin ("Minimum blur radius (pixels at 1080p)", Range(0, 6)) = 1
        _BlurRadius ("Maximum blur radius (pixels at 1080p)", Range(0, 6)) = 6
        _BlurCycleSeconds ("Blur cycle duration (seconds)", Range(0.5, 20)) = 6
        _MotionStrength ("Motion blur strength", Range(0, 1)) = 0.25
        _MaxMotionPixels ("Maximum motion trail (pixels at 1080p)", Range(0, 20)) = 8
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }
        Pass
        {
            Name "Drunk Blur"
            ZWrite Off ZTest Always Cull Off
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            TEXTURE2D_X(_MotionVectorTexture);
            CBUFFER_START(UnityPerMaterial)
                float _Intensity;
                float _BlurRadius;
                float _BlurRadiusMin;
                float _BlurCycleSeconds;
                float _MotionStrength;
                float _MaxMotionPixels;
            CBUFFER_END

            half4 ReadColor(float2 uv)
            {
                float2 border = 0.5 / _ScaledScreenParams.xy;
                return SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, clamp(uv, border, 1.0 - border));
            }

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = input.texcoord;
                half4 original = ReadColor(uv);
                if (_Intensity <= 0) return original;

                float resolutionScale = _ScaledScreenParams.y / 1080.0;
                // Smoothly breathe from minimum to maximum and back each cycle.
                float phase = _Time.y * 6.2831853 / max(_BlurCycleSeconds, 0.5);
                float blurRadius = lerp(_BlurRadiusMin, _BlurRadius, 0.5 - 0.5 * cos(phase));
                float2 radius = blurRadius * resolutionScale / _ScaledScreenParams.xy;
                float2 motion = -SAMPLE_TEXTURE2D_X(_MotionVectorTexture, sampler_LinearClamp, uv).xy;
                float2 motionPixels = motion * _ScaledScreenParams.xy * _MotionStrength;
                motionPixels *= min(1.0, _MaxMotionPixels * resolutionScale / max(length(motionPixels), 0.0001));
                motion = motionPixels / _ScaledScreenParams.xy;

                // A small Gaussian kernel at three points along the motion trail.
                // All samples come from camera color, including the camera-space HUD.
                half4 blurred = 0;
                [unroll] for (int t = 0; t < 3; t++)
                {
                    [unroll] for (int y = -1; y <= 1; y++)
                    {
                        [unroll] for (int x = -1; x <= 1; x++)
                        {
                            float weight = (x == 0 ? 2.0 : 1.0) * (y == 0 ? 2.0 : 1.0);
                            blurred += ReadColor(uv + float2(x, y) * radius + motion * (t * 0.5)) * weight;
                        }
                    }
                }
                return lerp(original, blurred / 48.0, _Intensity);
            }
            ENDHLSL
        }
    }
}
