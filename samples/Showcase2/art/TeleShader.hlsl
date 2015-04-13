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
	float2 v = PSin.Texcoord;
	v.x +=.005 * sin(47 * translate(v).y + time * 180*translate(v).x + 10*translate(v).x);
	v.y +=.005 * sin(47 * translate(v).x + time * 180*translate(v).y + 10*translate(v).y);
	float4 color = tex2D(sample, v);
	
	
	
	
	return color;
}

technique DefaultTechnique
{
	pass p0
	{
		PixelShader = compile ps_2_0 ps_main();
	}
}



