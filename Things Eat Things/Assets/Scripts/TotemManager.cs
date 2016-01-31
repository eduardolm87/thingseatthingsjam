using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class TotemManager : MonoBehaviour
{
    public List<Creature.CREATURES> TotemOrder = new List<Creature.CREATURES>();

    public Creature.CREATURES NextCreatureToEmbody()
    {
        int nextCreatureIndex = GetCurrentCreatureIndex() + 1;

        if (nextCreatureIndex < 0 || nextCreatureIndex >= TotemOrder.Count)
        {
            return Creature.CREATURES.END;
        }

        return TotemOrder[nextCreatureIndex];
    }

    int GetCurrentCreatureIndex()
    {
        return TotemOrder.IndexOf(Creature.Player.CreatureType);
    }
}
