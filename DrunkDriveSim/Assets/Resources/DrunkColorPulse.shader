Shader "Hidden/SynapticResponse/DrunkColorPulse"
{
  HLSLINCLUDE
      #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
      TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
      TEXTURE2D_SAMPLER2D(_PrevFrameTex, sampler_PrevFrameTex);
            
      float4    _OverlayColor;
      int       _PulseEnabled;
      float     _PulseSpeed;
      float     _OverLayMaxIntensity;
      float     _TrailBlurStrength;

      float4 Frag(VaryingsDefault i) : SV_Target
      {
          float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);  
          if (_TrailBlurStrength > 0.0f) {            
            float4 prevColor = SAMPLE_TEXTURE2D(_PrevFrameTex, sampler_PrevFrameTex, i.texcoord);
            color = prevColor * _TrailBlurStrength + color * (1 - _TrailBlurStrength); 
          }
          
          if (_OverLayMaxIntensity > 0.0f) {
            float level = _OverLayMaxIntensity;
            if (_PulseEnabled) level = (sin(_Time.y*_PulseSpeed)*0.5+0.5)*_OverLayMaxIntensity;
            color.rgb = lerp(color.rgb, _OverlayColor.rgb, level);
          }

          return color;
      }
  ENDHLSL
  SubShader
  {
      Cull Off ZWrite Off ZTest Always
      Pass
      {
          HLSLPROGRAM
              #pragma vertex VertDefault
              #pragma fragment Frag
          ENDHLSL
      }
  }
}
