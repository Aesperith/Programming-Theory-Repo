using UnityEngine;

public class Projectile : MonoBehaviour
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
}
