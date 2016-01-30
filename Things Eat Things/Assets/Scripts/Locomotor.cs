using UnityEngine;
using System.Collections;

public class Locomotor : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public CapsuleCollider CapsuleCollider;
    [HideInInspector]
    public Creature Creature;


    [HideInInspector]
    public Vector3 TargetPosition;
    const float MinimumDistanceToTargetPosition = 1f;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        Creature = GetComponent<Creature>();
        TargetPosition = transform.position;
    }

    void Update()
    {
        if (IsFarFromTargetPosition)
        {
            MoveTowardsPoint(TargetPosition, Creature.speed);
        }
        else
        {
            Stop();
        }


		  if( Creature.Animator != null ){
			  Vector3 velocity = Rigidbody.velocity;
			  if ( velocity.x < -0.05f ){
				  Creature.Sprite.flipX = true;
			  } else if ( velocity.x > 0.05f ){
				  Creature.Sprite.flipX = false;
			  }

			  if( velocity.magnitude > .5f )
				  Creature.Animator.SetBool( "running", true );
			  else if ( velocity.magnitude < .1f )
				  Creature.Animator.SetBool( "running", false );
		  }
    }




    bool IsFarFromTargetPosition
    {
        get
        {
            return (Vector3.SqrMagnitude(TargetPosition - transform.position) > MinimumDistanceToTargetPosition);
        }
    }

    public void MoveTowardsPoint(Vector3 zPoint, float zSpeed = 1)
    {
        Vector3 targetSpeed = new Vector3(zPoint.x, transform.position.y, zPoint.z);
        targetSpeed -= transform.position;

        Rigidbody.velocity = targetSpeed.normalized * zSpeed;
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
    }


}
