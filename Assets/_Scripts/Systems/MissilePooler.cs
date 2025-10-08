using UnityEngine;

// INHERITANCE
public class MissilePooler : ObjectPooler
{
    public static MissilePooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
