using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : UIManager
{
    [SerializeField]
    private ScreenBlurEffect screenBlurEffect;
    
    public void SetBlurOn()
    {
        screenBlurEffect.render_blur_effect = true;
    }

    public void SetBlurOff()
    {
        screenBlurEffect.render_blur_effect = false;
    }
}
