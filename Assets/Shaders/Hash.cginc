// https://www.shadertoy.com/view/XlycWh
// Hash functions by Nimitz:
// https://www.shadertoy.com/view/Xt3cDn

uint BaseHash(uint2 p) {
	p = 1103515245U * ((p >> 1U) ^ (p.yx));
	uint h32 = 1103515245U * ((p.x) ^ (p.y >> 3U));
	return h32 ^ (h32 >> 16);
}

float Hash1(inout float seed) {
	uint n = BaseHash(asuint(float2(seed += .1, seed += .1)));
	return float(n) / float(0xffffffffU);
}

float2 Hash2(inout float seed) {
	uint n = BaseHash(asuint(float2(seed += .1, seed += .1)));
	uint2 rz = uint2(n, n * 48271U);
	return float2(rz & uint2(0x7fffffffU, 0x7fffffffU)) / float(0x7fffffff);
}

float3 Hash3(inout float seed) {
	uint n = BaseHash(asuint(float2(seed += .1, seed += .1)));
	uint3 rz = uint3(n, n * 16807U, n * 48271U);
	return float3(rz & uint3(0x7fffffffU, 0x7fffffffU, 0x7fffffffU)) / float(0x7fffffff);
}


