sampler2D sample;
float time; //this value is auto-set by Whiskey2D
float2 mousePosition; //this value is auto-set by Whiskey2D
float2 cameraTranslation; //this value is auto-set by Whiskey2D
float cameraZoom; //this value is auto-set by Whiskey2D

//transform a regular position to a game position
float2 translate (float2 v){
	 return (v - cameraTranslation) / cameraZoom;
}

struct PSInput
{
	float2 Texcoord : TEXCOORD0;
};

float4 ps_main( PSInput PSin )  : COLOR0
{
	float2 modPos = PSin.Texcoord;
	
	
	float4 color = tex2D(sample, PSin.Texcoord);
	//color.a = 0;
	//color.a = 1- abs(modPos.x -.5f )*1.5 ;
	color.gb = 0;
	color.r =  ( (modPos.x -.5));
	color.r = .5- abs(color.r);
	color.r *= modPos.y;
	//if (color.a > .5) color.a = 1; 
	//color.r = 0;
	color.a = color.r * 2;
	return color;
}

technique DefaultTechnique
{
	pass p0
	{
		PixelShader = compile ps_2_0 ps_main();
	}
}

