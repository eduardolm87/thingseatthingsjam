using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(transform.root);
    }
    #endregion

    public Hitbox HitboxPrefab;

    public Hitbox HunterBulletPrefab;

    public GotoPointer Gotopointer;

    public IEnumerator GameOver()
    {
        yield return StartCoroutine(StopTheGame());

        IngameUI.Instance.FadeScreen.FadeIn(2);
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Game Over");
        //todo: now what? title?
    }

    public IEnumerator WinTheGame()
    {
        yield return StartCoroutine(StopTheGame());

        Debug.Log("You won the game!");
        IngameUI.Instance.FadeScreen.FadeIn(2);
        yield return new WaitForSeconds(2.5f);

        //todo: now what? title -> Credits?
    }

    IEnumerator StopTheGame()
    {
        DestroyAllHitboxes();
        yield return new WaitForEndOfFrame();

        DisableAllCreaturesButThePlayer();
        yield return new WaitForEndOfFrame();

        DisablePlayerControl();
        yield return new WaitForEndOfFrame();

    }




    public void StartNewGame()
    {
        Reset();
        SceneManager.LoadScene("RoughLayout", LoadSceneMode.Single);

        if (IngameUI.Instance != null)
        {
            IngameUI.Instance.FadeScreen.FadeOut(1);
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;
    }

    public void DisableAllCreaturesButThePlayer()
    {
        List<Creature> listOfCreatures = GameObject.FindObjectsOfType<Creature>().ToList();
        listOfCreatures.Remove(Creature.Player);

        listOfCreatures.ForEach(creature =>
        {
            Destroy(creature.gameObject);
            //creature.Graphic.SpriteRenderer.enabled = false;
            //creature.enabled = false;
            //creature.gameObject.SetActive(false);
        });

    }

    public void DestroyAllHitboxes()
    {
        List<Hitbox> listOfHitboxes = GameObject.FindObjectsOfType<Hitbox>().ToList();
        listOfHitboxes.ForEach(o => Destroy(o.gameObject));
    }

    public void DisablePlayerControl()
    {
        Creature.Player.enabled = false;
    }

    public IEnumerator ProceedToIncarnate(Creature zCreatureYouWhere, Creature zCreatureYouWillBecome)
    {
        Debug.Log("Incarnate from " + zCreatureYouWhere.name + " to " + zCreatureYouWillBecome.name);

        DestroyAllHitboxes();

        //Health fix
        zCreatureYouWhere.health = zCreatureYouWhere.maxhealth;
        zCreatureYouWillBecome.health = zCreatureYouWillBecome.maxhealth;

        //Deplete your incarnation energy
        IngameUI.Instance.PlayerIncarnation = 0;

        //The killed creature gets its Brain destroyed
        Destroy(zCreatureYouWhere.Brain);
        yield return new WaitForEndOfFrame();

        //The killer creature swaps its brain for a PlayerInput component
        Brain oldBrain = zCreatureYouWillBecome.GetComponent<Brain>();
        Destroy(oldBrain);
        yield return new WaitForEndOfFrame();

        Brain newBrain = zCreatureYouWillBecome.gameObject.AddComponent<PlayerInput>();
        zCreatureYouWillBecome.GetBrain();

        //We kill the old creature
        zCreatureYouWhere.EffectsWhenDestroyed();
        Destroy(zCreatureYouWhere.gameObject);
        yield return new WaitForEndOfFrame();

    }
}
