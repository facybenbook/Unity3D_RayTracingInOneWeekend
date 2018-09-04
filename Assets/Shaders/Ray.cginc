struct Ray
{
	float3 origin;
	float3 direction;

	float3 PointAtParameter(float t);
};

float3 Ray::PointAtParameter(float t)
{
	return origin + direction * t;
}

Ray InitRay(float3 origin, float3 direction)
{
	Ray r;

	r.origin = origin;
	r.direction = direction;

	return r;
}