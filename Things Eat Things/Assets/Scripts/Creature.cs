using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public enum CREATURES { TinyLight, Rabbit, Wolf, Hunter, END };

    public static Creature Player;

    public const float HurtingTime = 0.5f;

    const float RefreshFrequency = 0.25f;


    public string DisplayName = "???";

    public int maxhealth = 5;
    
    [HideInInspector]
    public int health = 5;

    [Range(1f, 4.5f)]
    public float speed = 1;

    public float attackDistance = 1;
    public float cooldownAfterAttack = 0.75f;
    public int attackDamage = 1;

    public Locomotor.MovementTypes MovementType = Locomotor.MovementTypes.WALK;
    public CREATURES CreatureType = CREATURES.Rabbit;

    [HideInInspector]
    public Graphic Graphic;

    [HideInInspector]
    public Locomotor Locomotor;

    [HideInInspector]
    public Brain Brain;

    [HideInInspector]
    public Animator Animator;

    [HideInInspector]
    public SpriteRenderer Sprite;

    float cooldown = 0;
    public float Cooldown
    {
        get { return cooldown; }
        set
        {
            cooldown = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    [HideInInspector]
    public bool isHurt = false;

    public bool isPlayer
    {
        get
        {
            return Creature.Player == this;
        }
    }





    void Awake()
    {
        Graphic = GetComponent<Graphic>();
        Locomotor = GetComponent<Locomotor>();
        Animator = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();
        GetBrain();
    }

    public void GetBrain()
    {
        Brain = GetComponent<Brain>();
        if (Brain == null) Debug.LogError("Brain not found for " + gameObject.name);

        if (GetComponents<Brain>().Length > 1)
            Debug.LogError(name + " has too many Brains!");

        if (Brain is PlayerInput)
        {
            Player = this;
        }
    }

    void Start()
    {
        if (!isPlayer)
            InvokeRepeating("EfficientUpdate", RefreshFrequency, RefreshFrequency);

        health = maxhealth;
    }

    void Update()
    {
        if (isHurt)
        {
            HurtTime(Time.deltaTime);
        }
        else
        {
            if (isPlayer)
            {
                Brain.GetInput();
                CooldownTime(Time.deltaTime);
            }
        }
    }

    void EfficientUpdate()
    {
        if (isHurt)
        {
            HurtTime(RefreshFrequency);
        }
        else
        {
            Brain.GetInput();
            CooldownTime(RefreshFrequency);
        }
    }

    void HurtTime(float zTimeElapsed)
    {
        if (Cooldown > 0)
        {
            if (Cooldown > zTimeElapsed)
                Cooldown -= zTimeElapsed;
            else
            {
                Cooldown = 0;
                isHurt = false;
                Graphic.SpriteRenderer.color = Color.white;
            }
        }
    }

    void CooldownTime(float zQuantity)
    {
        if (Cooldown > 0)
        {
            if (Cooldown > zQuantity)
                Cooldown -= zQuantity;
            else
                Cooldown = 0;
        }
    }

    void OnTriggerEnter(Collider zOther)
    {
        Hitbox hitbox = zOther.GetComponent<Hitbox>();
        if (hitbox == null)
            return;

        if (HitboxAffectsMe(hitbox))
        {
            InflictDamage(hitbox.Damage, hitbox);
        }
    }

    bool HitboxAffectsMe(Hitbox zHitbox)
    {
        if (isHurt)
            return false;

        if (isPlayer)
        {
            return zHitbox.Owner != this;
        }
        else
        {
            return zHitbox.Owner == Creature.Player;
        }
    }

    void InflictDamage(int zQuantity, Hitbox zSource = null)
    {
        health = Mathf.Clamp(health - zQuantity, 0, int.MaxValue);
        Debug.Log(name + " loses " + zQuantity + " health  (" + health + "/" + maxhealth + ")");
        if (health < 1)
        {
            Die(zSource);
        }
        else
        {
            isHurt = true;
            Cooldown = HurtingTime;
            Graphic.SpriteRenderer.color = Color.red;
        }
    }

    void Die(Hitbox zSource = null)
    {
        if (isPlayer)
        {
            if (zSource != null && IngameUI.IsPlayerAtFullIncarnationEnergy)
            {
                if (HaveIBeenKilledByTheRightCreature(zSource.Owner))
                {
                    GameManager.Instance.StartCoroutine(GameManager.Instance.ProceedToIncarnate(this, zSource.Owner));
                    return;
                }
            }

            GameManager.Instance.StartCoroutine(GameManager.Instance.GameOver());
        }
        else
        {
            EffectsWhenDestroyed();
            Destroy(gameObject);
        }
    }

    public void EffectsWhenDestroyed()
    {
        //todo: effects

    }

    void OnMouseEnter()
    {
        if (!isPlayer)
        {
            CreatureInteractionSuggestion(this);
        }
    }

    void OnMouseExit()
    {
        GamePointer.Instance.Text.text = "";
    }

    public static void CreatureInteractionSuggestion(Creature zOtherCreature)
    {
        string suggestion = "";

        Interactions.Outcomes outcome = Interactions.GetOutcome(Player.CreatureType, zOtherCreature.CreatureType);

        switch (outcome)
        {
            case Interactions.Outcomes.CanAttack:
                if (Player.CreatureType == CREATURES.Hunter)
                    suggestion = "Shoot";
                else
                    suggestion = "Attack";

                break;

            default:
                suggestion = "<color=gray>Move to " + zOtherCreature.name + "</color>";
                break;
        }

        GamePointer.Instance.Text.text = suggestion;
    }

    public bool HaveIBeenKilledByTheRightCreature(Creature zCreatureThatKilledYou)
    {
        Creature.CREATURES nextCreature = IngameUI.Instance.TotemManager.NextCreatureToEmbody();
        return nextCreature != CREATURES.END && zCreatureThatKilledYou.CreatureType == nextCreature;
    }


}
