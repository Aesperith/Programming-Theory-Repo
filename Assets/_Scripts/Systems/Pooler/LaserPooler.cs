using UnityEngine;

/// <summary>
/// For Laser Pooling.
/// Inherits from <see cref="ObjectPooler"/>.
/// </summary>
public class LaserPooler : ObjectPooler // INHERITANCE
{
    public static LaserPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
