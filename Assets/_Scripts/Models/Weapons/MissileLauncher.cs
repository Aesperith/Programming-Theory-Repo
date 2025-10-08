using System.Reflection;
using UnityEngine;

public class MissileLauncher : Weapon
{
    private Missile missile;


    private void Start()
    {
        damage = 2;
        cooldownTime = 1f;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        // Get an object object from the pool
        GameObject pooledProjectile =
            MissilePooler.SharedInstance.GetPooledObject();

        if (pooledProjectile != null && cooldownUntilNextShot < Time.time)
        {
            missile = pooledProjectile.GetComponent<Missile>();
            if (missile != null)
            {
                missile.Damage = damage;
            }

            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.SetPositionAndRotation
                (transform.position, transform.rotation);
            cooldownUntilNextShot = Time.time + cooldownTime;
        }
    }
}
