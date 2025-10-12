using UnityEngine;

public class HyperbeamPooler : ObjectPooler
{
    public static HyperbeamPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
}
