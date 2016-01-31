using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotemPart : MonoBehaviour
{
    public Image Image;
    public Creature.CREATURES AssociatedCreature;

    public void Reveal()
    {
        Image.color = IngameUI.Instance.TotemManager.RevealedPartColor;
    }

    public void Hide()
    {
        Image.color = IngameUI.Instance.TotemManager.HiddenPartColor;
    }
}
