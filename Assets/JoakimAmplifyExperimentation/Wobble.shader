// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JoakimShaderCollection/Wobble"
{
	Properties
	{
		_Float0("Float 0", Float) = 1
		_BlendToWhite("BlendToWhite", Range( 0 , 1)) = 0
		_AlbedoTexture("AlbedoTexture", 2D) = "white" {}
		_ColourScale("ColourScale", Float) = 1
		_DistortionMap("DistortionMap", 2D) = "bump" {}
		_Smoothness("Smoothness", Float) = 1
		_DistortionScale("DistortionScale", Range( 0 , 1)) = 0
		_ExtrusionPoint("ExtrusionPoint", Float) = 0.25
		_ExtrusionAmount("ExtrusionAmount", Float) = 20
		_WobbleSpeed("WobbleSpeed", Range( 0 , 1)) = 0.2
		_MinAlpha("MinAlpha", Float) = 0.5
		_MaxAlpha("MaxAlpha", Float) = 0.7
		_MetallicTex("MetallicTex", 2D) = "white" {}
		_AOTexture("AOTexture", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float _WobbleSpeed;
		uniform float _ExtrusionPoint;
		uniform float _ExtrusionAmount;
		uniform sampler2D _AlbedoTexture;
		uniform float4 _AlbedoTexture_ST;
		uniform float _ColourScale;
		uniform sampler2D _GrabTexture;
		uniform sampler2D _DistortionMap;
		uniform float4 _DistortionMap_ST;
		uniform float _DistortionScale;
		uniform float _Float0;
		uniform float _BlendToWhite;
		uniform sampler2D _MetallicTex;
		uniform float4 _MetallicTex_ST;
		uniform float _Smoothness;
		uniform sampler2D _AOTexture;
		uniform float4 _AOTexture_ST;
		uniform float _MaxAlpha;
		uniform float _MinAlpha;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz += ( ase_vertexNormal * max( ( sin( ( ( ase_vertex3Pos + ( _Time.y * _WobbleSpeed ) ) / _ExtrusionPoint ) ) / _ExtrusionAmount ) , float3( 0,0,0 ) ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_AlbedoTexture = i.uv_texcoord * _AlbedoTexture_ST.xy + _AlbedoTexture_ST.zw;
			o.Albedo = ( tex2D( _AlbedoTexture, uv_AlbedoTexture ) * _ColourScale ).rgb;
			float2 uv_DistortionMap = i.uv_texcoord * _DistortionMap_ST.xy + _DistortionMap_ST.zw;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor120 = tex2D( _GrabTexture, ( float4( ( UnpackNormal( tex2D( _DistortionMap, uv_DistortionMap ) ) * _DistortionScale ) , 0.0 ) + ase_grabScreenPosNorm ).xy );
			float4 temp_cast_3 = (_Float0).xxxx;
			float4 lerpResult123 = lerp( screenColor120 , temp_cast_3 , _BlendToWhite);
			o.Emission = lerpResult123.rgb;
			float2 uv_MetallicTex = i.uv_texcoord * _MetallicTex_ST.xy + _MetallicTex_ST.zw;
			o.Metallic = tex2D( _MetallicTex, uv_MetallicTex ).r;
			o.Smoothness = _Smoothness;
			float2 uv_AOTexture = i.uv_texcoord * _AOTexture_ST.xy + _AOTexture_ST.zw;
			o.Occlusion = tex2D( _AOTexture, uv_AOTexture ).r;
			float lerpResult48 = lerp( _MaxAlpha , _MinAlpha , (_SinTime.w*0.3 + 0.3));
			o.Alpha = lerpResult48;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
98;668;1397;962;208.4384;1016.631;1;True;False
Node;AmplifyShaderEditor.TimeNode;64;-1189.858,729.4724;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;65;-1229.09,908.9932;Float;False;Property;_WobbleSpeed;WobbleSpeed;13;0;Create;True;0;0;False;0;0.2;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;12;-955.1736,464.6086;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-940.5585,744.1241;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-581.3204,744.4107;Float;False;Property;_ExtrusionPoint;ExtrusionPoint;8;0;Create;True;0;0;False;0;0.25;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-566.4521,497.0351;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;124;-479.0424,-1340.301;Float;False;1420.031;630.6984;Faux Refraction;9;123;121;120;122;119;118;116;115;117;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;115;-369.7686,-1274.293;Float;True;Property;_DistortionMap;DistortionMap;4;0;Create;True;0;0;False;0;dd2fd2df93418444c8e280f1d34deeb5;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;116;-361.7632,-1047.029;Float;False;Property;_DistortionScale;DistortionScale;6;0;Create;True;0;0;False;0;0;0.009;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-325.7119,619.1431;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-132.2213,843.0555;Float;False;Property;_ExtrusionAmount;ExtrusionAmount;9;0;Create;True;0;0;False;0;20;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;117;-107.0418,-920.4785;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-8.94528,-1089.302;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SinOpNode;8;-143.9212,591.5381;Float;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-513.1348,356.0553;Float;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;False;0;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;23;-588.1594,181.3666;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;119;153.237,-971.0719;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;5;153.4754,750.4563;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;94;-335.4164,119.5272;Float;False;Property;_MinAlpha;MinAlpha;17;0;Create;True;0;0;False;0;0.5;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;461.4229,-278.7152;Float;False;Property;_ColourScale;ColourScale;3;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-341.1978,19.31093;Float;False;Property;_MaxAlpha;MaxAlpha;18;0;Create;True;0;0;False;0;0.7;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;108;334.831,-618.1435;Float;True;Property;_AlbedoTexture;AlbedoTexture;2;0;Create;True;0;0;False;0;None;12ab00dfff9f9f648a0c20f59223af6b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;2;198.4157,561.9323;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMaxOpNode;4;374.5703,753.2361;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;51;-252.9793,315.2936;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;122;410.7513,-833.3702;Float;False;Property;_BlendToWhite;BlendToWhite;1;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;549.4713,-932.3101;Float;False;Property;_Float0;Float 0;0;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;120;327.295,-1011.155;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;129;-291.6892,1256.489;Float;False;823.0333;349.1648;Might be usable for dissolve of environmment. Add to albedo to see effect;4;125;126;127;128;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;641.5826,-346.4554;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1598.74,1939.767;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;89;-2233.89,-653.7187;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;127;49.00415,1351.211;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;62;-2383.758,921.2408;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;80;-1630.787,-1190.307;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;59;-2874.752,646.7855;Float;False;Property;_PannerSpeed;PannerSpeed;10;0;Create;True;0;0;False;0;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1369.956,1749.684;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1143.894,2097.718;Float;False;Property;_AnimScale;AnimScale;16;0;Create;True;0;0;False;0;0.1;0;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;109;158.8201,-234.8706;Float;True;Property;_MetallicTex;MetallicTex;19;0;Create;True;0;0;False;0;None;4ed213f774ea5c24d8cd30319ef7bddb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-2629.9,841.7506;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;60;-2390.679,636.5266;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-2890.689,448.8988;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;90;-2194.077,-435.7157;Float;False;Property;_Float5;Float 5;15;0;Create;True;0;0;False;0;0.4;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;125;-241.6892,1306.489;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;75;-2286.827,266.6452;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;114;-1897.186,1139.664;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenColorNode;128;337.3441,1341.796;Float;False;Global;_GrabScreen1;Grab Screen 1;3;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;81;-1721.126,-1479.095;Float;False;Constant;_Color2;Color 2;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;84;-1965.967,-1324.234;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;74;-2377.167,-22.14325;Float;False;Constant;_Color3;Color 3;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;111;219.7355,-24.5816;Float;True;Property;_AOTexture;AOTexture;20;0;Create;True;0;0;False;0;None;12cb9d41641827847bc38972d9957790;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;88;-2234.649,-1008.053;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;76;-1764.356,603.3207;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TimeNode;27;-1848.04,1925.115;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;31;-862.9958,1725.016;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;79;-1388.252,-826.4387;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;72;-2622.008,132.7182;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;112;-1607.197,1042.383;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;123;751.4312,-1011.87;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-2850.116,1021.237;Float;False;Property;_TimeMultiplier;TimeMultiplier;14;0;Create;True;0;0;False;0;0.4;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-913.7758,1893.743;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;113;-2115.782,1104.979;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;85;-1734.639,-820.4263;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;78;-1043.63,-1034.445;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;48;125.5868,232.0688;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;553.2689,-169.5312;Float;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;61;-2061.1,699.8474;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1887.271,2104.634;Float;False;Property;_AnimSpeed;AnimSpeed;12;0;Create;True;0;0;False;0;0.1;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;32;-664.8527,1904.517;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;55;-2889.93,803.2344;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;126;-97.97913,1491.154;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;82;-1631.072,-577.7316;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-2546.983,307.4069;Float;False;Constant;_Float3;Float 3;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;86;-2218.713,-810.1674;Float;False;Property;_Vector0;Vector 0;11;0;Create;True;0;0;False;0;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;83;-1890.943,-1149.546;Float;False;Constant;_Float4;Float 4;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;25;-1836.345,1740.369;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;528.6909,573.7028;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-1973.86,-615.2017;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;951.1589,-341.0562;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;JoakimShaderCollection/Wobble;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;7;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;63;0;64;2
WireConnection;63;1;65;0
WireConnection;11;0;12;0
WireConnection;11;1;63;0
WireConnection;9;0;11;0
WireConnection;9;1;10;0
WireConnection;118;0;115;0
WireConnection;118;1;116;0
WireConnection;8;0;9;0
WireConnection;119;0;118;0
WireConnection;119;1;117;0
WireConnection;5;0;8;0
WireConnection;5;1;6;0
WireConnection;4;0;5;0
WireConnection;51;0;23;4
WireConnection;51;1;52;0
WireConnection;51;2;52;0
WireConnection;120;0;119;0
WireConnection;103;0;108;0
WireConnection;103;1;102;0
WireConnection;26;0;27;1
WireConnection;26;1;28;0
WireConnection;127;0;125;0
WireConnection;127;1;126;0
WireConnection;62;0;60;0
WireConnection;80;0;84;4
WireConnection;80;1;83;0
WireConnection;80;2;83;0
WireConnection;29;0;25;0
WireConnection;29;1;26;0
WireConnection;58;0;55;2
WireConnection;58;1;56;0
WireConnection;60;0;57;0
WireConnection;60;2;59;0
WireConnection;60;1;58;0
WireConnection;75;0;72;4
WireConnection;75;1;71;0
WireConnection;75;2;71;0
WireConnection;114;0;113;0
WireConnection;128;0;127;0
WireConnection;76;0;61;0
WireConnection;76;1;74;0
WireConnection;76;2;75;0
WireConnection;31;0;29;0
WireConnection;79;0;82;0
WireConnection;112;0;114;0
WireConnection;123;0;120;0
WireConnection;123;1;121;0
WireConnection;123;2;122;0
WireConnection;35;0;30;0
WireConnection;113;0;62;0
WireConnection;85;0;88;0
WireConnection;85;2;86;0
WireConnection;85;1;87;0
WireConnection;78;0;79;0
WireConnection;78;1;81;0
WireConnection;78;2;80;0
WireConnection;48;0;93;0
WireConnection;48;1;94;0
WireConnection;48;2;51;0
WireConnection;61;0;62;0
WireConnection;32;0;31;0
WireConnection;32;3;35;0
WireConnection;32;4;30;0
WireConnection;82;0;85;0
WireConnection;3;0;2;0
WireConnection;3;1;4;0
WireConnection;87;0;89;2
WireConnection;87;1;90;0
WireConnection;0;0;103;0
WireConnection;0;2;123;0
WireConnection;0;3;109;0
WireConnection;0;4;1;0
WireConnection;0;5;111;0
WireConnection;0;9;48;0
WireConnection;0;11;3;0
ASEEND*/
//CHKSM=898F916513E1CB8E3A117133C267503D7704ACF8