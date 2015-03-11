sampler2D sample;
float time; //this value is auto-set by Whiskey2D
float2 mousePosition; //this value is auto-set by Whiskey2D
float2 cameraTranslation;
float cameraZoom;


struct PSInput
{
	float2 Texcoord : TEXCOORD0;
};

float2 translate(float2 v){
	return (v - cameraTranslation) / cameraZoom;
}

float4 ps_main( PSInput PSin )  : COLOR0
{
	float4 color = tex2D(sample, PSin.Texcoord);
	
	
	color.r = .2f;
	color.g = .4f;
	color.b = .6f;
	
	if (color.a > .6){
		color.a = .6;
	} else color.a = 0;
	
	return color;
}



technique DefaultTechnique
{
	pass p0
	{
		PixelShader = compile ps_2_0 ps_main();
	}
}

















