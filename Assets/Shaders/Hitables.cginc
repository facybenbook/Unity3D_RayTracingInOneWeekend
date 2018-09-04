struct HitRecord
{
	bool isHit;

	float tMin;
	float tMax;

	float t;
	float3 p;
	float3 normal;
};

HitRecord InitHitRecord(float tMin, float tMax)
{
	HitRecord h;

	h.isHit = false;

	h.tMin = tMin;
	h.tMax = tMax;

	h.t = tMax;

	h.p = float3(9999., 9999., 9999.);
	h.normal = float3(1., 0., 0.);

	return h;
}

struct Sphere
{
	float3 center;
	float radius;

	void Intersect(Ray r, inout HitRecord rec);
};

Sphere InitSphere(float3 center, float radius)
{
	Sphere s;

	s.center = center;
	s.radius = radius;

	return s;
}

void Sphere::Intersect(Ray r, inout HitRecord rec)
{
	float3 oc = r.origin - center;
	float a = dot(r.direction, r.direction);
	float b = dot(oc, r.direction);
	float c = dot(oc, oc) - radius * radius;
	float discriminant = b * b - a * c;

	if (discriminant > 0)
	{
		float temp = (-b - sqrt(discriminant)) / a;

		if (temp < rec.tMax && temp > rec.tMin)
		{
			rec.isHit = true;
			rec.tMax = temp;

			rec.t = temp;
			rec.p = r.PointAtParameter(temp);
			rec.normal = (rec.p - center) / radius;

			return;
		}

		temp = (-b + sqrt(discriminant)) / a;

		if (temp < rec.tMax && temp > rec.tMin)
		{
			rec.isHit = true;
			rec.tMax = temp;

			rec.t = temp;
			rec.p = r.PointAtParameter(temp);
			rec.normal = (rec.p - center) / radius;

			return;
		}
	}

	return;
}