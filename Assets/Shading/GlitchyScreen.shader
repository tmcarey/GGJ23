Shader "Custom/GlitchyScreen" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 10.0
        _Distortion ("Distortion", Float) = 0.1
        _Tiling ("Tiling", Vector) = (1,1,0,0)
        _Offset ("Offset", Vector) = (0,0,0,0)
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Speed;
            float _Distortion;
            float2 _Tiling;
            float2 _Offset;

            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * float2(_Tiling.x, _Tiling.y) + float2(_Offset.x, _Offset.y);
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                uv.y += sin(uv.x * 10.0 * random(uv) + (_Time.y * _Speed)) * _Distortion;
                uv.x += sin(uv.y * 10.0 + (_Time.y * _Speed)) * _Distortion;
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}