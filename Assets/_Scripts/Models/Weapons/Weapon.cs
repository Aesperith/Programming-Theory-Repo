using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int damage;

    // ENCAPSULATION
    public int Damage
    {
        get { return damage; }
        set 
        {
            if (value >= 0)
            {
                damage = value;
            }            
        }
    }

    protected float cooldownTime;

    // ENCAPSULATION
    public float Cooldown
    {
        get { return cooldownTime; }
        set
        {
            if (value >= 0)
            {
                cooldownTime = value;
            }
        }
    }

    protected float cooldownUntilNextShot;

    public abstract void Shoot();
}
