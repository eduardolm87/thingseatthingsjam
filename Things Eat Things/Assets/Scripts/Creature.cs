using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public enum CREATURES { TinyLight, Rabbit, Wolf, Hunter };

    public static Creature Player;

    public const float HurtingTime = 0.5f;

    const float RefreshFrequency = 0.25f;

    int health = 5;
    public int maxhealth = 5;
    public float speed = 1;
    public float detectionDistance = 3;
    public float attackDistance = 1;
    public float cooldownAfterAttack;
    public Locomotor.MovementTypes MovementType = Locomotor.MovementTypes.WALK;
    public CREATURES CreatureType = CREATURES.Rabbit;
    public string DisplayName = "???";

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
        Brain = GetComponent<Brain>();
        Animator = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();

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
            return;
        }

        if (isPlayer)
        {
            Brain.GetInput();
            CooldownTime(Time.deltaTime);
        }
    }

    void EfficientUpdate()
    {
        if (isHurt)
        {
            HurtTime(RefreshFrequency);
            return;
        }

        Brain.GetInput();
        CooldownTime(RefreshFrequency);
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
            InflictDamage(hitbox.Damage);
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

    void InflictDamage(int zQuantity)
    {
        health = Mathf.Clamp(health - zQuantity, 0, int.MaxValue);
        Debug.Log(name + " loses " + zQuantity + " health  (" + health + "/" + maxhealth + ")");
        if (health < 1)
        {
            Die();
        }
        else
        {
            isHurt = true;
            Cooldown = HurtingTime;
            Graphic.SpriteRenderer.color = Color.red;
        }
    }

    void Die()
    {
        if (isPlayer)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.GameOver());
        }
        else
        {
            //todo: effects
            Destroy(gameObject);
        }
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

        switch (Player.CreatureType)
        {
            case CREATURES.Wolf:
                switch (zOtherCreature.CreatureType)
                {
                    case CREATURES.Rabbit:
                    case CREATURES.Hunter:
                        suggestion = "Attack";
                        break;
                }

                break;

            case CREATURES.Hunter:
                switch (zOtherCreature.CreatureType)
                {
                    case CREATURES.Rabbit:
                    case CREATURES.Wolf:
                    case CREATURES.Hunter:
                        suggestion = "Shoot";
                        break;
                }

                break;

            default:
                suggestion = "<color=gray>Move to " + zOtherCreature.name + "</color>";
                break;
        }

        GamePointer.Instance.Text.text = suggestion;
    }
}
