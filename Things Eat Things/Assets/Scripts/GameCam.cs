using UnityEngine;
using System.Collections;

public class GameCam : MonoBehaviour
{
    public float camSpeed;
    Vector3 targetLookPos;
    public float dist;


    void Start()
    {
        //Vector3 playerPos = new Vector3()
        //todo: Move instantly to the player's pos
    }

    void FixedUpdate()
    {
		 transform.rotation = Quaternion.Euler( GameManager.Instance.CamTilt, 0,0 );
        Vector3 offset = new Vector3(0, Mathf.Sin(-Mathf.Deg2Rad * GameManager.Instance.CamTilt), Mathf.Cos(-Mathf.Deg2Rad * GameManager.Instance.CamTilt));
        offset *= dist;

        Vector3 targetPos = targetLookPos - offset;
        float interp = Mathf.Clamp(Time.deltaTime * camSpeed, 0, 1);
        Vector3 nuPos = transform.position * (1.0f - interp) + targetPos * (interp);
        transform.position = nuPos;
    }


    public void LookAtThis(Vector3 inPos)
    {
        targetLookPos = inPos;
    }

}
