using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class TotemManager : MonoBehaviour
{
    public List<Creature.CREATURES> TotemOrder = new List<Creature.CREATURES>();
    public List<TotemPart> TotemParts = new List<TotemPart>();
    public Color RevealedPartColor = Color.white;
    public Color HiddenPartColor = Color.gray;


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

    public bool isPlayerAtMaximumIncarnation
    {
        get
        {
            return (NextCreatureToEmbody() == Creature.CREATURES.END);
        }
    }

    public Creature.CREATURES EndCreature
    {
        get
        {
            return TotemOrder.Last();
        }
    }

    public void Refresh()
    {
        Reset();
        int maxTotemIndex = GetCurrentCreatureIndex();
        for (int i = 0; i <= maxTotemIndex; i++)
        {
            Creature.CREATURES currentCreature = TotemOrder[i];
            SetTotemPart(currentCreature, true);
        }

        RefreshClue();
    }

    public void SetTotemPart(Creature.CREATURES zTotem, bool zStatus)
    {
        TotemPart totem = GetTotemPart(zTotem);
        if (zStatus)
            totem.Reveal();
        else
            totem.Hide();
    }

    TotemPart GetTotemPart(Creature.CREATURES zTotem)
    {
        return TotemParts.FirstOrDefault(c => c.AssociatedCreature == zTotem);
    }

    public void Reset()
    {
        TotemParts.ForEach(t => t.Hide());
    }

    void RefreshClue()
    {
        if (Creature.Player.CreatureType == Creature.CREATURES.TinyLight)
        {
            IngameUI.Instance.ClueText.text = "Click on a Rabbit to start the Inkarmation ritual";
        }
        else
        {
            if (isPlayerAtMaximumIncarnation)
            {
                IngameUI.Instance.ClueText.text = "Go to the Altar and end the ritual";
            }
            else
            {
                if (IngameUI.IsPlayerAtFullIncarnationEnergy)
                {
                    IngameUI.Instance.ClueText.text = "Inkarmation energy full! Get killed by a " + NextCreatureToEmbody().ToString();
                }
                else
                {
                    IngameUI.Instance.ClueText.text = "Collect tokens to refill and avoid getting killed";
                }
            }
        }
    }
}
