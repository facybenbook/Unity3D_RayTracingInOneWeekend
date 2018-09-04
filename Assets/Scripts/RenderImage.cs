using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderImage : MonoBehaviour {
    public RayTracer mRayTracer;
    public Shader blendAccumeShader;
    private Material blendAccumMat;

    private void Start()
    {
        blendAccumMat = new Material(blendAccumeShader);
        blendAccumMat.SetFloat("uSampleCount", (float)mRayTracer.sampleCount);
        blendAccumMat.SetFloat("uWidth", (float)mRayTracer.width);
        blendAccumMat.SetFloat("uHeight", (float)mRayTracer.height);
    }

    private void LateUpdate()
    {
        blendAccumMat.SetBuffer("uRayTraceLayout", mRayTracer.rayTraceLayout);
    }

    private void OnDestroy()
    {
        if(blendAccumMat != null)
        {
            Destroy(blendAccumMat);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, blendAccumMat);
    }
}
