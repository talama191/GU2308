Shader "Custom/ReverseMask" {
    Properties {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _MaskTex ("Mask Texture", 2D) = "white" { }
    }

    SubShader {
        Tags { "Queue" = "Overlay" }
        LOD 100

        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
        }
    }

    SubShader {
        Tags { "Queue" = "Overlay" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Stencil {
            Ref 1
            Comp always
            Pass replace
        }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _MaskTex;

        struct Input {
            float2 uv_MainTex;
        };

        void vert (inout appdata_full v) {
            v.vertex.y = 1;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * c.a;
            o.Alpha = 1 - tex2D(_MaskTex, IN.uv_MainTex).r; // Reverse the visibility based on the mask
        }
        ENDCG
    }
}
