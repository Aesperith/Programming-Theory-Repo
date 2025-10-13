using UnityEngine;

/// <summary>
/// Base class for Special Power.
/// </summary>
public abstract class SpecialPower : MonoBehaviour
{
    protected float cooldownTime;

    // ENCAPSULATION
    public virtual float Cooldown
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

    protected float cooldownUntilNextActivation;

    public abstract void Activate();
}
