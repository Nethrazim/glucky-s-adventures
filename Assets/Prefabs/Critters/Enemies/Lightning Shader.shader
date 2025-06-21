Shader "Unlit/Lightning Shader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Float) = 5.0
        _Speed ("Flash Speed", Float) = 10.0
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;

            float4 _MainColor;
            float _GlowIntensity;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.x * _Speed;
                float2 timeOffset = float2(time * 0.1, time * 0.2);

                // Sample lightning shape
                float shape = tex2D(_NoiseTex, uv).r;

                // Sample animated noise
                //float noise = tex2D(_NoiseTex, uv + timeOffset).r;

                // Combine flicker with lightning shape
                float flash = pow(saturate(sin(time * 10.0)), 8.0);
                float intensity = shape * _GlowIntensity;
                fixed4 col = _MainColor * intensity;

                return col;
            }
            ENDCG
        }
    }
}
