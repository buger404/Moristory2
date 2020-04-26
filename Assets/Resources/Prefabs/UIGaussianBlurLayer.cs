using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// [ExecuteInEditMode]
public class UIGaussianBlurLayer : MonoBehaviour {

    public UnityEngine.UI.RawImage rImg;
    public Shader m_shr;

    [Range(0, 6), Tooltip("[降采样次数]向下采样的次数。此值越大,则采样间隔越大,需要处理的像素点越少,运行速度越快。")]
    public int DownSampleNum=2;
    [Range(0.0f, 20.0f), Tooltip("[模糊扩散度]进行高斯模糊时，相邻像素点的间隔。此值越大相邻像素间隔越远，图像越模糊。但过大的值会导致失真。")]
    public float BlurSpreadSize=3.0f;
    [Range(0, 8), Tooltip("[迭代次数]此值越大,则模糊操作的迭代次数越多，模糊效果越好，但消耗越大。")]
    public int BlurIterations=3;

    private Camera m_camera;
    private RenderTexture m_rt;
    private Material m_mat;
    private string m_shr_name="UI/UIGaussianBlurLayer";
    private Color m_color;

    #region MaterialGetAndSet
    Material material {
        get {
            if (m_mat==null) {
                m_mat=new Material(m_shr);
                m_mat.hideFlags=HideFlags.HideAndDontSave;
            }
            return m_mat;
        }
    }
    #endregion

    void Start () {
        m_camera = GetComponent<Camera>();
        m_shr=Shader.Find(m_shr_name);
        m_color = rImg.color;
        m_color.a = 1f;
    }

    private void Cleanup() {
        if (m_mat) Object.DestroyImmediate(m_mat);
        if (rImg.texture) RenderTexture.ReleaseTemporary(m_rt);
    }

    private void OnEnable() {
        Cleanup();
    }

    private void OnDestroy() {
        Cleanup();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest){
        Graphics.Blit(src, dest);

        if (!gameObject.activeInHierarchy && enabled) return;
        if (!m_camera || !m_shr || m_rt != null) return;

        float widthMod=1.0f / (1.0f * (1 << DownSampleNum));
        material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod);

        int renderWidth = src.width>>DownSampleNum;
        int renderHeight = src.height>>DownSampleNum;
        m_rt = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float);
        m_rt.filterMode = FilterMode.Bilinear;

        Graphics.Blit(src, m_rt, material, 0);
        for (int i = 0; i < BlurIterations; i++) {
            //【2.1】Shader参数赋值
            //迭代偏移量参数
            float iterationOffs = (i * 1.0f);
            //Shader的降采样参数赋值
            material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod + iterationOffs);
            // 【2.2】处理Shader的通道1，垂直方向模糊处理 || Pass1,for vertical blur
            // 定义一个临时渲染的缓存tempBuffer
            RenderTexture tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float);
            // 拷贝m_rt中的渲染数据到tempBuffer,并仅绘制指定的pass1的纹理数据
            Graphics.Blit(m_rt, tempBuffer, material, 1);
            //  清空m_rt
            RenderTexture.ReleaseTemporary(m_rt);
            // 将tempBuffer赋给m_rt，此时m_rt里面pass0和pass1的数据已经准备好
            m_rt = tempBuffer;
            // 【2.3】处理Shader的通道2，竖直方向模糊处理 || Pass2,for horizontal blur
            // 获取临时渲染纹理
            tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float);
            // 拷贝m_rt中的渲染数据到tempBuffer,并仅绘制指定的pass2的纹理数据
            Graphics.Blit(m_rt, tempBuffer, m_mat, 2);
            //【2.4】得到pass0、pass1和pass2的数据都已经准备好的m_rt
            // 再次清空m_rt
            RenderTexture.ReleaseTemporary(m_rt);
            // 再次将tempBuffer赋给m_rt，此时m_rt里面pass0、pass1和pass2的数据都已经准备好
            m_rt = tempBuffer;
        }
        rImg.texture = m_rt;
        rImg.color = m_color;
        m_camera.enabled = false;
        enabled = false;
    }
}