// https://www.shadertoy.com/view/llVcDz
float3 RandomInUnitSphere(float seed)
{
	float3 p = Hash3(seed) * float3(2., 6.28318530718, 1.) - float3(1., 0., 0.);
	float phi = p.y;
	float r = sqrt(p.z);

	return r * float3(sqrt(1. - p.x * p.x) * float2(sin(phi), cos(phi)), p.x);
}