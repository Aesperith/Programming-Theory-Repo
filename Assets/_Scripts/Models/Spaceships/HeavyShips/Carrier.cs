using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Carrier : HeavyShip
{
    [SerializeField]
    private List<LaserGun> laserGuns;


    private void Start()
    {
        healthPoint = 50;
        speed = 100;
        angleVelocity = new(0, 15, 0);
        tiltAngleMax = 20f;
        price = 1000;
        scorePoint = 100;
        maxArmor = 1;
        armor = 1;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 1500;
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

    // POLYMORPHISM
    public override void Activate1()
    {
        specialPower.Activate();
    }
}
