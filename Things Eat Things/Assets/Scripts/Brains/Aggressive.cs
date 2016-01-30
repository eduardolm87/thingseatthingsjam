using UnityEngine;
using System.Collections;

public class Aggressive : Brain
{
    public override void GetInput()
    {
        base.GetInput();

        if(CanISeePlayer())
        {
            if(AmIInAttackDistanceOfPlayer())
            {
                Attack();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            MoveToRandomPosition();
        }

    }

    void Attack()
    {
        if (Creature.distanceToPlayer > 1.1f)
        {

        }
    }

    void MoveTowardsPlayer()
    {
        Creature.Locomotor.TargetPosition = Creature.Player.transform.position;
        Debug.Log(gameObject + "distance to player is " + Creature.distanceToPlayer); 
    }

    bool AmIInAttackDistanceOfPlayer()
    {
        if (Creature.distanceToPlayer > Creature.attackDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    bool CanISeePlayer()
    {

        if (Vector3.Distance(Creature.Player.transform.position, transform.position) > Creature.detectionDistance)
        {
            return false;
        }

        else
        {
            return true;
        }
        
    }

    void MoveToRandomPosition()
    {
        Vector3 nuPos = transform.position;
        nuPos.x += (.5f - Random.value) * 5.0f;
        nuPos.z += (.5f - Random.value) * 3.0f;
        Creature.Locomotor.TargetPosition = nuPos;
    }
}
