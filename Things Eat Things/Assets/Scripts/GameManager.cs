using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    public IEnumerator GameOver()
    {
        DisableAllCreaturesButThePlayer();
        DisablePlayerControl();

        yield return new WaitForEndOfFrame();

        float timescale = 1;
        while (timescale > 0.25f)
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

}
