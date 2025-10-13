using UnityEngine;

/// <summary>
/// Base class for HeavyShip.
/// Inherits from <see cref="MediumShip"/>.
/// </summary>
public abstract class HeavyShip : MediumShip    // INHERITANCE
{
    [SerializeReference]
    protected SpecialPower specialPower;
}
