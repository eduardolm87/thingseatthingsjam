﻿using UnityEngine;
using System.Collections;

public class Locomotor : MonoBehaviour
{
    public enum MovementTypes { WALK = 0, JUMP = 1 };

    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public CapsuleCollider CapsuleCollider;
    [HideInInspector]
    public Creature Creature;


    Vector3 targetPosition = Vector3.zero;
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }
        set
        {
            if (!InAir)
            {
                targetPosition = value;
            }
        }
    }

    [HideInInspector]
    public float MinimumDistanceToTargetPosition = 1.1f;


    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        Creature = GetComponent<Creature>();
        TargetPosition = transform.position;
    }

    void Start()
    {
        AutoGround();
        TargetPosition = transform.position;
        Stop();

        MinimumDistanceToTargetPosition = CapsuleCollider.radius * +0.5f;
    }

    void Update()
    {
        if (InAir)
        {
            return;
        }

        if (IsFarFromTargetPosition)
        {
            switch (Creature.MovementType)
            {
                case MovementTypes.JUMP:
                    JumpTowardsPoint(TargetPosition, Creature.speed);
                    break;
                default:
                    MoveTowardsPoint(TargetPosition, Creature.speed);
                    break;
            }
        }
        else
        {
            if (Creature.isPlayer && GameManager.Instance.Gotopointer.gameObject.activeInHierarchy)
            {
                GameManager.Instance.Gotopointer.Hide();
            }

            Stop();
        }


        if (Creature.Animator != null)
        {
            Vector3 velocity = Rigidbody.velocity;
            if (velocity.x < -0.05f)
            {
                Creature.Sprite.flipX = true;
            }
            else if (velocity.x > 0.05f)
            {
                Creature.Sprite.flipX = false;
            }

            if (velocity.magnitude > .5f)
                Creature.Animator.SetBool("running", true);
            else if (velocity.magnitude < .1f)
                Creature.Animator.SetBool("running", false);
        }
    }




    bool IsFarFromTargetPosition
    {
        get
        {
            return (Vector3.SqrMagnitude(TargetPosition - transform.position) > MinimumDistanceToTargetPosition);
        }
    }

    public bool InAir
    {
        get
        {
            return !Physics.Raycast(transform.position, -Vector3.up, CapsuleCollider.bounds.extents.y + 0.1f);
        }
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
        TargetPosition = transform.position;
    }

    public void MoveTowardsPoint(Vector3 zPoint, float zSpeed = 1)
    {
        Vector3 targetDir = new Vector3(zPoint.x, transform.position.y, zPoint.z);
        targetDir -= transform.position;

        Rigidbody.velocity = targetDir.normalized * zSpeed;
    }

    public void JumpTowardsPoint(Vector3 zPoint, float zSpeed = 1)
    {
        float jumpSpeed = 1f;

        Vector3 targetDir = (new Vector3(zPoint.x, transform.position.y, zPoint.z) - transform.position).normalized * zSpeed;

        targetDir.y = transform.position.y + jumpSpeed;

        if (Creature.Cooldown == 0)
        {
            Rigidbody.velocity = targetDir;
            Creature.Cooldown = 0.75f;
        }
    }

    public void AutoGround()
    {
        RaycastHit RayCast = new RaycastHit();
        Ray Ray = new Ray((transform.position + Vector3.up * 20), -Vector3.up);
        if (Physics.Raycast(Ray, out RayCast, 5000))
        {
            transform.position = RayCast.point + (0.01f * Vector3.up);
        }
        else
        {
            Debug.LogError("Can't ground " + name);
        }
    }

}
