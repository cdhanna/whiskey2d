sampler2D baseMap;
sampler2D backgroundMap : register(s1);

float screenWidth;
float screenHeight;
float test;
float4 ambience;

struct PS_INPUT 
{
   float2 Texcoord : TEXCOORD0;

};

float4 SoftLight (float4 cBase, float4 cBlend)
{
	float4 cNew;
	if (cBlend.r > .5) { cNew.r = cBase.r * (1 - (1 - cBase.r) * (1 - 2 * (cBlend.r)));}
	else { cNew.r = 1 - (1 - cBase.r) * (1 - (cBase.r * (2 * cBlend.r))); }
	
	if (cBlend.g > .5) { cNew.g = cBase.g * (1 - (1 - cBase.g) * (1 - 2 * (cBlend.g)));}
	else { cNew.g = 1 - (1 - cBase.g) * (1 - (cBase.g * (2 * cBlend.g))); }
	
	if (cBlend.g > .5) { cNew.b = cBase.b * (1 - (1 - cBase.b) * (1 - 2 * (cBlend.b)));}
	else { cNew.b = 1 - (1 - cBase.b) * (1 - (cBase.b * (2 * cBlend.b))); }
	
	cNew.a = 1.0;
	return cNew;
}


float4 ps_main( PS_INPUT Input ) : COLOR0
{
	float4 bColor = tex2D(backgroundMap, Input.Texcoord);

	float4 pColor = tex2D(baseMap, Input.Texcoord);

	/*if (bColor.r > 1) {bColor.r = 1;} if (bColor.r < 0) { bColor.r = 0;}
	if (bColor.g > 1) {bColor.g = 1;} if (bColor.g < 0) { bColor.g = 0;}
	if (bColor.b > 1) {bColor.b = 1;} if (bColor.b < 0) { bColor.b = 0;}
	if (pColor.r > 1) {pColor.r = 1;} if (pColor.r < 0) { pColor.r = 0;}
	if (pColor.g > 1) {pColor.g = 1;} if (pColor.g < 0) { pColor.g = 0;}
	if (pColor.b > 1) {pColor.b = 1;} if (pColor.b < 0) { pColor.b = 0;}*/

	//float4 rColor = SoftLight(bColor, pColor);

	bColor += ambience;

	float4 rColor = pColor*bColor;
	rColor.w = 1;
	
	
	
	return float4(rColor.x, rColor.y, rColor.z, rColor.w);

}




// Default Technique
technique DefaultTechnique
{
	pass p0
	{
		PixelShader = compile ps_2_0 ps_main();
	}
}