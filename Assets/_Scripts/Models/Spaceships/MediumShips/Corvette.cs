using UnityEngine;

// INHERITANCE
public class Corvette : MediumShip
{
    [SerializeField]
    private LaserGun[] laserGuns = new LaserGun[3];


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 20;
        speed = 10;
        angleVelocity = new(0, 30, 0);
        tiltAngleMax = 30f;
        maxArmor = 1;
        armor = 1;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 100;
        base.Start();
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        foreach (var laserGun in laserGuns)
        {
            laserGun.Shoot();
        }
    }
}
