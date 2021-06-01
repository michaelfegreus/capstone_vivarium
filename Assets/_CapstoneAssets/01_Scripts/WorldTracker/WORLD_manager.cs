using UnityEngine;

[RequireComponent(typeof(GAME_clock_manager))]
[RequireComponent(typeof(WORLD_objectpool_manager))]
public class WORLD_manager : Singleton<WORLD_manager>
{
    // Set up components
    [System.NonSerialized]
    public GAME_clock_manager timeManager;
    [System.NonSerialized]
    public WORLD_objectpool_manager objectPoolManager;

    // Start is called before the first frame update
    void Awake()
    {
        timeManager = GetComponent<GAME_clock_manager>();
        objectPoolManager = GetComponent<WORLD_objectpool_manager>();
    }
}
