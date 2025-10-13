using UnityEngine;

/// <summary>
/// For Missile Pooling.
/// Inherits from <see cref="ObjectPooler"/>.
/// </summary>
public class MissilePooler : ObjectPooler   // INHERITANCE
{
    public static MissilePooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
