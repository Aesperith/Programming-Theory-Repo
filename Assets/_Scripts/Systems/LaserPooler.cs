using UnityEngine;

// INHERITANCE
public class LaserPooler : ObjectPooler
{
    public static LaserPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
