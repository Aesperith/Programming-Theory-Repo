using System.Collections;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Base class for MediumShip.
/// Inherits from <see cref="Spaceship"/>.
/// </summary>
public abstract class MediumShip : Spaceship    // INHERITANCE
{
    protected int armor;
    protected int maxArmor;

    // ENCAPSULATION
    public virtual int Armor
    {
        get { return armor; }
        set 
        {
            if (value >= 0 && value <= maxArmor)
            {
                armor = value;
            }            
        }
    }

    [SerializeField]
    protected GameObject shieldPrefab;
    protected int shield;
    protected int maxShield;

    // ENCAPSULATION
    public virtual int Shield
    {
        get { return shield; }
        set
        {
            if (value >= 0 && value <= maxShield)
            {
                shield = value;
            }
        }
    }

    protected float cooldownShield;
    protected bool isRechargingShield = false;

    protected virtual void Update()
    {
        ManageShield();
    }

    protected void ManageShield()
    {
        if (shield <= 0)
        {
            shieldPrefab.SetActive(false);
        }
        else
        {
            shieldPrefab.SetActive(true);
        }

        if (shield < maxShield && !isRechargingShield)
        {
            StartCoroutine(RechargeShield());
        }
    }

    protected IEnumerator RechargeShield()
    {
        isRechargingShield = true;
        yield return new WaitForSeconds(cooldownShield);
        if (shield < maxShield)
        {            
            shield++;
        }
        isRechargingShield = false;
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (other.gameObject.TryGetComponent<Laser>(out var laser))
            {
                ManageLaserDamage(laser);

                other.gameObject.SetActive(false);
                
                CheckDeath();
            }
            else if(other.gameObject.TryGetComponent<Missile>(out var missile))
            {
                ManageMissileDamage(missile);

                other.gameObject.SetActive(false);
                
                CheckDeath();
            }
            else
            {
                base.OnTriggerEnter(other);
            }
        }
    }

    /// <summary>
    /// Manage laser damage.
    /// </summary>
    /// <param name="laser">Laser data.</param>
    protected virtual void ManageLaserDamage(Laser laser)
    {
        if (!this.CompareTag(laser.source))
        {
            if (shield > 0)
            {
                shield--;
                Debug.Log(gameObject.name + ": Shield block (remaining: "
                    + shield + ")");
            }
            else if (armor > 0)
            {
                armor--;
                Debug.Log(gameObject.name + ": Armor block (remaining: "
                    + armor + ")");
            }
            else
            {
                Debug.Log(gameObject.name + ": Take " + laser.Damage
                    + " damages");
                healthPoint -= laser.Damage;
                Debug.Log("HP: " + healthPoint);
            }
        }     
    }

    /// <summary>
    /// Manage missile damage.
    /// </summary>
    /// <param name="missile">Missile data.</param>
    protected virtual void ManageMissileDamage(Missile missile)
    {
        if (!this.CompareTag(missile.source))
        {
            if (armor > 0)
            {
                armor--;
                Debug.Log(gameObject.name + ": Armor block (remaining: "
                    + armor + ")");
            }
            else
            {
                Debug.Log(gameObject.name + ": Take " + missile.Damage
                    + " damages");
                healthPoint -= missile.Damage;
                Debug.Log(gameObject.name + ": HP: " + healthPoint);
            }            
        }
    }
}
