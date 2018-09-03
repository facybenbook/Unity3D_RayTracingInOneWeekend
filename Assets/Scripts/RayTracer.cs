using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour {
    public ComputeShader mRayTracer;

    private const int w = 1280, h = 720;
    private const float ratio = (float)w / (float)h;
    private const int numThreads = 8;
    private const int numGroupX = w / numThreads;
    private const int numGroupY = h / numThreads;
    private int kernel;

    private RenderTexture _renderOut;
    public RenderTexture renderOut
    {
        get { return _renderOut; }
    }

	void Start ()
    {
        InitResources();
	}
	
	void Update ()
    {
        DispatchComputeShader();
	}

    void DispatchComputeShader()
    {
        mRayTracer.SetTexture(kernel, "RenderOut", _renderOut);

        mRayTracer.Dispatch(kernel, numGroupX, numGroupY, 1);
    }

    void InitResources()
    {
        // render texture for compute shader output
        _renderOut = new RenderTexture(w, h, 0);
        _renderOut.format = RenderTextureFormat.ARGB32;
        _renderOut.filterMode = FilterMode.Bilinear;
        _renderOut.wrapMode = TextureWrapMode.Clamp;
        _renderOut.enableRandomWrite = true;
        _renderOut.Create();

        // setup compute shader 
        kernel = mRayTracer.FindKernel("RayTrace");

        mRayTracer.SetFloat("uWidth", (float)w);
        mRayTracer.SetFloat("uHeight", (float)h);
        mRayTracer.SetFloat("uRatio", ratio);
    }

    void DestroyResources()
    {
        // destroy render texture
        if(_renderOut != null)
        {
            _renderOut.Release();
            _renderOut = null;
            Destroy(_renderOut);
        }

    }
}
