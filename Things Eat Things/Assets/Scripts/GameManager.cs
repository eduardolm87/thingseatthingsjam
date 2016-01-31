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
    public static bool NormalFlow = false;

    void Awake()
    {
        Instance = this;



        DontDestroyOnLoad(transform.root);

        if (NormalFlow == true)
        {
            GameObject.FindObjectOfType<FadeScreen>().BlackScreen.enabled = true;
        }
    }
    #endregion

    public Hitbox HitboxPrefab;

    public Hitbox HunterBulletPrefab;

    public GotoPointer Gotopointer;

    void Start()
    {
        StartCoroutine(StartNewGame());
    }

    public IEnumerator GameOver()
    {
        yield return StartCoroutine(StopTheGame());

        Debug.Log("Game Over");

        IngameUI.Instance.FadeScreen.FadeIn(2);
        yield return new WaitForSeconds(2.5f);

        ToMainMenu();
    }

    public IEnumerator WinTheGame()
    {
        yield return StartCoroutine(StopTheGame());

        Debug.Log("You won the game!");
        IngameUI.Instance.FadeScreen.FadeIn(2);
        yield return new WaitForSeconds(2.5f);

        ToMainMenu();
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




    public IEnumerator StartNewGame()
    {
        PlayerInput.AcceptInput = false;

        IngameUI.Instance.FadeScreen.BlackScreen.enabled = true;
        IngameUI.Instance.FadeScreen.BlackScreen.color = Color.black;

        yield return new WaitForSeconds(1);

        IngameUI.Instance.TotemManager.Reset();
        IngameUI.Instance.TotemManager.Refresh();


        IngameUI.Instance.FadeScreen.FadeOut(1);

        SpawnPlayerEffect(2);

        yield return new WaitForSeconds(2.25f);

        PlayerInput.AcceptInput = true;
    }

    void SpawnPlayerEffect(float zTime)
    {
        iTween.ScaleFrom(Creature.Player.gameObject, iTween.Hash("scale", Vector3.zero, "time", zTime, "easetype", iTween.EaseType.easeOutQuad));
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene(0);
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
        PlayerInput.AcceptInput = false;
        Creature.Player.enabled = false;
        Creature.Player.Graphic.SpriteRenderer.enabled = false;
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


        IngameUI.Instance.TotemManager.Refresh();

    }
}
