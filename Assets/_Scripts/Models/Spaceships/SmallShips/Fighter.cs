using UnityEditor.Experimental.GraphView;
using UnityEngine;

// INHERITANCE
public class Fighter : SmallShip
{
    [SerializeField]
    private LaserGun laserGun;


    // POLYMORPHISM
    protected override void Start()
    {
        healthPoint = 1;
        speed = 3;
        angleVelocity = new(0, 60, 0);
        tiltAngleMax = 45f;
        mass = 10;
        base.Start();
        rb.mass = mass;
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        laserGun.Shoot();
    }
}
