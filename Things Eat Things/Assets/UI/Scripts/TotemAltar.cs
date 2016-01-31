using UnityEngine;
using System.Collections;

public class TotemAltar : MonoBehaviour
{
    void OnTriggerEnter(Collider zOther)
    {
        Creature creature = zOther.GetComponent<Creature>();
        if (creature != null)
        {
            if (creature == Creature.Player)
            {
                PlayerEnters();
            }
        }
    }

    void PlayerEnters()
    {
        if (IngameUI.Instance.TotemManager.isPlayerAtMaximumIncarnation)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.WinTheGame());
        }
    }

    void OnMouseEnter()
    {
        if (Creature.Player.CreatureType != IngameUI.Instance.TotemManager.EndCreature)
            GamePointer.Instance.Text.text = "Only for " + IngameUI.Instance.TotemManager.EndCreature.ToString();
        else
            GamePointer.Instance.Text.text = "Fullfil the Ritual";
    }

    void OnMouseExit()
    {
        GamePointer.Instance.Text.text = "";
    }
}
