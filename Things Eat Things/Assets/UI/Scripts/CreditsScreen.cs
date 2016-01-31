using UnityEngine;
using System.Collections;

public class CreditsScreen : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        Close();
    }
}
