using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public static Creature Player;

    public float health;
    public float speed;
    public float detectionDistance;
    public float attackDistance;
    public float distanceToPlayer;
    public Vector3 targetPos;

    [HideInInspector]
    public Graphic Graphic;

    [HideInInspector]
    public Locomotor Locomotor;

    [HideInInspector]
    public Brain Brain;

    [HideInInspector]
    public Animator Animator;


    void Awake()
    {
        Graphic = GetComponent<Graphic>();
        Locomotor = GetComponent<Locomotor>();
        Brain = GetComponent<Brain>();
		  Animator = GetComponentInChildren<Animator>();

        detectionDistance = 20f;
        //distanceToPlayer = Vector3.Distance(Creature.Player.transform.position, this.transform.position);

        if (Brain == null) Debug.LogError("Brain not found for " + gameObject.name);

        if (GetComponents<Brain>().Length > 1)
            Debug.LogError(name + " has too many Brains!");

        if (Brain is PlayerInput)
        {
            Player = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        Brain.GetInput();
        distanceToPlayer = Vector3.Distance(Creature.Player.transform.position, this.transform.position);
        //// walk towards:
        //Vector3 posOffs = targetPos - transform.position;
        //if (posOffs.magnitude > .1f)
        //{
        //    posOffs = posOffs.normalized * speed * Time.deltaTime;
        //    transform.position += posOffs;
        //}

    }
}
