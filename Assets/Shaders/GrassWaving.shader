Shader "Custom/GrassWaving_Inspired"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _WavingStrength ("X Waving Strength", Float) = 0.05
        _YScaleStrength ("Y Scale Strength", Float) = 0.05
        _WavingScale ("Wave Scale", Float) = 1.0
        _WavingSpeed ("Wave Speed", Float) = 2.0
        _BendPower ("Bend Curve Power", Float) = 2.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _WavingStrength;
            float _YScaleStrength;
            float _WavingScale;
            float _WavingSpeed;
            float _BendPower;

            // Optional: smoother pseudo-noise
            float smootherNoise(float2 p) {
                return sin(dot(p, float2(12.9898, 78.233))) * 0.5 + 0.5;
            }

            v2f vert(appdata_t v)
            {
                v2f o;
                float2 uv = v.uv;
                float heightFactor = pow(saturate(uv.y), _BendPower); // низ неподвижен, верх движется сильнее
                float wavePhase = (_Time.y * _WavingSpeed + v.vertex.x * _WavingScale);
                float wave = sin(wavePhase);

                float xOffset = wave * _WavingStrength * heightFactor;
                float yScale = 1.0 + wave * _YScaleStrength * heightFactor;
                float pivot = 0.0;

                v.vertex.x += xOffset;
                v.vertex.y = pivot + (v.vertex.y - pivot) * yScale;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
