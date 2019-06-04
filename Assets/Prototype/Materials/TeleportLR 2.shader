// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TeleportLR"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_MaskTiling("MaskTiling", Vector) = (2,2,0,0)
		_Multiplyfactor("Multiply factor", Float) = 1.31
		_MainCol("MainCol", Color) = (0.6521201,1,0.5411765,1)
		_Float1("Float 1", Range( 2 , 10)) = 1
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_GlowCol("GlowCol", Color) = (1,0.9282377,0.8160377,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample1;
		uniform float2 _MaskTiling;
		uniform float _Float1;
		uniform float4 _GlowCol;
		uniform float4 _MainCol;
		uniform sampler2D _TextureSample0;
		uniform float _Multiplyfactor;
		uniform sampler2D _Mask;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime43 = _Time.y * _Float1;
			float2 panner44 = ( mulTime43 * float2( -1,0 ) + float2( 0,0 ));
			float2 uv_TexCoord46 = i.uv_texcoord * _MaskTiling + panner44;
			float2 uv_TexCoord1 = i.uv_texcoord * float2( 1,1 );
			float2 panner4 = ( _Time.y * float2( 2,0 ) + uv_TexCoord1);
			float2 uv_TexCoord66 = i.uv_texcoord * float2( 0.95,0.95 );
			float4 temp_output_51_0 = saturate( ( ( ( tex2D( _TextureSample1, uv_TexCoord46 ) * _GlowCol ) + _MainCol ) * ( tex2D( _TextureSample0, panner4 ).a * _Multiplyfactor * tex2D( _Mask, uv_TexCoord66 ) ) ) );
			o.Emission = temp_output_51_0.rgb;
			o.Alpha = temp_output_51_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

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
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
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
239;73;1363;369;2294.154;437.8283;3.045082;True;False
Node;AmplifyShaderEditor.RangedFloatNode;42;-1686.354,-631.4406;Float;False;Property;_Float1;Float 1;6;0;Create;True;0;0;False;0;1;0.4;2;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;-1360.388,-624.9718;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;44;-1172.767,-696.0593;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;45;-1243.556,-844.8904;Float;False;Property;_MaskTiling;MaskTiling;3;0;Create;True;0;0;False;0;2,2;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;3;-1560.054,-193.3046;Float;True;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-882.8615,-848.8134;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;11;-1456.618,77.51581;Float;False;Constant;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;2,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;12;-1367.873,204.7069;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1359.974,-173.0389;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;65;-873.5941,402.8686;Float;True;Constant;_Vector2;Vector 2;0;0;Create;True;0;0;False;0;0.95,0.95;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-637.1539,403.5557;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;-542.6153,-761.4229;Float;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;False;0;e35304ad568e1c44aa133b9924a36372;e35304ad568e1c44aa133b9924a36372;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;4;-1107.765,33.85844;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;49;-484.2969,-548.3222;Float;False;Property;_GlowCol;GlowCol;8;0;Create;True;0;0;False;0;1,0.9282377,0.8160377,0;1,0.4292452,0.4292452,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;57;-348.475,287.1181;Float;True;Property;_Mask;Mask;1;0;Create;True;0;0;False;0;c31e2dce422fffa42966ebdb141c15b7;32b6b176d6e56dd449302545e40ab0d6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;22;-59.8361,-149.8134;Float;False;Property;_MainCol;MainCol;5;0;Create;True;0;0;False;0;0.6521201,1,0.5411765,1;1,0.7550271,0.004716992,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-195.8961,-646.4934;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-307.1085,176.8746;Float;False;Property;_Multiplyfactor;Multiply factor;4;0;Create;True;0;0;False;0;1.31;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-862.0425,5.059975;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;c31e2dce422fffa42966ebdb141c15b7;39809054c169edb4f9f841013ade79ea;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;50.94725,91.23792;Float;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;210.7577,-395.8611;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;444.166,-183.4604;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1579.165,-1229.629;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;51;694.2802,-171.7567;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;18;-2074.066,-1474.625;Float;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;1,0.8648109,0.7019608,1;0.2891153,0.6886792,0.6886792,0.1058824;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;15;-2066.871,-1287.229;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1991.179,-1016.491;Float;False;Property;_Gradientopacity;Gradient opacity;2;0;Create;True;0;0;False;0;1.83;1.74;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;788.2131,154.487;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;998.9048,-256.1738;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;TeleportLR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;1;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;42;0
WireConnection;44;1;43;0
WireConnection;46;0;45;0
WireConnection;46;1;44;0
WireConnection;1;0;3;0
WireConnection;66;0;65;0
WireConnection;47;1;46;0
WireConnection;4;0;1;0
WireConnection;4;2;11;0
WireConnection;4;1;12;0
WireConnection;57;1;66;0
WireConnection;48;0;47;0
WireConnection;48;1;49;0
WireConnection;7;1;4;0
WireConnection;24;0;7;4
WireConnection;24;1;25;0
WireConnection;24;2;57;0
WireConnection;50;0;48;0
WireConnection;50;1;22;0
WireConnection;21;0;50;0
WireConnection;21;1;24;0
WireConnection;17;0;18;0
WireConnection;17;1;15;0
WireConnection;17;2;16;0
WireConnection;51;0;21;0
WireConnection;0;2;51;0
WireConnection;0;9;51;0
ASEEND*/
//CHKSM=BFAF9325223078BD7B16392ADCB102051CA39F60