using System;
using UnityEngine;
// ��δ��뱣֤�˹��ص�ʱ��һ��Ҫ��Camera���
[RequireComponent(typeof(Camera))]
public class ScreenBlurEffect : MonoBehaviour
{
    // Ԥ�ȶ���shader��Ⱦ�õ�pass
    const int BLUR_HOR_PASS = 0;
    const int BLUR_VER_PASS = 1;
    bool is_support; // �жϵ�ǰƽ̨�Ƿ�֧��ģ��

    RenderTexture final_blur_rt;
    RenderTexture temp_rt;
    [SerializeField]
    public Material blur_mat; // ģ��������

    // �ⲿ����
    [Range(0, 127)]
    float blur_size = 1.0f; // ģ������ɢ����С
    [Range(1, 10)]
    public int blur_iteration = 4; // ģ��������������
    public float blur_spread = 1; // ģ��ɢֵ
    int cur_iterate_num = 1; // ��ǰ��������
    public int blur_down_sample = 4; // ģ����ʼ����������
    public bool render_blur_effect = false; // �Ƿ�ʼ��Ⱦģ��Ч��

    void Awake()
    {
        is_support = SystemInfo.supportsImageEffects;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (is_support && blur_mat != null && render_blur_effect)
        {
            // ���ȶ�����Ľ����һ�ν�������Ҳ���ǽ��ͷֱ��ʣ���СRTͼ�Ĵ�С
            int width = src.width / blur_down_sample;
            int height = src.height / blur_down_sample;
            // ����ǰ�����������Ⱦ������������RT��
            final_blur_rt = RenderTexture.GetTemporary(width, height, 0);
            Graphics.Blit(src, final_blur_rt);

            cur_iterate_num = 1; // ��ʼ������
            while (cur_iterate_num <= blur_iteration)
            {
                blur_mat.SetFloat("_BlurSize", (1.0f + cur_iterate_num * blur_spread) * blur_size);  // ����ģ����ɢuvƫ��
                temp_rt = RenderTexture.GetTemporary(width, height, 0);
                // ʹ��blit���������أ���Զ�Ӧ�Ĳ������pass������Ⱦ��������
                Graphics.Blit(final_blur_rt, temp_rt, blur_mat, BLUR_HOR_PASS);
                Graphics.Blit(temp_rt, final_blur_rt, blur_mat, BLUR_VER_PASS);
                RenderTexture.ReleaseTemporary(temp_rt);   // �ͷ���ʱRT
                cur_iterate_num++;
            }
            Graphics.Blit(final_blur_rt, dest);
            RenderTexture.ReleaseTemporary(final_blur_rt);  // final_blur_rt�����Ѿ���ɣ����Ի�����
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}