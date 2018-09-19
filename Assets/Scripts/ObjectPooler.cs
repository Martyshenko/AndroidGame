﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPoolItem
{

    public GameObject objectToPool;

    public bool shouldExpand = true;

    public int amountToPool;



}
public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;

    public List<ObjectPoolItem> itemsToPool;

    public List<GameObject> pooledObjects;


    private void Awake()
    {
        SharedInstance = this;
    }

    // Use this for initialization
    void Start()
    {

        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;

    }
}