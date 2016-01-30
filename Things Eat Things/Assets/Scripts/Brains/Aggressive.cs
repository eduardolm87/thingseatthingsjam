using UnityEngine;
using System.Collections;

public class Aggressive : Brain
{
    float distanceToPlayer;

    public override void GetInput()
    {
        if (Creature.Player == null)
            return;


        base.GetInput();

        distanceToPlayer = Vector3.Distance(Creature.Player.transform.position, this.transform.position);

        if (CanISeePlayer())
        {
            if (AmIInAttackDistanceOfPlayer())
            {
                if (Creature.Cooldown <= 0)
                    Attack();
                else
                    Creature.Locomotor.Stop();
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
        Creature.Locomotor.Stop();

        Vector3 attackDirection = Creature.Player.transform.position - transform.position;
        Hitbox.Shoot(Creature, transform.position, attackDirection, 3, 1, 1, true); //todo: make this variable
        Creature.Cooldown = Creature.cooldownAfterAttack;
    }

    void MoveTowardsPlayer()
    {
        Creature.Locomotor.TargetPosition = Creature.Player.transform.position;
        //Debug.Log(gameObject + "distance to player is " + Creature.distanceToPlayer);
    }

    bool AmIInAttackDistanceOfPlayer()
    {
        if (distanceToPlayer > Creature.attackDistance)
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
