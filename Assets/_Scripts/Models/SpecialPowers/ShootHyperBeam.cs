using UnityEngine;

// INHERITANCE
public class ShootHyperBeam : SpecialPower
{
    [SerializeField]
    private int damage;

    private Hyperbeam hyperbeam;


    private void Start()
    {
        cooldownTime = 30f;
        damage = 100;
    }

    // POLYMORPHISM
    public override void Activate()
    {
        // Get an object object from the pool
        GameObject pooledProjectile =
            HyperbeamPooler.SharedInstance.GetPooledObject();

        if (pooledProjectile != null 
            && cooldownUntilNextActivation < Time.time)
        {
            hyperbeam = pooledProjectile.GetComponent<Hyperbeam>();
            if (hyperbeam != null)
            {
                hyperbeam.Damage = damage;
                hyperbeam.source = this.tag;
            }

            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.SetPositionAndRotation
                (transform.position, transform.rotation);
            cooldownUntilNextActivation = Time.time + cooldownTime;
        }
    }
}
