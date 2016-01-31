using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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
        DisableAllCreaturesButThePlayer();
        DisablePlayerControl();

        yield return new WaitForEndOfFrame();

        float timescale = 1;
        while (timescale > 0.5f)
        {
            timescale -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = timescale;
        }

        Time.timeScale = 0;
        Debug.Log("Game Over");
    }




    public void StartNewGame()
    {
        Reset();
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
            creature.Graphic.SpriteRenderer.enabled = false;
            creature.enabled = false;

        });

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
