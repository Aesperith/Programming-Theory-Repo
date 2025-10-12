using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SummonSmallShips : SpecialPower
{
    private void Start()
    {
        cooldownTime = 30f;
    }

    // POLYMORPHISM
    public override void Activate()
    {
        Debug.Log("SummonSmallShips Power");
    }
}
