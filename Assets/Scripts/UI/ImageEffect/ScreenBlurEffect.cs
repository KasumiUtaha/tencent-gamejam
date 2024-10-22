using System;
using UnityEngine;
// 这段代码保证了挂载的时候一定要有Camera组件
[RequireComponent(typeof(Camera))]
public class ScreenBlurEffect : MonoBehaviour
{
    // 预先定义shader渲染用的pass
    const int BLUR_HOR_PASS = 0;
    const int BLUR_VER_PASS = 1;
    bool is_support; // 判断当前平台是否支持模糊

    RenderTexture final_blur_rt;
    RenderTexture temp_rt;
    [SerializeField]
    public Material blur_mat; // 模糊材质球

    // 外部参数
    [Range(0, 127)]
    float blur_size = 1.0f; // 模糊额外散步大小
    [Range(1, 10)]
    public int blur_iteration = 4; // 模糊采样迭代次数
    public float blur_spread = 1; // 模糊散值
    int cur_iterate_num = 1; // 当前迭代次数
    public int blur_down_sample = 4; // 模糊初始降采样比率
    public bool render_blur_effect = false; // 是否开始渲染模糊效果

    void Awake()
    {
        is_support = SystemInfo.supportsImageEffects;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (is_support && blur_mat != null && render_blur_effect)
        {
            // 首先对输出的结果做一次降采样，也就是降低分辨率，减小RT图的大小
            int width = src.width / blur_down_sample;
            int height = src.height / blur_down_sample;
            // 将当前摄像机画面渲染到被降采样的RT上
            final_blur_rt = RenderTexture.GetTemporary(width, height, 0);
            Graphics.Blit(src, final_blur_rt);

            cur_iterate_num = 1; // 初始化迭代
            while (cur_iterate_num <= blur_iteration)
            {
                blur_mat.SetFloat("_BlurSize", (1.0f + cur_iterate_num * blur_spread) * blur_size);  // 设置模糊扩散uv偏移
                temp_rt = RenderTexture.GetTemporary(width, height, 0);
                // 使用blit的其他重载，针对对应的材质球和pass进行渲染并输出结果
                Graphics.Blit(final_blur_rt, temp_rt, blur_mat, BLUR_HOR_PASS);
                Graphics.Blit(temp_rt, final_blur_rt, blur_mat, BLUR_VER_PASS);
                RenderTexture.ReleaseTemporary(temp_rt);   // 释放临时RT
                cur_iterate_num++;
            }
            Graphics.Blit(final_blur_rt, dest);
            RenderTexture.ReleaseTemporary(final_blur_rt);  // final_blur_rt作用已经完成，可以回收了
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}