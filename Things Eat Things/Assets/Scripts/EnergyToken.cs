using UnityEngine;
using System.Collections;

public class EnergyToken : Interactable
{
    public int IncarnationRecovery = 5;

    void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("position", gameObject.transform.position + Vector3.up * 0.25f, "time", 1.5f, "easetype", iTween.EaseType.easeOutQuad, "looptype", iTween.LoopType.pingPong));
    }

    public override void GetTouchedByCreature(Creature zCreature)
    {
        if (!zCreature.isPlayer)
            return;

        IngameUI.Instance.PlayerIncarnation += IncarnationRecovery;

        //Todo: Effect
        Destroy(gameObject);
    }
}
