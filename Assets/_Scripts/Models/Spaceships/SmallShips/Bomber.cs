using UnityEngine;

// INHERITANCE
public class Bomber : SmallShip
{
    [SerializeField]
    private MissileLauncher missileLauncher;


    private void Start()
    {
        healthPoint = 2;
        speed = 1.5f;
        angleVelocity = new(0, 60, 0);
        tiltAngleMax = 45f;
        price = 125;
        scorePoint = 5;
        mass = 10;
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        missileLauncher.Shoot();
    }
}
