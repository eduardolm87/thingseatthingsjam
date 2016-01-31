using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IngameUI : MonoBehaviour
{
    #region Singleton

    public static IngameUI Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton Error in IngameUI");
        }
        Instance = this;
    }

    #endregion

    public TotemManager TotemManager;
    public FadeScreen FadeScreen;
    public Image IncarnationBar;
    public Image IncarnationBarBg;
    public Text ClueText;

    public const int minIncarnation = 0;
    public int maxIncarnation = 50;
    int playerIncarnation = minIncarnation;
    public int PlayerIncarnation
    {
        get
        {
            return playerIncarnation;
        }
        set
        {
            playerIncarnation = Mathf.Clamp(value, minIncarnation, maxIncarnation);
        }
    }
    public static bool IsPlayerAtFullIncarnationEnergy
    {
        get
        {
            return Instance.playerIncarnation >= IngameUI.Instance.maxIncarnation;
        }
    }

    public void Update()
    {
        //todo: DEBUG
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            playerIncarnation -= 20;
            TotemManager.Refresh();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            playerIncarnation += 20;
            TotemManager.Refresh();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            FadeScreen.FadeIn(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            FadeScreen.FadeOut(1);
        }
        //

        if (Creature.Player == null)
            return;

        RefreshIncarnationBar();
        RefreshTotem();
    }

    void RefreshIncarnationBar()
    {
        maxIncarnation = Creature.Player.IncarnationBarSize;

        IncarnationBar.rectTransform.sizeDelta = new Vector2(maxIncarnation * 2, 10);
        IncarnationBarBg.rectTransform.sizeDelta = IncarnationBar.rectTransform.sizeDelta;

        IncarnationBar.fillAmount = playerIncarnation * 1f / maxIncarnation;

        bool ShouldShowBar = (Creature.Player.CreatureType != Creature.CREATURES.TinyLight);

        IncarnationBar.enabled = ShouldShowBar;
        IncarnationBarBg.enabled = ShouldShowBar;

    }

    void RefreshTotem()
    {
        //todo
    }


}
