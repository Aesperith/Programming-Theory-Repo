using UnityEngine;

/// <summary>
/// Bomber Spaceship.
/// Inherits from <see cref="SmallShip"/>.
/// </summary>
public class Bomber : SmallShip // INHERITANCE
{
    private static int price = 100;
    // ENCAPSULATION
    public static int Price { get { return price; } }

    [SerializeField]
    private MissileLauncher missileLauncher;


    private void Start()
    {
        healthPoint = 2;
        speed = 2f;
        angleVelocity = new(0, 60, 0);
        tiltAngleMax = 45f;
        scorePoint = 5;
        mass = 10;
        rb.mass = mass;

        UpdateHpUI();
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        missileLauncher.Shoot();
    }

    // POLYMORPHISM
    public override void Activate1()
    {
        //No special power
    }
}
