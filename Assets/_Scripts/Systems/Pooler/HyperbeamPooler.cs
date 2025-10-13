using UnityEngine;

/// <summary>
/// For Hyperbeam Pooling.
/// Inherits from <see cref="ObjectPooler"/>.
/// </summary>
public class HyperbeamPooler : ObjectPooler // INHERITANCE
{
    public static HyperbeamPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
