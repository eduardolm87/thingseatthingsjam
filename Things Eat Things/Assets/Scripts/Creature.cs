using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour
{
    public static Creature Player;
    public bool isPlayer = false;
    const float RefreshFrequency = 0.25f;

    int health = 5;
    public int maxhealth = 5;
    public float speed = 1;
    public float detectionDistance = 3;
    public float attackDistance = 1;
    public float cooldownAfterAttack;

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

    void Awake()
    {
        Graphic = GetComponent<Graphic>();
        Locomotor = GetComponent<Locomotor>();
        Brain = GetComponent<Brain>();
        Animator = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();

        detectionDistance = 20f;
        //distanceToPlayer = Vector3.Distance(Creature.Player.transform.position, this.transform.position);

        if (Brain == null) Debug.LogError("Brain not found for " + gameObject.name);

        if (GetComponents<Brain>().Length > 1)
            Debug.LogError(name + " has too many Brains!");

        if (Brain is PlayerInput)
        {
            Player = this;
            isPlayer = true;
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
        if (isPlayer)
        {
            Brain.GetInput();
            CooldownTime(Time.deltaTime);
        }
    }

    void EfficientUpdate()
    {
        Brain.GetInput();
        CooldownTime(RefreshFrequency);
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
        else
            Debug.Log(name + ":  hb no aff");
    }

    bool HitboxAffectsMe(Hitbox zHitbox)
    {
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
}
