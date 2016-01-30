using UnityEngine;
using System.Collections;

public class BranchesAnimation : MonoBehaviour
{
    public float Time = 5;

    void Start()
    {
        iTween.ScaleFrom(gameObject, iTween.Hash("scale", Vector3.one * 1.05f, "time", Time, "easetype", iTween.EaseType.easeInQuad, "looptype", iTween.LoopType.pingPong));
    }

}
