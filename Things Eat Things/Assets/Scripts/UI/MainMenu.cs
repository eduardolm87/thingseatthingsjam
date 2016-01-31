using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public FadeScreen FadeScreen;




    bool busy = false;


    public void Click_StarButton()
    {
        if (!busy)
            StartCoroutine(StartGame());
    }

    public void Click_Credits()
    {
        if (!busy)
            StartCoroutine(ShowCredits());
        //todo
    }


    IEnumerator StartGame()
    {
        busy = true;

        FadeScreen.FadeIn(2);
        yield return new WaitForSeconds(2);
        Debug.Log("Start");

        busy = false;
    }

    IEnumerator ShowCredits()
    {
        busy = true;

        yield return null;
        //TODO: show credits or something
        busy = false;
    }

}
