using UnityEngine;

// INHERITANCE
public abstract class HeavyShip : MediumShip
{
    [SerializeReference]
    protected SpecialPower specialPower;
}
