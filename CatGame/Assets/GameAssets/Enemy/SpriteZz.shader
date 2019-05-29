Shader "Legacy Shaders/Transparent/Cutout/Custom Bumped Diffuse" {
 Properties {
     _Color ("Main Color", Color) = (1,1,1,0)
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _BumpMap ("Normalmap", 2D) = "bump" {}
     _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="TransparentCutout" "PreviewType" = 
"Plane" "CanUseSpriteAtlas" = "True"}
     LOD 300
     ZWrite On
     Cull Off
     Lighting On
 
 CGPROGRAM
 #pragma surface surf Lambert addshadow alphatest:_Cutoff
 sampler2D _MainTex;
 sampler2D _BumpMap;
 fixed4 _Color;

 struct Input
 {
     float2 uv_MainTex;
     float2 uv_BumpMap;
     float3 viewDir;
 };

 void surf (Input IN, inout SurfaceOutput o)
 {
     fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
     o.Albedo = c.rgb;
     o.Alpha = c.a;
     o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
     float3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
      o.Normal = dot(IN.viewDir, float3(0, 0, 1)) > 0 ? n : -n;
 }
 ENDCG
 }

 FallBack "Legacy Shaders/Transparent/Cutout/Diffuse"
 }