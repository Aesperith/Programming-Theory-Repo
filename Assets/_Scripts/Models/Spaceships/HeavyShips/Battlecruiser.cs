using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Battlecruiser Spaceship.
/// Inherits from <see cref="HeavyShip"/>.
/// </summary>
public class Battlecruiser : HeavyShip  // INHERITANCE
{
    private static int price = 2000;
    // ENCAPSULATION
    public static int Price { get { return price; } }

    [SerializeField]
    private List<Weapon> weapons;


    private void Start()
    {
        healthPoint = 100;
        speed = 200;
        angleVelocity = new(0, 15, 0);
        tiltAngleMax = 20f;
        scorePoint = 200;
        maxArmor = 3;
        armor = 3;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 2000;
        rb.mass = mass;

        UpdateHpUI();
        UpdateArmorUI();
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        foreach (var weapon in weapons)
        {
            weapon.Shoot();
        }
    }

    // POLYMORPHISM
    public override void Activate1()
    {
        specialPower.Activate();
    }
}
