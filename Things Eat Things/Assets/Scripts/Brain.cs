using UnityEngine;
using System.Collections;

public class Brain : MonoBehaviour
{
    [HideInInspector]
    public Creature Creature;

    void Awake()
    {
        Creature = GetComponent<Creature>();
    }

    public virtual void GetInput()
    {

    }

}
