using UnityEngine;

// INHERITANCE
public class Destoyer : MediumShip
{
    [SerializeField]
    private MissileLauncher[] missileLaunchers = new MissileLauncher[3];


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 10;
        speed = 10;
        angleVelocity = new(0, 30, 0);
        tiltAngleMax = 30f;
        maxArmor = 3;
        armor = 3;
        maxShield = 1;
        shield = 1;
        cooldownShield = 3;
        mass = 150;
        base.Start();
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        foreach (var missileLauncher in missileLaunchers)
        {
            missileLauncher.Shoot();
        }
    }
}
