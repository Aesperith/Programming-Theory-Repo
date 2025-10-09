using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Carrier : HeavyShip
{
    [SerializeField]
    private List<LaserGun> laserGuns;


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 50;
        speed = 20;
        angleVelocity = new(0, 15, 0);
        tiltAngleMax = 20f;
        maxArmor = 1;
        armor = 1;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 1500;
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
