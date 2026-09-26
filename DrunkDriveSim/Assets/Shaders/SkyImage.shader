Shader "DrunkDrive/Sky Image"
{
    Properties
    {
        _MainTex ("Sky image", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        Cull Off ZWrite Off
        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            struct Varyings
            {
                float4 position : SV_POSITION;
                float4 screenPosition : TEXCOORD0;
            };

            Varyings Vert(float4 position : POSITION)
            {
                Varyings output;
                output.position = UnityObjectToClipPos(position);
                output.screenPosition = ComputeScreenPos(output.position);
                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                // A flat image fills the view; scene geometry still occludes it.
                float2 uv = input.screenPosition.xy / input.screenPosition.w;
                float2 border = abs(_MainTex_TexelSize.xy) * 0.5;
                return half4(tex2D(_MainTex, clamp(uv, border, 1.0 - border)).rgb, 1);
            }
            ENDHLSL
        }
    }
    Fallback Off
}
