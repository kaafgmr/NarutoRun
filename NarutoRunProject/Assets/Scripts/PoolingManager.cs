using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PooledItems //Class to identify object list
{
    public string Name; //List name
    public GameObject objectToPool; //List object
    public int amount; //Amounbt of objects in the list
}

public class PoolingManager : MonoBehaviour
{
    public static List<GameObject> TotalObjects;

    public static PoolingManager Instance;

    [SerializeField]
    private List<PooledItems> pooledLists = new List<PooledItems>();//Object list

    [SerializeField]
    private Dictionary<string, List<GameObject>> _items = new Dictionary<string, List<GameObject>>();//Dictionary that stores all the lists

    void Awake()
    {
        Instance = this;

        TotalObjects = new List<GameObject>();

        for (int i = 0; i < pooledLists.Count; i++) //For each object list
        {
            PooledItems l = pooledLists[i];
            _items.Add(l.Name, new List<GameObject>()); //We create an entry
                                                        //in the dictionary
            for (int j = 0; j < l.amount; j++)        //and add the copies
            {
                GameObject tmp;
                tmp = Instantiate(l.objectToPool); //Creates copies
                tmp.SetActive(false); //desacvates it
                _items[l.Name].Add(tmp); //it gets added to the list
                TotalObjects.Add(tmp);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        List<GameObject> objs = _items[name];

        for (int i = 0; i < objs.Count; i++)
        {
            if (!objs[i].activeInHierarchy)
            {
                objs[i].SetActive(true);
                return objs[i];
            }
        }

        GameObject newInstance = Instantiate(objs[0].gameObject);
        newInstance.SetActive(true);
        objs.Add(newInstance);
        TotalObjects.Add(newInstance);

        return newInstance;
    }

    public void DisableAllObjects()
    {
        for (int i = 0; i < TotalObjects.Count; i++)
        {
            TotalObjects[i].SetActive(false);
        }
    }
}