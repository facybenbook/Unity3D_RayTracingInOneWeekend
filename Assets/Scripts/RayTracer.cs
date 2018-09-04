using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour {
    public ComputeShader mRayTracer;

    private const int w = 1280, h = 720;
    private const int samples = 128;
    private const float ratio = (float)w / (float)h;
    private const int numThreads = 8;
    private const int numGroupX = w / numThreads;
    private const int numGroupY = h / numThreads;
    private const int numGroupZ = samples / numThreads;
    private int kernel_ray_trace;

    private int animFrame = 0;

    struct RayTracerLayout
    {
        public Vector4 accumColor;
    }

    public int sampleCount
    {
        get { return samples; }
    }

    public int width
    {
        get { return w; }
    }

    public int height
    {
        get { return h; }
    }

    private ComputeBuffer _rayTraceLayout;
    public ComputeBuffer rayTraceLayout
    {
        get { return _rayTraceLayout; }
    }

    #region MonoBehaviour
    void Start ()
    {
        InitResources();
    }
	
	void Update ()
    {
        DispatchComputeShader();

        if (animFrame > 9999) animFrame = 0;

        animFrame++;
    }

    private void OnDestroy()
    {
        DestroyResources();
    }
    #endregion

    #region Compute Shader
    void DispatchComputeShader()
    {
        mRayTracer.SetFloat("uFrame", (float)animFrame);
        mRayTracer.SetBuffer(kernel_ray_trace, "rayTraceLayout", _rayTraceLayout);

        mRayTracer.Dispatch(kernel_ray_trace, numGroupX, numGroupY, numGroupZ);
    }
    #endregion

    #region Resources
    ComputeBuffer InitComputeBuffer(int x, int y, int z)
    {
        int size = x * y * z;

        RayTracerLayout[] layout = new RayTracerLayout[size];

        for(int i = 0; i < size; i++)
        {
            layout[i].accumColor = Vector4.zero;
        }

        const int stride = 16; // size of RayTraceLayout
        ComputeBuffer b = new ComputeBuffer(size, stride);

        b.SetData(layout);

        return b;
    }

    void InitResources()
    {
        // render texture for compute shader output
        _rayTraceLayout = InitComputeBuffer(w, h, samples);

        // setup compute shader 
        kernel_ray_trace = mRayTracer.FindKernel("RayTrace");

        mRayTracer.SetFloat("uWidth", (float)w);
        mRayTracer.SetFloat("uHeight", (float)h);
        mRayTracer.SetFloat("uRatio", ratio);
        mRayTracer.SetFloat("uSampleCount", (float)samples);
    }

    void DestroyResources()
    {
        if (_rayTraceLayout != null)
        {
            _rayTraceLayout.Dispose();
            _rayTraceLayout = null;
        }
    }
    #endregion
}
