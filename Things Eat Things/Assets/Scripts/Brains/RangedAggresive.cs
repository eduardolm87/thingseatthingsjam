using UnityEngine;
using System.Collections;

public class RangedAggresive : Aggressive
{
    public Hitbox BulletPrefab;
    public float TimeToDisappear = 3;
    public float BulletSpeed = 2;

    protected override void Attack()
    {
        Creature.Locomotor.Stop();

        Vector3 attackDirection = (Creature.Player.transform.position - transform.position).normalized;
        ShootBullet(attackDirection, BulletSpeed);
        Creature.Cooldown = Creature.cooldownAfterAttack;
    }

    void ShootBullet(Vector3 zDirection, float zSpeed)
    {
        if (BulletPrefab == null)
        {
            Debug.LogError("You have not assigned the bullet prefab for the creature " + name);
        }

        GameObject hitboxObj = Instantiate(BulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        Hitbox newHitbox = hitboxObj.GetComponent<Hitbox>();

        newHitbox.Owner = Creature;
        newHitbox.Damage = Creature.attackDamage;
        newHitbox.DisappearWhenTouchingValidTarget = true;

        newHitbox.Rigidbody.velocity = zDirection.normalized * zSpeed;

        newHitbox.Invoke("DieByTime", TimeToDisappear);
    }
}
