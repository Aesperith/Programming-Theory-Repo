using UnityEditor.Experimental.GraphView;
using UnityEngine;

// INHERITANCE
public class Fighter : SmallShip
{
    [SerializeField]
    private LaserGun laserGun;


    private void Start()
    {
        healthPoint = 1;
        speed = 3;
        angleVelocity = new(0, 60, 0);
        tiltAngleMax = 45f;
        price = 100;
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
