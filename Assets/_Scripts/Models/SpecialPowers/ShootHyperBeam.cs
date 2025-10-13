using UnityEngine;

/// <summary>
/// Special Power that shoots a HyperBeam.
/// Inherits from <see cref="SpecialPower"/>.
/// </summary>
public class ShootHyperBeam : SpecialPower  // INHERITANCE
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
