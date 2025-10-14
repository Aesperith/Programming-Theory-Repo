using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Fighter Spaceship.
/// Inherits from <see cref="SmallShip"/>.
/// </summary>
public class Fighter : SmallShip    // INHERITANCE
{
    private static int price = 0;
    public static int Price { get { return price; } }

    [SerializeField]
    private LaserGun laserGun;


    private void Start()
    {
        healthPoint = 1;
        speed = 3;
        angleVelocity = new(0, 60, 0);
        tiltAngleMax = 45f;
        scorePoint = 1;
        mass = 10;
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        laserGun.Shoot();
    }

    // POLYMORPHISM
    public override void Activate1()
    {
        //No special power
    }
}
