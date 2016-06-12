Shader "Custom/Anenome Shader"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}

	_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}
	_CausticsStartLevel("Caustics Start Level", Float) = 0.0
		_CausticsShallowFadeDistance("Caustics Shallow Distance", Float) = 1.0
		_CausticsScale("Caustics Scale", Float) = 1.0
		_CausticsDrift("Caustics Drift", Vector) = (0.1, 0.0, -0.4)
		//_Health("Health", Float) = 1.0 

	}
		SubShader
	{
		Tags{ "DisableBatching" = "True" "LightMode" = "ForwardBase" }
		Pass
	{
		


		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog
#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"
#include "UnityStandardCausticsCore.cginc"


		//uniform fixed3 unity_FogColor;
		uniform half unity_FogDensity;

	struct v2f
	{
		float2 uv : TEXCOORD0;
		fixed4 diff : COLOR0;
		float4 vertex : SV_POSITION;
		UNITY_FOG_COORDS(1)
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		o.vertex = v.vertex;

		if (v.vertex.y >= 0.0f) {
			float y = v.vertex.y;
			
			float delta = 0.9f * sin(_Time.y*0.6f + (y*5.0f)) * (y * 2.0f);

			o.vertex.x += delta;
			o.vertex.z += delta;
			v.vertex.z -= delta;
		}
		o.vertex = mul(UNITY_MATRIX_MVP, o.vertex);


		o.uv = v.texcoord;
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
		o.diff = nl * _LightColor0;

		o.diff.rgb += ShadeSH9(half4(worldNormal,1));
		o.diff.rgb *= 3.0f;

		UNITY_TRANSFER_FOG(o, o.vertex);

		return o;
	}

	//sampler2D _MainTex;

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv);
	col *= i.diff;

	UNITY_APPLY_FOG(i.fogCoord, col);
	UNITY_OPAQUE_ALPHA(col.a);

	// Caustics projection for texels below water level - provided license free by dualheights
	float4 s = i.vertex;
	if (s.y < _CausticsStartLevel) {
		// Move the caustics in world space
		float3 drift = _CausticsDrift * _Time.y;
		// Fade out caustics for shallow water
		float fadeFactor = min(1.0f,
			(_CausticsStartLevel - s.y) /
			_CausticsShallowFadeDistance);
		// Remove caustics on half bottom of objects, i.e. no caustics "from below"
		float3 upVec = float3(0, 1, 0);
		float belowFactor = min(1.0f, max(0.0f, dot(s.w, upVec) + 0.5f));
		// Calculate the projected texture coordinate in the caustics texture
		float3 worldCoord = (s + drift) / _CausticsScale;
		float2 causticsTextureCoord = mul(worldCoord, _CausticsLightOrientation).xy;
		// Calculate caustics light emission
		float3 toAdd = Emission(causticsTextureCoord) * fadeFactor * belowFactor;
		col = float4(toAdd.r, toAdd.g, toAdd.b, 1);

		//col += float4(1.0, 1.0, 1.0, 1.0)*(1.0-_Health);
	}

	return col;
	}
		ENDCG
	}
	}
}