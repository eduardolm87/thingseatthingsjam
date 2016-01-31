using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    public const bool ShowHitbox = true;

    [HideInInspector]
    public Rigidbody Rigidbody;

    [HideInInspector]
    public SphereCollider Collider;

    //Properties
    [HideInInspector]
    public Creature Owner = null;
    [HideInInspector]
    public bool DisappearWhenTouchingValidTarget = true;
    [HideInInspector]
    public int Damage = 1;

    public static void Shoot(Creature zOwner, Vector3 zOrigin, Vector3 zDirection, float zSpeed, int zDamage = 1, float zTimeToDisappear = -1, bool zDisappearWhenTouchingValidTarget = true)
    {
        Hitbox newHitbox = InstantiateHitbox(zOrigin);

        newHitbox.Owner = zOwner;
        newHitbox.Damage = zDamage;
        newHitbox.DisappearWhenTouchingValidTarget = zDisappearWhenTouchingValidTarget;

        newHitbox.Rigidbody.velocity = zDirection.normalized * zSpeed;

        if (ShowHitbox)
        {
            newHitbox.GetComponent<MeshRenderer>().enabled = true;
        }

        if (zTimeToDisappear > 0)
            newHitbox.Invoke("DieByTime", zTimeToDisappear);
    }

    static Hitbox InstantiateHitbox(Vector3 zPosition)
    {
        GameObject hitboxObj = Instantiate(GameManager.Instance.HitboxPrefab.gameObject, zPosition, Quaternion.identity) as GameObject;
        return hitboxObj.GetComponent<Hitbox>();
    }

    void DieByTime()
    {
        Destroy(gameObject);
    }
}
