using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    public Image fillBar;

    const float TimeToHideBar = 2.5f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Invoke("Hide", TimeToHideBar);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
