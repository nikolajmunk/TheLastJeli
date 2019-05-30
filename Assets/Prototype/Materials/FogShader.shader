// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Teresina/Fog"
{
	Properties
	{
		_MaxIntensity("MaxIntensity", Range( 0 , 1)) = 0.6588235
		_Color("Color", Color) = (0.2857901,0,0.3490566,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_FogColor("FogColor", Color) = (0.275814,0.2217871,0.5283019,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 5.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard alpha:fade keepalpha noshadow exclude_path:deferred novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _FogColor;
		uniform float4 _Color;
		uniform sampler2D _CameraDepthTexture;
		uniform float _MaxIntensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner14 = ( 0.08 * _Time.y * float2( 1,0 ) + i.uv_texcoord);
			o.Albedo = ( tex2D( _TextureSample0, panner14 ) * _FogColor ).rgb;
			o.Emission = _Color.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float eyeDepth3 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float clampResult7 = clamp( ( abs( ( eyeDepth3 - ase_screenPos.w ) ) * 0.5 ) , 0.0 , _MaxIntensity );
			o.Alpha = clampResult7;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
216;73;1425;418;2188.613;854.0778;2.889618;True;False
Node;AmplifyShaderEditor.ScreenPosInputsNode;2;-1160.549,116.6848;Float;True;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;3;-768.8066,76.47312;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-489.7092,183.5446;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-670.1818,-335.8673;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;14;-384.4291,-320.1064;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;0.08;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;6;-281.9094,178.7809;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-291.7506,269.6771;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;-14.94145,-90.49378;Float;False;Property;_FogColor;FogColor;4;0;Create;True;0;0;False;0;0.275814,0.2217871,0.5283019,0;1,0.4669811,0.4669811,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-56.80948,115.5843;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-30.79567,301.4422;Float;False;Property;_MaxIntensity;MaxIntensity;1;0;Create;True;0;0;False;0;0.6588235;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-157.7466,-321.9906;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;e28dc97a9541e3642a48c0e3886688c5;e28dc97a9541e3642a48c0e3886688c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;20;-813.8995,531.239;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;21;-624.8784,558.9322;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-590.0841,441.6937;Float;False;InstancedProperty;_Intensity;Intensity;0;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;18;-1118.54,430.1019;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;19;-1116.523,614.8384;Float;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;22;-245.0358,522.3373;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;180.9665,394.8402;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;359.5981,-57.179;Float;False;Property;_Color;Color;2;0;Create;True;0;0;False;0;0.2857901,0,0.3490566,0;0.2786579,0.2027857,0.2924528,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;368.3858,-271.6726;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;7;471.4159,157.8273;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;664.765,-93.55676;Float;False;True;7;Float;ASEMaterialInspector;0;0;Standard;Teresina/Fog;False;False;False;False;False;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;True;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;4;0;3;0
WireConnection;4;1;2;4
WireConnection;14;0;15;0
WireConnection;6;0;4;0
WireConnection;5;0;6;0
WireConnection;5;1;12;0
WireConnection;13;1;14;0
WireConnection;20;0;18;0
WireConnection;20;1;19;0
WireConnection;21;0;20;0
WireConnection;22;1;1;0
WireConnection;22;2;21;0
WireConnection;23;1;22;0
WireConnection;16;0;13;0
WireConnection;16;1;17;0
WireConnection;7;0;5;0
WireConnection;7;2;8;0
WireConnection;0;0;16;0
WireConnection;0;2;11;0
WireConnection;0;9;7;0
ASEEND*/
//CHKSM=A058D507277307E1C980B0723E34485EF7A3713D