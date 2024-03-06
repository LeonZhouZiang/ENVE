Shader "Unlit/Outline"
{
    Properties
    {
    
        _Thickness ("Thickness", Range(0.0005, 0.1)) = 0.01
        _Color("Color", Color) = (1, 1, 1, 1)

    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 100

        Pass
        {

            Cull Off
            Stencil
            {
                Ref 1 
                Comp NotEqual 
                ZFail keep
                Fail keep 
            }

            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS   : POSITION;             
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;    
                float _Thickness;
            CBUFFER_END  
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 extrude = IN.normal * _Thickness;
                IN.positionOS += float4(extrude.x, extrude.y, extrude.z, 0);

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return _Color;
            }
            ENDHLSL
        }
    }
}
