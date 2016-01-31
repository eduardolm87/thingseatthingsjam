using UnityEngine;
using System.Collections;

public class PlayerInput : Brain
{
    void Start()
    {
        Globals.Init();
    }

    void Update()
    {
        Globals.gCamera.GetComponent<GameCam>().LookAtThis(transform.position);
    }

    public override void GetInput()
    {
        if (Creature.Locomotor.InAir)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Globals.gCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                switch (hit.collider.gameObject.layer)
                {
                    case Globals.kGroundLayer:
                        ClickedGround(hit.point);
                        break;

                    case Globals.kCreaturesLayer:
                        ClickedCreature(hit.collider.gameObject);
                        break;

                    case Globals.kSceneryLayer:
                        ClickedScenery(hit.collider.gameObject);
                        break;

                    default:
                        //Debug.Log("Clicked something unexpected: " + hit.collider.name);
                        break;
                }
            }
        }
    }




    void ClickedCreature(GameObject creature)
    {
        //todo: Interact with the creature if the creature is able to be interacted by you. If not, just move towards it
        Creature otherCreature = creature.GetComponent<Creature>();
        if (otherCreature != null)
        {
            if (Creature.isPlayer && !otherCreature.isPlayer)
            {
                Interactions.Outcomes outcome = Interactions.GetOutcome(Creature.CreatureType, otherCreature.CreatureType);
                if (outcome == Interactions.Outcomes.CanAttack)
                {
                    TryToAttackCreature(otherCreature);
                }
            }
        }

        Creature.Locomotor.TargetPosition = creature.transform.position;
        GamePointer.Instance.Text.text = "";
    }


    void ClickedScenery(GameObject scenery)
    {
        //...
    }

    void ClickedGround(Vector3 position)
    {
        if (isValidTerrainPoint(position))
            MoveToPoint(position);
    }

    bool isValidTerrainPoint(Vector3 zPosition)
    {
        //todo: differentiate whether you have clicked a valid position
        return true;
    }


    void MoveToPoint(Vector3 position)
    {
        Creature.Locomotor.TargetPosition = position;
        GameManager.Instance.Gotopointer.Show(position + (Vector3.up * 0.1f));
    }

    void TryToAttackCreature(Creature zCreature)
    {
        if (Creature.Cooldown <= 0 && Vector3.Distance(transform.position, zCreature.transform.position) < Creature.attackDistance)
        {
            switch (Creature.CreatureType)
            {
                case global::Creature.CREATURES.Hunter:
                    Shoot(zCreature);
                    break;
                default:
                    Melee(zCreature);
                    break;
            }
        }
        else
        {
            MoveToPoint(zCreature.transform.position);
        }
    }


    void Melee(Creature zCreature)
    {
        Creature.Locomotor.Stop();

        Vector3 attackDirection = zCreature.transform.position - transform.position;
        Hitbox.Shoot(Creature, transform.position, attackDirection, 3, Creature.attackDamage, 1, true); //todo: make this variable
        Creature.Cooldown = Creature.cooldownAfterAttack;
    }

    void Shoot(Creature zCreature)
    {
        Creature.Locomotor.Stop();

        Vector3 attackDirection = zCreature.transform.position - transform.position;

        HunterShootBullet(attackDirection, 4); //hacks!: we should really look into an enemy database to get the attack speed

        Creature.Cooldown = Creature.cooldownAfterAttack;
    }


    void HunterShootBullet(Vector3 zDirection, float zSpeed)
    {
        GameObject hitboxObj = Instantiate(GameManager.Instance.HunterBulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        Hitbox newHitbox = hitboxObj.GetComponent<Hitbox>();

        newHitbox.Owner = Creature;
        newHitbox.Damage = Creature.attackDamage;
        newHitbox.DisappearWhenTouchingValidTarget = true;

        newHitbox.Rigidbody.velocity = zDirection.normalized * zSpeed;

        newHitbox.Invoke("DieByTime", 3);//hacks!: we should really look into an enemy database to get the time to disappear
    }

}
