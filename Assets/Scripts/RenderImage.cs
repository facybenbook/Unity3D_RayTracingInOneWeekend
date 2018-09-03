using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderImage : MonoBehaviour {
    public RayTracer mRayTracer;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(mRayTracer.renderOut, destination);
    }
}
