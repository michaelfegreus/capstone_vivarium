using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsSwitcher : MonoBehaviour
{
    private Gamepad currentGamepad;
    public bool gamepadPluggedIn = false;

    [SerializeField] private GameObject[] gamepadIcons;

    [SerializeField] private GameObject[] keyboardIcons;

    void Update()
    {
		currentGamepad = Gamepad.current;

		if (currentGamepad != null && gamepadPluggedIn == false)
		{
			gamepadPluggedIn = true;
			Debug.Log("Gamepad detected");
			foreach (GameObject i in gamepadIcons)
			{
				i.SetActive(true);
			}
			foreach (GameObject b in keyboardIcons)
			{
				b.SetActive(false);
			}

		}
		else if (currentGamepad == null & gamepadPluggedIn == true)
		{
			gamepadPluggedIn = false;
			Debug.Log("No gamepad detected");
			foreach (GameObject i in gamepadIcons)
			{
				i.SetActive(false);
			}
			foreach (GameObject b in keyboardIcons)
			{
				b.SetActive(true);
			}
		}
	}
}
