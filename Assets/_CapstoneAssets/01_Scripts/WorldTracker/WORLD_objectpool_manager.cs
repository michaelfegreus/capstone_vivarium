using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class WORLD_objectpool_manager : MonoBehaviour
{
    /// <summary>
    ///  This code thanks to the Brackeys channel
    /// </summary>

    [System.Serializable]
    public class Pool
    {
        public EventObjectType tag;
        public GameObject prefab;
        public int size;
    }

    // Types of Object pool tags
    public enum EventObjectType { NPC, Item };

    public List<Pool> pools;
    public Dictionary<EventObjectType, Queue<GameObject>> poolDictionary;

    // Objects should start in the pool parent, then get removed by flowcharts as they use them.
    GameObject poolParent;

    void Start()
    {
        poolDictionary = new Dictionary<EventObjectType, Queue<GameObject>>();
        poolParent = new GameObject("ObjectPoolParent");

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, poolParent.transform);
                //SetActive(false); // Should set the prefabs as de-active in advance.
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    //public void SpawnEventItem(EventObjectType _eventObjType, Vector3 _position, GaneTime _eventEndTime, bool _nonInteractable = false, AnimatorOverrideController _animatorOverride = null, string _name = "")

    public void SpawnEventNPC(Transform parentFlowchartTransform, Vector3 _position, /* string _yarnNode,*/ bool _nonInteractable, AnimatorOverrideController _animatorOverride, GameTime _eventEndTime, string _NPCName, BlockReference _interactBlock, bool _disableDespawn = false /*bool _exitOnInteraction = false, EVENT_npc.ExitDelegate _exitDelegate = null*/)
    {
        GameObject objectToSpawn = poolDictionary[EventObjectType.NPC].Dequeue();
        objectToSpawn.transform.parent = parentFlowchartTransform;
        EVENT_npc npcScript = objectToSpawn.GetComponent<EVENT_npc>();

        objectToSpawn.transform.position = _position;

        if (_NPCName != "")
        {
            npcScript.NPCName = _NPCName;
        }
        else
        {
            npcScript.NPCName = "NPC";
        }

        //npcScript.yarnNode = _yarnNode;
        npcScript.nonInteractable = _nonInteractable;
        npcScript.animatorOverride = _animatorOverride;
        npcScript.eventEndTime = _eventEndTime;
        npcScript.ignoreEventEndTime = _disableDespawn;
        npcScript.interactBlock = _interactBlock;
        //npcScript.exitOnInteraction = _exitOnInteraction;
        //npcScript.exitDelegate = _exitDelegate;

        // Make sure to set active at the end so it can set everythihng up first.
        objectToSpawn.SetActive(true);

    }

    public void SpawnEventItem(Transform parentFlowchartTransform, Vector3 _position, Item _itemType, GameTime _eventEndTime, bool _disableDespawn = false, EVENT_item.ItemPickupDelegate _itemDelegate = null)
    {
        GameObject objectToSpawn = poolDictionary[EventObjectType.Item].Dequeue();
        objectToSpawn.transform.parent = parentFlowchartTransform;
        EVENT_item itemScript = objectToSpawn.GetComponent<EVENT_item>();

        objectToSpawn.transform.position = _position;
        itemScript.itemType = _itemType;
        itemScript.eventEndTime = _eventEndTime;
        itemScript.ignoreEventEndTime = _disableDespawn;
        itemScript.itemDelegate = _itemDelegate;

        objectToSpawn.SetActive(true);
    }

    /// <summary>
    /// Adds an existing game object to an affiliated to queue.
    /// Meant to be used to reconnect objects from the pool that have since been disabled.
    /// </summary>
    public void AddToQueue(GameObject eventObj, EventObjectType objTag)
    {
        poolDictionary[objTag].Enqueue(eventObj);

        // Return Game Objects to the pool parent.
        //eventObj.transform.parent = poolParent.transform;
    }

    /// <summary>
    /// Return Game Objects to the pool parent.
    /// This extra step has to be taken because an object can't be disabled and have its parent set on the same frame for whatever reason.
    /// </summary>
    public void ReattachEventObject(GameObject eventObj)
    {
        // Return Game Objects to the pool parent.
        eventObj.transform.parent = poolParent.transform;
    }


    /* Based on Brackeys method
    public void SpawnFromPool(EventObjectType objTag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning("Pool with tag " + objTag + " doesn't exist.");
            return;
        }

        GameObject objectToSpawn = poolDictionary[objTag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Add it back to the queue
        poolDictionary[objTag].Enqueue(objectToSpawn);
    }*/
}
