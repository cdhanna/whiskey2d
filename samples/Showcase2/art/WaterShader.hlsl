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
	
	
	color.r = .1;
	color.g = .2;
	color.b = .3;
	
	if (color.a > .6){
		color.a = .9;
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



























