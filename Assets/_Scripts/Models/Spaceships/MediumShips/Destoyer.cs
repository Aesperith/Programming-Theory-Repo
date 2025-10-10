using UnityEngine;

// INHERITANCE
public class Destoyer : MediumShip
{
    [SerializeField]
    private MissileLauncher[] missileLaunchers = new MissileLauncher[3];


    private void Start()
    {
        healthPoint = 10;
        speed = 10;
        angleVelocity = new(0, 30, 0);
        tiltAngleMax = 30f;
        price = 750;
        scorePoint = 50;
        maxArmor = 3;
        armor = 3;
        maxShield = 1;
        shield = 1;
        cooldownShield = 3;
        mass = 150;
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
