using UnityEngine;

/// <summary>
/// For Enemy Fighter Pooling.
/// Inherits from <see cref="ObjectPooler"/>.
/// </summary>
public class EnemyFighterPooler : ObjectPooler  // INHERITANCE
{
    public static EnemyFighterPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
