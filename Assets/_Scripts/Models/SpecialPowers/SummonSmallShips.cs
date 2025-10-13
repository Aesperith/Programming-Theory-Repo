using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Special Power that summons up to 6 <see cref="Fighter"/>.
/// Inherits from <see cref="SpecialPower"/>.
/// </summary>
public class SummonSmallShips : SpecialPower    // INHERITANCE
{
    [SerializeField]
    private GameObject port1;

    [SerializeField]
    private GameObject port2;

    private const int amountToSummoned = 6;
    private const float catapultForce = 50f;

    private void Start()
    {
        cooldownTime = 30f;
    }

    // POLYMORPHISM
    public override void Activate()
    {
        if (cooldownUntilNextActivation < Time.time)
        {
            for (int i = 0; i < amountToSummoned; i++)
            {
                // Get an object object from the pool
                GameObject pooledFighter;
                if (this.CompareTag("Enemy"))
                {
                    pooledFighter = EnemyFighterPooler.SharedInstance
                        .GetPooledObject();
                }
                else
                {
                    pooledFighter = PlayerFighterPooler.SharedInstance
                        .GetPooledObject();
                }

                if (pooledFighter != null)
                {
                    pooledFighter.SetActive(true); // activate it

                    GameObject port = Random.value < 0.5f ? port1 : port2;

                    pooledFighter.transform.SetPositionAndRotation
                        (port.transform.position, port.transform.rotation);

                    pooledFighter.GetComponent<Rigidbody>().AddForce
                        (catapultForce * port.transform.forward, ForceMode.Impulse);
                }
            }
            cooldownUntilNextActivation = Time.time + cooldownTime;
        }
    }
}
