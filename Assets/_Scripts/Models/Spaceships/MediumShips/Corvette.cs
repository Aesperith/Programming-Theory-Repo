using UnityEngine;

/// <summary>
/// Corvette Spaceship.
/// Inherits from <see cref="MediumShip"/>.
/// </summary>
public class Corvette : MediumShip  // INHERITANCE
{
    private static int price = 250;
    public static int Price { get { return price; } }

    [SerializeField]
    private LaserGun[] laserGuns = new LaserGun[3];


    private void Start()
    {
        healthPoint = 20;
        speed = 25;
        angleVelocity = new(0, 30, 0);
        tiltAngleMax = 30f;
        scorePoint = 25;
        maxArmor = 1;
        armor = 1;
        maxShield = 3;
        shield = 3;
        cooldownShield = 3;
        mass = 100;
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
        //No special power
    }
}
