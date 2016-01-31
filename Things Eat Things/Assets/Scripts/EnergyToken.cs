using UnityEngine;
using System.Collections;

public class EnergyToken : Interactable
{
    const int IncarnationRecovery = 20;

    void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("position", gameObject.transform.position + Vector3.up * 0.25f, "time", 1.5f, "easetype", iTween.EaseType.easeOutQuad, "looptype", iTween.LoopType.pingPong));
    }

    public override void GetTouchedByCreature(Creature zCreature)
    {
        if (!zCreature.isPlayer)
            return;

        if (zCreature.CreatureType == Creature.CREATURES.TinyLight)
            return;

        IngameUI.Instance.PlayerIncarnation += IncarnationRecovery;
        IngameUI.Instance.TotemManager.Refresh();

        //Todo: Effect
        Destroy(gameObject);
    }
}
