using UnityEngine;
using System.Collections;

public class GotoPointer : MonoBehaviour
{
    public void Show(Vector3 zPosition)
    {
        transform.position = zPosition;
        gameObject.SetActive(true);

        transform.localScale = Vector3.one * 0.005f;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one * 0.0025f, "looptype", iTween.LoopType.pingPong, "time", 0.25f));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
