using System.Collections;
using System.Reflection;
using UnityEngine;

public class MissileLauncher : Weapon
{
    [SerializeField]
    private int munitionMax = 3;

    private Missile missile;

    private float elapsedTime;
    private float cooldownReload;
    private int munition;
    private bool isActive;
    private Coroutine reloading;


    private void Start()
    {
        damage = 2;
        cooldownTime = 1f;
        munition = munitionMax;
        cooldownReload = 5f;

        isActive = true;
        reloading = StartCoroutine(Reloading());
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        // Get an object object from the pool
        GameObject pooledProjectile =
            MissilePooler.SharedInstance.GetPooledObject();

        if (pooledProjectile != null && cooldownUntilNextShot < Time.time)
        {   
            if (munition > 0)
            {
                missile = pooledProjectile.GetComponent<Missile>();
                if (missile != null)
                {
                    missile.Damage = damage;
                    missile.source = this.tag;
                }

                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.SetPositionAndRotation
                    (transform.position, transform.rotation);
                cooldownUntilNextShot = Time.time + cooldownTime;

                munition--;
            }
            Debug.Log("Munition: " + munition);
        }
    }

    /// <summary>
    /// Coroutine for reloading munition.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reloading()
    {
        while (isActive)
        {
            if (munition < munitionMax)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= cooldownReload)
                {
                    munition++;
                    elapsedTime = 0f;
                    Debug.Log("Munition: " + munition);
                }
            }

            yield return null;
        }
    }
}
