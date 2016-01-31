using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image BlackScreen;

    float fadeTime = 1;

    public void FadeIn(float zTime)
    {
        fadeTime = zTime;

        if (!BlackScreen.enabled)
        {
            BlackScreen.enabled = true;
        }

        BlackScreen.CrossFadeAlpha(1, fadeTime, false);
    }

    public void FadeOut(float zTime)
    {
        BlackScreen.enabled = true;
        BlackScreen.CrossFadeAlpha(0, fadeTime, false);
        Invoke("DisableAfterTime", fadeTime);
    }

    void DisableAfterTime()
    {
        BlackScreen.enabled = false;
    }

}
