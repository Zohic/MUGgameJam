Shader "Unlit/ScrollShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _NormalMap("TransparencyMask", 2D)="white" {}
        _AddTex("AdditionalTexture", 2D) = "white" {}
        _OffsetFloatt("TextureOffsetCoof", Float) = 0.5
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull back
        LOD 100

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert alpha
            #pragma fragment frag alpha

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float _OffsetFloatt;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NormalMap;
            float4 _NormalMap_ST;

            sampler2D _AddTex;
            float4 _AddTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
               
                float2 transUV = TRANSFORM_TEX(i.uv, _NormalMap);

                if (transUV.x > 0)
                    transUV = transUV % 1;
                else
                    transUV = float2(floor(abs(transUV.x)+1), 0) + transUV;

                float2 addUV = TRANSFORM_TEX(float2(i.uv.x * _OffsetFloatt, i.uv.y), _NormalMap);

                if (addUV.x > 0)
                    addUV = addUV % 1;
                else
                    addUV = float2(floor(abs(addUV.x) + 1), 0) + addUV;

                col *= tex2D(_AddTex, addUV);
                //if (transUV.x > 0)
                //    transUV = transUV;
               // else
               //     return float4(1,1,0,1);

                col.a = tex2D(_NormalMap, transUV).a;
                return col;
            }
            ENDCG
        }
    }
}
