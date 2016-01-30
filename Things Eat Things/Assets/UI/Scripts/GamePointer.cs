using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePointer : MonoBehaviour
{
    public Text Text;

    public static GamePointer Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("GamePointer singleton Error!");
        }
        Instance = this;
    }

    void Start()
    {
        Text.text = "";
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }


}
