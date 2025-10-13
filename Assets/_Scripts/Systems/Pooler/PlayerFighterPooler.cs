using UnityEngine;

/// <summary>
/// For Player Fighter Pooling.
/// Inherits from <see cref="ObjectPooler"/>.
/// </summary>
public class PlayerFighterPooler : ObjectPooler // INHERITANCE
{
    public static PlayerFighterPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
