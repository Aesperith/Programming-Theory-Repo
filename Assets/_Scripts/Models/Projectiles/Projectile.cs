using UnityEngine;

/// <summary>
/// Base class for Projectile.
/// </summary>
public class Projectile : MonoBehaviour
{
    public string source;

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

    protected float speed;

    // ENCAPSULATION
    public float Speed
    {
        get { return speed; }
    }
}
