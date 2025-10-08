using UnityEngine;

public class LaserGun : Weapon
{
    private Laser laser;


    private void Start()
    {
        damage = 1;
        cooldownTime = 0.2f;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        // Get an object object from the pool
        GameObject pooledProjectile =
            LaserPooler.SharedInstance.GetPooledObject();

        if (pooledProjectile != null && cooldownUntilNextShot < Time.time)
        {
            laser = pooledProjectile.GetComponent<Laser>();
            if (laser != null)
            {
                laser.Damage = damage;
            }            

            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.SetPositionAndRotation
                (transform.position, transform.rotation);
            cooldownUntilNextShot = Time.time + cooldownTime;
        }
    }
}
