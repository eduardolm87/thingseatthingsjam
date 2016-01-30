using UnityEngine;
using System.Collections;

public class Wanderer : Brain
{
    public override void GetInput()
    {
        //base.GetInput();

        if (Random.value < .01f)
            MoveToRandomPosition();

    }

    void MoveToRandomPosition()
    {
        Vector3 nuPos = transform.position;
        nuPos.x += (.5f - Random.value) * 5.0f;
        nuPos.z += (.5f - Random.value) * 3.0f;
        Creature.Locomotor.TargetPosition = nuPos;
    }
}
