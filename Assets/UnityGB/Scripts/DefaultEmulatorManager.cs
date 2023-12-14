using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.InputSystem;
using UnityGB;

public class DefaultEmulatorManager : MonoBehaviour
{
	public string Filename;
	public Renderer ScreenRenderer;

	public string sortingLayerName = string.Empty; //initialization before the methods
	public int orderInLayer = 0;

	public MenuInput inputs;
	public float xValue;
	public float yValue;

	public EmulatorBase Emulator
	{
		get;
		private set;
	}

	private Dictionary<KeyCode, EmulatorBase.Button> _keyMapping;
	void SetSortingLayer()
	{
		if (sortingLayerName != string.Empty)
		{
			ScreenRenderer.sortingLayerName = sortingLayerName;
			ScreenRenderer.sortingOrder = orderInLayer;
		}
	}

	// Use this for initialization
	void Start()
	{
		inputs = new MenuInput();
		inputs.Enable();
		inputs.UI.Navigate.performed += Movement;
		inputs.UI.Navigate.canceled += StopMovement;
        inputs.UI.Submit.performed += Fire_performed;
		inputs.UI.Submit.canceled += Fire_canceled;

		// Init Keyboard mapping
		_keyMapping = new Dictionary<KeyCode, EmulatorBase.Button>();
		_keyMapping.Add(KeyCode.UpArrow, EmulatorBase.Button.Up);
		_keyMapping.Add(KeyCode.DownArrow, EmulatorBase.Button.Down);
		_keyMapping.Add(KeyCode.LeftArrow, EmulatorBase.Button.Left);
		_keyMapping.Add(KeyCode.RightArrow, EmulatorBase.Button.Right);
		_keyMapping.Add(KeyCode.Z, EmulatorBase.Button.A);
		_keyMapping.Add(KeyCode.X, EmulatorBase.Button.B);
		_keyMapping.Add(KeyCode.Space, EmulatorBase.Button.Start);
		_keyMapping.Add(KeyCode.LeftShift, EmulatorBase.Button.Select);




		// Load emulator
		IVideoOutput drawable = new DefaultVideoOutput();
		IAudioOutput audio = GetComponent<DefaultAudioOutput>();
		ISaveMemory saveMemory = new DefaultSaveMemory();
		Emulator = new Emulator(drawable, audio, saveMemory);
		ScreenRenderer.material.mainTexture = ((DefaultVideoOutput) Emulator.Video).Texture;
		SetSortingLayer();


		gameObject.GetComponent<AudioSource>().enabled = false;
		StartCoroutine(LoadRom(Filename));
	}

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
		Emulator.SetInput(EmulatorBase.Button.A, false);
	}

	private void Fire_performed(InputAction.CallbackContext obj)
    {
		Emulator.SetInput(EmulatorBase.Button.A, true);
	}

	private void StopMovement(InputAction.CallbackContext obj)
    {
		Emulator.SetInput(EmulatorBase.Button.Left, false);
		Emulator.SetInput(EmulatorBase.Button.Right, false);
	}

    private void Movement(InputAction.CallbackContext obj)
    {
		xValue = obj.ReadValue<Vector2>().x;
		yValue = obj.ReadValue<Vector2>().y;

		if(xValue > 0)
        {
			Emulator.SetInput(EmulatorBase.Button.Right, true);
		}else if(xValue < 0)
        {
			Emulator.SetInput(EmulatorBase.Button.Left, true);
        }
        else
        {
			Emulator.SetInput(EmulatorBase.Button.Left, false);
			Emulator.SetInput(EmulatorBase.Button.Right, false);
		}
	}

	void Update()
	{
		// Input
		/*foreach (KeyValuePair<KeyCode, EmulatorBase.Button> entry in _keyMapping)
		{
			if (Input.GetKeyDown(entry.Key))
				Emulator.SetInput(entry.Value, true);
			else if (Input.GetKeyUp(entry.Key))
				Emulator.SetInput(entry.Value, false);
		}
		*/
		/*if (Input.GetKeyDown(KeyCode.T))
		{
			byte[] screenshot = ((DefaultVideoOutput) Emulator.Video).Texture.EncodeToPNG();
			File.WriteAllBytes("./screenshot.png", screenshot);
			Debug.Log("Screenshot saved.");
		}
		*/
	}



	private IEnumerator LoadRom(string filename)
	{
		string path = System.IO.Path.Combine(Application.streamingAssetsPath, filename);
		Debug.Log("Loading ROM from " + path + ".");

		if (!File.Exists (path)) {
			Debug.LogError("File couldn't be found.");
			yield break;
		}

		WWW www = new WWW("file://" + path);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			Emulator.LoadRom(www.bytes);
			StartCoroutine(Run());
		} else
			Debug.LogError("Error during loading the ROM.\n" + www.error);
	}

	private IEnumerator Run()
	{
		gameObject.GetComponent<AudioSource>().enabled = true;
		while (true)
		{
			// Run
			Emulator.RunNextStep();
			Emulator.RunNextStep();

			yield return null;
		}
	}
}
