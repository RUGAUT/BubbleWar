Shader "Custom/GrainEffect"
{
    Properties
    {
        _MainTex ("Texture principale", 2D) = "white" {} // Texture principale
        _GrainTex ("Texture de grain", 2D) = "white" {}  // Texture de grain
        _Intensity ("Intensité", Range(0, 1)) = 0.5      // Contrôle de l'intensité
        _Response ("Réponse lumineuse", Range(0, 1)) = 0.7 // Contrôle de la réponse lumineuse
        _Scale ("Échelle", Vector) = (1, 1, 0, 0)        // Échelle de la texture de grain
        _Offset ("Décalage", Vector) = (0, 0, 0, 0)      // Décalage de la texture de grain
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            sampler2D _GrainTex;
            float _Intensity;
            float _Response;
            float4 _Scale;
            float4 _Offset;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Couleur d'entrée
                fixed4 input = tex2D(_MainTex, i.uv);

                // Échantillonner le grain
                half grain = tex2D(_GrainTex, i.uv * _Scale.xy + _Offset.xy).w;
                grain = (grain - 0.5) * 2.0;

                // Calcul de luminance
                float lum = 1.0 - sqrt(dot(input.rgb, float3(0.2126, 0.7152, 0.0722)));
                lum = lerp(1.0, lum, _Response);

                // Application du grain
                input.rgb += input.rgb * grain * _Intensity * lum;

                // Maintenir la transparence de la texture principale
                input.a = tex2D(_MainTex, i.uv).a;

                return input;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}



