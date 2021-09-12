using UnityEngine;

[RequireComponent(typeof(GAME_clock_manager))]
public class WORLD_manager : Singleton<WORLD_manager>
{
    // Set up components
    [System.NonSerialized]
    public GAME_clock_manager timeManager;

    // Start is called before the first frame update
    void Awake()
    {
        timeManager = GetComponent<GAME_clock_manager>();
    }
}
