// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JoakimShaderCollection/Dissolve"
{
	Properties
	{
		_NoiseScale("NoiseScale", Float) = 2.37
		_EmissionOffset("EmissionOffset", Float) = 0.1
		[HDR]_EmissionColour("EmissionColour", Color) = (1.639216,0,2,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NoiseScale;
		uniform float _EmissionOffset;
		uniform float4 _EmissionColour;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color10 = IsGammaSpace() ? float4(0.254717,0.254717,0.254717,0) : float4(0.05278496,0.05278496,0.05278496,0);
			float2 temp_cast_0 = (_NoiseScale).xx;
			float2 temp_cast_1 = (5.0).xx;
			float2 uv_TexCoord3 = i.uv_texcoord * temp_cast_0 + temp_cast_1;
			float simplePerlin2D1 = snoise( uv_TexCoord3 );
			float temp_output_5_0 = (simplePerlin2D1*0.5 + 0.5);
			float temp_output_28_0 = (0.0 + (_SinTime.w - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
			clip( temp_output_5_0 - temp_output_28_0);
			o.Albedo = color10.rgb;
			o.Emission = ( step( temp_output_5_0 , ( temp_output_28_0 + _EmissionOffset ) ) * _EmissionColour ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
753;75;925;936;2160.889;1066.605;2.660723;False;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-1522.709,77.00368;Float;False;Property;_NoiseScale;NoiseScale;1;0;Create;True;0;0;False;0;2.37;2.37;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1528.129,180.7776;Float;False;Constant;_Float2;Float 2;0;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1320.436,63.33098;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;32;-1051.903,769.5349;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;1;-1094.115,70.58798;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;28;-829.5012,727.029;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-872.9801,499.5887;Float;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-611.5701,824.0281;Float;False;Property;_EmissionOffset;EmissionOffset;3;0;Create;True;0;0;False;0;0.1;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;5;-632.8879,240.6732;Float;True;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-346.9083,532.9062;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;50;-3622.37,-1822.106;Float;False;2107.832;1224.905;Youtube Guy;11;38;49;44;36;43;47;46;45;39;37;42;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;35;-122.1865,759.167;Float;False;Property;_EmissionColour;EmissionColour;4;1;[HDR];Create;True;0;0;False;0;1.639216,0,2,0;0.1982176,0,1.498039,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;15;-95.39118,512.4177;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-464.8264,-171.1698;Float;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.254717,0.254717,0.254717,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;66;-3590.917,-184.8815;Float;False;1652.978;1068.544;Amplify Guy;12;60;52;54;64;56;51;57;61;59;55;58;53;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;52;-2547.343,-134.8815;Float;False;Constant;_Color4;Color 4;6;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;58;-3453.482,558.5894;Float;False;Property;_PlanePos;PlanePos;7;0;Create;True;0;0;False;0;0,0,0;0,-1.33,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;53;-3460.079,174.5915;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-3008.252,-1164.656;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;42;-2437.951,-976.5529;Float;True;FLOAT;2;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;21;-562.3171,56.88352;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;-1701.038,-1281.553;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-880.6027,-17.52078;Float;False;Property;_Dissolve;Dissolve;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;54;-3540.917,379.6424;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;20;-821.9497,87.90896;Float;False;Constant;_Float3;Float 3;1;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;38;-3572.37,-922.2014;Float;True;Property;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;-1,-1,-1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClipNode;9;-150.3069,40.98268;Float;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;49;-2780.911,-974.1906;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;64;-2124.439,138.0792;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;44;-1930.496,-967.8395;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;43;-2231.985,-965.5569;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-727.358,587.1547;Float;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.75;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;36;-3503.762,-1258.895;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;46;-2336.64,-1772.106;Float;False;Constant;_Color2;Color 2;5;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;213.357,597.5519;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-2884.199,654.7434;Float;False;Property;_TransitionDistance;TransitionDistance;6;0;Create;True;0;0;False;0;4;3.46;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;55;-3077.58,306.6915;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;59;-2740.212,435.2416;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-2622.083,769.1622;Float;False;Property;_TransitionFalloff;TransitionFalloff;8;0;Create;True;0;0;False;0;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;57;-2501.043,458.9066;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;56;-2253.043,522.9067;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-2555.228,64.25444;Float;False;Constant;_Color3;Color 3;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;45;-2349.243,-1540.114;Float;False;Constant;_Color1;Color 1;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-3280.327,-886.5027;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;488.4923,92.15024;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;JoakimShaderCollection/Dissolve;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;4;0
WireConnection;3;1;7;0
WireConnection;1;0;3;0
WireConnection;28;0;32;4
WireConnection;5;0;1;0
WireConnection;5;1;6;0
WireConnection;5;2;6;0
WireConnection;33;0;28;0
WireConnection;33;1;27;0
WireConnection;15;0;5;0
WireConnection;15;1;33;0
WireConnection;39;0;36;0
WireConnection;39;1;37;0
WireConnection;42;0;49;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;21;2;20;0
WireConnection;47;0;46;0
WireConnection;47;1;45;0
WireConnection;47;2;44;0
WireConnection;9;0;10;0
WireConnection;9;1;5;0
WireConnection;9;2;28;0
WireConnection;49;0;39;0
WireConnection;64;0;52;0
WireConnection;64;1;51;0
WireConnection;64;2;56;0
WireConnection;44;0;43;0
WireConnection;43;0;42;0
WireConnection;34;0;15;0
WireConnection;34;1;35;0
WireConnection;55;0;53;0
WireConnection;55;1;58;0
WireConnection;59;0;55;0
WireConnection;59;1;60;0
WireConnection;57;0;59;0
WireConnection;57;1;61;0
WireConnection;56;0;57;0
WireConnection;37;0;38;0
WireConnection;0;0;9;0
WireConnection;0;2;34;0
ASEEND*/
//CHKSM=05E37C08719DB5DD2BD897B1B0FDD65C266C8E54