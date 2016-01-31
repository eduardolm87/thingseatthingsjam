using UnityEngine;
using System.Collections;

public class Wanderer : Brain
{
    public float timerToMove = 0;

    public int chanceToStop = 4;

    public override void GetInput()
    {
        //base.GetInput();

        if (timerToMove <= 0)
            MoveToRandomPosition();
        else
        {
            timerToMove -= Creature.RefreshFrequency;
            if (timerToMove < 0)
                timerToMove = 0;
        }

    }

    void MoveToRandomPosition()
    {
        if (Random.Range(1, 10) < chanceToStop)
        {
            Creature.Locomotor.Stop();
        }
        else
        {

            Vector3 nuPos = transform.position;
            nuPos.x += (.5f - Random.value) * 5.0f;
            nuPos.z += (.5f - Random.value) * 3.0f;
            Creature.Locomotor.TargetPosition = nuPos;
        }

        timerToMove = 1;
    }
}
