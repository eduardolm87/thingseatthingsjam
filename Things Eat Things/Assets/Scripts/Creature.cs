using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public float health;
    public float speed;
    public Vector3 targetPos;

    [HideInInspector]
    public Graphic Graphic;

    [HideInInspector]
    public Locomotor Locomotor;

    [HideInInspector]
    public Brain Brain;

    void Awake()
    {
        Graphic = GetComponent<Graphic>();
        Locomotor = GetComponent<Locomotor>();
        Brain = GetComponent<Brain>();

        if (Brain == null) Debug.LogError("Brain not found for " + gameObject.name);

        if (GetComponents<Brain>().Length > 1)
            Debug.LogError(name + " has too many Brains!");
    }

    void Start()
    {


    }

    void Update()
    {
        Brain.GetInput();

        //// walk towards:
        //Vector3 posOffs = targetPos - transform.position;
        //if (posOffs.magnitude > .1f)
        //{
        //    posOffs = posOffs.normalized * speed * Time.deltaTime;
        //    transform.position += posOffs;
        //}

    }
}
