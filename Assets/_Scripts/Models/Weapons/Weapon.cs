using UnityEngine;

/// <summary>
/// Base class for Weapon in <see cref="Spaceship"/>.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected AudioSource shootAudioSource;

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


    private void Awake()
    {
        shootAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Shoot with the weapon.
    /// </summary>
    public abstract void Shoot();
}
