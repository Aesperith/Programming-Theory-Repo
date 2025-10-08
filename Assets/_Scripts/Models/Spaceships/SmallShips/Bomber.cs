using UnityEngine;

// INHERITANCE
public class Bomber : SmallShip
{
    [SerializeField]
    private MissileLauncher missileLauncher;


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 2;
        speed = 5;
        angleVelocity = new(0, 60, 0);
        mass = 10;
        base.Start();
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        missileLauncher.Shoot();
    }
}
