using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Missile Launcher.
/// Inherits from <see cref="Weapon"/>.
/// </summary>
public class MissileLauncher : Weapon   // INHERITANCE
{
    [SerializeField]
    private int munitionMax = 3;
    private int munition;

    // ENCAPSULATION
    public int CurrentAmmo
    {
        get { return munition; }
    }

    public UnityEvent munitionValueChanged;

    private Missile missile;

    private float elapsedTime;
    private float cooldownReload;
    
    private bool isActive;
    private Coroutine reloading;


    // POLYMORPHISM
    protected override void Awake()
    {
        damage = 2;
        cooldownTime = 1f;
        munition = munitionMax;
        cooldownReload = 5f;

        base.Awake();
    }

    private void Start()
    {
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

                shootAudioSource.Play();

                cooldownUntilNextShot = Time.time + cooldownTime;

                munition--;
                munitionValueChanged.Invoke();
            }
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
                    munitionValueChanged.Invoke();
                    elapsedTime = 0f;
                }
            }

            yield return null;
        }
    }
}
