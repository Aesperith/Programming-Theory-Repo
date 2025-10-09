using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Battlecruiser : HeavyShip
{
    [SerializeField]
    private List<Weapon> weapons;


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 100;
        speed = 30;
        angleVelocity = new(0, 15, 0);
        tiltAngleMax = 20f;
        maxArmor = 3;
        armor = 3;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 2000;
        base.Start();
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        foreach (var weapon in weapons)
        {
            weapon.Shoot();
        }
    }
}
