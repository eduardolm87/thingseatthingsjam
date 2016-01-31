using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IngameUI : MonoBehaviour
{
    public static IngameUI Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton Error in IngameUI");
        }
        Instance = this;
    }

    public TotemManager TotemManager;

    public const int minIncarnation = 0;
    public const int maxIncarnation = 50;


    public Image IncarnationBar;

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
            return Instance.playerIncarnation >= maxIncarnation;
        }
    }


    public void Update()
    {
        //todo: DEBUG
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            playerIncarnation -= 3;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            playerIncarnation += 3;
        }
        //

        RefreshIncarnationBar();
        RefreshTotem();
    }

    void RefreshIncarnationBar()
    {
        IncarnationBar.fillAmount = playerIncarnation * 1f / maxIncarnation;
    }

    void RefreshTotem()
    {
        //todo
    }
}
