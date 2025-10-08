using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    void Start()
    {
        // Loop through list of pooled objects,
        // deactivating them and adding them to the list
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(transform); // set as children of Spawn Manager
        }
    }

    /// <summary>
    /// Return the pooled object if not active.
    /// Return null if no object available.
    /// </summary>
    /// <returns>GameObject or null.</returns>
    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }
}
