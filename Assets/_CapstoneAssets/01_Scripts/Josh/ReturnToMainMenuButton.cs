using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync("map_prototype_TG_6");

        Queue objectsToDestroy = new Queue();

        // then find and destroy all gameobjects
        foreach (var gobj in FindObjectsOfType<GameObject>())
        {
            objectsToDestroy.Enqueue(gobj);
        }

        // then dequeue and killthem
        for (int i = 0; i < objectsToDestroy.Count; i++)
        {
            GameObject go = objectsToDestroy.Dequeue() as GameObject;
            Destroy(go);
        }

        Destroy(GameObject.Find("Dialogue Manager"));

        // load the main menu scene
        SceneManager.LoadSceneAsync("pitch_TitleScreen_TG", LoadSceneMode.Single);
    }
}
