﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace MoreMountains.Tools
{	
	[System.Serializable]
	public class ProgressEvent : UnityEvent<float>{}
	
	/// <summary>
	/// A class to load scenes using a loading screen instead of just the default API
	/// This is a new version of the classic LoadingSceneManager (now renamed to MMSceneLoadingManager for consistency)
	/// </summary>
	public class MMAdditiveSceneLoadingManager : MonoBehaviour 
	{
		/// The possible orders in which to play fades (depends on the fade you've set in your loading screen
		public enum FadeModes { FadeInThenOut, FadeOutThenIn }

        [Header("Settings")]
        /// the ID on which to trigger a fade, has to match the ID on the fader in your scene
        [Tooltip("the ID on which to trigger a fade, has to match the ID on the fader in your scene")]
        public int FaderID = 500;
        /// whether or not to output debug messages to the console
        [Tooltip("whether or not to output debug messages to the console")]
        public bool DebugMode = false;

        [Header("Progress Events")] 
        /// an event used to update progress 
        [Tooltip("an event used to update progress")]
        public ProgressEvent SetRealtimeProgressValue;
        /// an event used to update progress with interpolation
        [Tooltip("an event used to update progress with interpolation")]
        public ProgressEvent SetInterpolatedProgressValue;

        [Header("State Events")]
        /// an event that will be invoked when the load starts
        [Tooltip("an event that will be invoked when the load starts")]
        public UnityEvent OnLoadStarted;
        /// an event that will be invoked when the delay before the entry fade starts
        [Tooltip("an event that will be invoked when the delay before the entry fade starts")]
        public UnityEvent OnBeforeEntryFade;
        /// an event that will be invoked when the entry fade starts
        [Tooltip("an event that will be invoked when the entry fade starts")]
        public UnityEvent OnEntryFade;
        /// an event that will be invoked when the delay after the entry fade starts
        [Tooltip("an event that will be invoked when the delay after the entry fade starts")]
        public UnityEvent OnAfterEntryFade;
        /// an event that will be invoked when the origin scene gets unloaded
        [Tooltip("an event that will be invoked when the origin scene gets unloaded")]
        public UnityEvent OnUnloadOriginScene;
        /// an event that will be invoked when the destination scene starts loading
        [Tooltip("an event that will be invoked when the destination scene starts loading")]
        public UnityEvent OnLoadDestinationScene;
        /// an event that will be invoked when the load of the destination scene is complete
        [Tooltip("an event that will be invoked when the load of the destination scene is complete")]
        public UnityEvent OnLoadProgressComplete;
        /// an event that will be invoked when the interpolated load of the destination scene is complete
        [Tooltip("an event that will be invoked when the interpolated load of the destination scene is complete")]
        public UnityEvent OnInterpolatedLoadProgressComplete;
        /// an event that will be invoked when the delay before the exit fade starts
        [Tooltip("an event that will be invoked when the delay before the exit fade starts")]
        public UnityEvent OnBeforeExitFade;
        /// an event that will be invoked when the exit fade starts
        [Tooltip("an event that will be invoked when the exit fade starts")]
        public UnityEvent OnExitFade;
        /// an event that will be invoked when the destination scene gets activated
        [Tooltip("an event that will be invoked when the destination scene gets activated")]
        public UnityEvent OnDestinationSceneActivation;
        /// an event that will be invoked when the scene loader gets unloaded
        [Tooltip("an event that will be invoked when the scene loader gets unloaded")]
        public UnityEvent OnUnloadSceneLoader;
        /// an event that will be invoked when the whole transition is complete
        [Tooltip("an event that will be invoked when the whole transition is complete")]
        public UnityEvent OnLoadTransitionComplete;

        protected static bool _interpolateProgress;
        protected static float _progressInterpolationSpeed;
        protected static float _beforeEntryFadeDelay;
        protected static MMTweenType _entryFadeTween;
        protected static float _entryFadeDuration;
        protected static float _afterEntryFadeDelay;
        protected static float _beforeExitFadeDelay;
        protected static MMTweenType _exitFadeTween;
        protected static float _exitFadeDuration;
        protected static FadeModes _fadeMode;
        protected static string _sceneToLoadName = "";
        protected static string _loadingScreenSceneName;
        protected static List<string> _scenesInBuild;
        protected static Scene[] _initialScenes;
        
        protected float _loadProgress = 0f;
        protected float _interpolatedLoadProgress;
        protected static bool _loadingInProgress = false;
        
        protected AsyncOperation _unloadOriginAsyncOperation;
        protected AsyncOperation _loadDestinationAsyncOperation;
        protected AsyncOperation _unloadLoadingAsyncOperation;

        protected bool _setRealtimeProgressValueIsNull;
        protected bool _setInterpolatedProgressValueIsNull;
        
        protected const float _asyncProgressLimit = 0.9f;
        
        /// <summary>
        /// Call this static method to load a scene from anywhere
        /// </summary>
        /// <param name="sceneToLoadName">Level name.</param>
        public static void LoadScene(string sceneToLoadName, string loadingSceneName = "MMAdditiveLoadingScreen", 
                                        ThreadPriority threadPriority = ThreadPriority.High, bool secureLoad = true,
                                        bool interpolateProgress = true,
                                        float beforeEntryFadeDelay = 0f,
                                        float entryFadeDuration = 0.2f,
                                        float afterEntryFadeDelay = 0f,
                                        float beforeExitFadeDelay = 0.2f,
                                        float exitFadeDuration = 0.2f, 
                                        MMTweenType entryFadeTween = null, MMTweenType exitFadeTween = null,
                                        float progressBarSpeed = 5f, 
                                        FadeModes FadeMode = FadeModes.FadeInThenOut)
        {
	        if (_loadingInProgress)
            {
	            Debug.LogError("MMLoadingSceneManagerAdditive : a request to load a new scene was emitted while a scene load was already in progress");  
                return;
            }

            if (secureLoad)
            {
	            _scenesInBuild = MMScene.GetScenesInBuild();
	            
	            if (!_scenesInBuild.Contains(sceneToLoadName))
	            {
		            Debug.LogError("MMLoadingSceneManagerAdditive : impossible to load the '"+sceneToLoadName+"' scene, " +
		                           "there is no such scene in the project's build settings.");
		            return;
	            }
	            if (!_scenesInBuild.Contains(loadingSceneName))
	            {
		            Debug.LogError("MMLoadingSceneManagerAdditive : impossible to load the '"+loadingSceneName+"' scene, " +
		                           "there is no such scene in the project's build settings.");
		            return;
	            }
            }

            _loadingInProgress = true;
            _initialScenes = MMScene.GetLoadedScenes();

            Application.backgroundLoadingPriority = threadPriority;
            _sceneToLoadName = sceneToLoadName;					
			_loadingScreenSceneName = loadingSceneName;
			_beforeEntryFadeDelay = beforeEntryFadeDelay;
			_entryFadeDuration = entryFadeDuration;
            _entryFadeTween = entryFadeTween;
            _afterEntryFadeDelay = afterEntryFadeDelay;
            _progressInterpolationSpeed = progressBarSpeed;
            _beforeExitFadeDelay = beforeExitFadeDelay;
            _exitFadeDuration = exitFadeDuration;
            _exitFadeTween = exitFadeTween;
            _fadeMode = FadeMode;
            _interpolateProgress = interpolateProgress;

            SceneManager.LoadScene(_loadingScreenSceneName, LoadSceneMode.Additive);
		}

		/// <summary>
		/// On Start(), we start loading the new level asynchronously
		/// </summary>
		protected virtual void Awake()
        {
            Initialization();
        }

        /// <summary>
        /// Initializes timescale, computes null checks, and starts the load sequence
        /// </summary>
        protected virtual void Initialization()
        {
	        MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : Initialization");

	        if (DebugMode)
	        {
		        foreach (Scene scene in _initialScenes)
		        {
			        MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : Initial scene : " + scene.name);
		        }    
	        }

	        _setRealtimeProgressValueIsNull = SetRealtimeProgressValue == null;
            _setInterpolatedProgressValueIsNull = SetInterpolatedProgressValue == null;
            Time.timeScale = 1f;

            if ((_sceneToLoadName == "") || (_loadingScreenSceneName == ""))
            {
	            return;
            }
            
            StartCoroutine(LoadSequence());
        }

		/// <summary>
		/// Every frame, we fill the bar smoothly according to loading progress
		/// </summary>
		protected virtual void Update()
		{
			UpdateProgress();
		}

		/// <summary>
		/// Sends progress value via UnityEvents
		/// </summary>
		protected virtual void UpdateProgress()
		{
			if (!_setRealtimeProgressValueIsNull)
			{
				SetRealtimeProgressValue.Invoke(_loadProgress);
			}

			if (_interpolateProgress)
			{
				_interpolatedLoadProgress = MMMaths.Approach(_interpolatedLoadProgress, _loadProgress, Time.deltaTime * _progressInterpolationSpeed);
				if (!_setInterpolatedProgressValueIsNull)
				{
					SetInterpolatedProgressValue.Invoke(_interpolatedLoadProgress);	
				}
			}
			else
			{
				SetInterpolatedProgressValue.Invoke(_loadProgress);	
			}
		}

		/// <summary>
		/// Loads the scene to load asynchronously.
		/// </summary>
		protected virtual IEnumerator LoadSequence()
		{
			InitiateLoad();
			yield return ProcessDelayBeforeEntryFade();
			yield return EntryFade();
			yield return ProcessDelayAfterEntryFade();
			yield return UnloadOriginScenes();
			yield return LoadDestinationScene();
			yield return ProcessDelayBeforeExitFade();
            yield return DestinationSceneActivation();
			yield return ExitFade();
			yield return UnloadSceneLoader();
			LoadTransitionComplete();
		}

		/// <summary>
		/// Initializes counters and timescale
		/// </summary>
		protected virtual void InitiateLoad()
		{
			_loadProgress = 0f;
			_interpolatedLoadProgress = 0f;
			Time.timeScale = 1f;
			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : Initiate Load");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.LoadStarted);
			OnLoadStarted?.Invoke();
		}

		/// <summary>
		/// Waits for the specified BeforeEntryFadeDelay duration
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator ProcessDelayBeforeEntryFade()
		{
			if (_beforeEntryFadeDelay > 0f)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : delay before entry fade, duration : " + _beforeEntryFadeDelay);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.BeforeEntryFade);
				OnBeforeEntryFade?.Invoke();
				
				yield return MMCoroutine.WaitFor(_beforeEntryFadeDelay);
			}
		}

		/// <summary>
		/// Calls a fader on entry
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator EntryFade()
		{
			if (_entryFadeDuration > 0f)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : entry fade, duration : " + _entryFadeDuration);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.EntryFade);
				OnEntryFade?.Invoke();
				
				if (_fadeMode == FadeModes.FadeOutThenIn)
				{
					yield return null;
					MMFadeOutEvent.Trigger(_entryFadeDuration, _entryFadeTween, FaderID, true);
				}
				else
				{
					yield return null;
					MMFadeInEvent.Trigger(_entryFadeDuration, _entryFadeTween, FaderID, true);
				}           

				yield return MMCoroutine.WaitFor(_entryFadeDuration);
			}
		}

		/// <summary>
		/// Waits for the specified AfterEntryFadeDelay
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator ProcessDelayAfterEntryFade()
		{
			if (_afterEntryFadeDelay > 0f)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : delay after entry fade, duration : " + _afterEntryFadeDelay);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.AfterEntryFade);
				OnAfterEntryFade?.Invoke();
				
				yield return MMCoroutine.WaitFor(_afterEntryFadeDelay);
			}
		}

		/// <summary>
		/// Unloads the original scene(s) and waits for the unload to complete
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator UnloadOriginScenes()
		{
			foreach (Scene scene in _initialScenes)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : unload scene " + scene.name);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.UnloadOriginScene);
				OnUnloadOriginScene?.Invoke();
				
				_unloadOriginAsyncOperation = SceneManager.UnloadSceneAsync(scene);
				while (_unloadOriginAsyncOperation.progress < _asyncProgressLimit)
				{
					yield return null;
				}
			}
		}

		/// <summary>
		/// Loads the destination scene
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator LoadDestinationScene()
		{
			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : load destination scene");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.LoadDestinationScene);
			OnLoadDestinationScene?.Invoke();

			_loadDestinationAsyncOperation = SceneManager.LoadSceneAsync(_sceneToLoadName, LoadSceneMode.Additive );
            _loadDestinationAsyncOperation.completed += OnLoadOperationComplete;

            _loadDestinationAsyncOperation.allowSceneActivation = false;
            
			while (_loadDestinationAsyncOperation.progress < _asyncProgressLimit)
			{
				_loadProgress = _loadDestinationAsyncOperation.progress;
				yield return null;
			}
            
			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : load progress complete");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.LoadProgressComplete);
			OnLoadProgressComplete?.Invoke();

			// when the load is close to the end (it'll never reach it), we set it to 100%
			_loadProgress = 1f;

			// we wait for the bar to be visually filled to continue
			if (_interpolateProgress)
			{
				while (_interpolatedLoadProgress < 1f)
				{
					yield return null;
				}
			}			

			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : interpolated load complete");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.InterpolatedLoadProgressComplete);
			OnInterpolatedLoadProgressComplete?.Invoke();
		}

        /// <summary>
        /// Waits for BeforeExitFadeDelay seconds
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ProcessDelayBeforeExitFade()
		{
			if (_beforeExitFadeDelay > 0f)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : delay before exit fade, duration : " + _beforeExitFadeDelay);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.BeforeExitFade);
				OnBeforeExitFade?.Invoke();
				
				yield return MMCoroutine.WaitFor(_beforeExitFadeDelay);
			}
		}

		/// <summary>
		/// Requests a fade on exit
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator ExitFade()
		{
			if (_exitFadeDuration > 0f)
			{
				MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : exit fade, duration : " + _exitFadeDuration);
				MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.ExitFade);
				OnExitFade?.Invoke();
				
				if (_fadeMode == FadeModes.FadeOutThenIn)
				{
					MMFadeInEvent.Trigger(_exitFadeDuration, _exitFadeTween, FaderID, true);
				}
				else
				{
					MMFadeOutEvent.Trigger(_exitFadeDuration, _exitFadeTween, FaderID, true);
				}
				yield return MMCoroutine.WaitFor(_exitFadeDuration);
			}
		}

		/// <summary>
		/// Activates the destination scene
		/// </summary>
		protected virtual IEnumerator DestinationSceneActivation()
        {
            yield return MMCoroutine.WaitForFrames(1);
            MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : activating destination scene");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.DestinationSceneActivation);
			OnDestinationSceneActivation?.Invoke();
            _loadDestinationAsyncOperation.allowSceneActivation = true;
            while (_loadDestinationAsyncOperation.progress < 1.0f)
            {
                yield return null;
            }
        }

        /// <summary>
        /// A method triggered when the async operation completes
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnLoadOperationComplete(AsyncOperation obj)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoadName));
            MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : set active scene to " + _sceneToLoadName);

        }

        /// <summary>
        /// Unloads the scene loader
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator UnloadSceneLoader()
		{
			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : unloading scene loader");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.UnloadSceneLoader);
			OnUnloadSceneLoader?.Invoke();
			
			yield return null; // mandatory yield to avoid an unjustified warning
			_unloadLoadingAsyncOperation = SceneManager.UnloadSceneAsync(_loadingScreenSceneName);
			while (_unloadLoadingAsyncOperation.progress < _asyncProgressLimit)
			{
				yield return null;
			}	
		}

		/// <summary>
		/// Completes the transition
		/// </summary>
		protected virtual void LoadTransitionComplete()
		{
			MMLoadingSceneDebug("MMLoadingSceneManagerAdditive : load transition complete");
			MMSceneLoadingManager.LoadingSceneEvent.Trigger(_sceneToLoadName, MMSceneLoadingManager.LoadingStatus.LoadTransitionComplete);
			OnLoadTransitionComplete?.Invoke();
			
			_loadingInProgress = false;
		}

		/// <summary>
		/// On Destroy we reset our state
		/// </summary>
		protected void OnDestroy()
		{
			_loadingInProgress = false;
		}

		/// <summary>
		/// A debug method used to output console messages, for this class only
		/// </summary>
		/// <param name="message"></param>
		protected void MMLoadingSceneDebug(string message)
		{
			if (!DebugMode)
			{
				return;
			}
			
			string output = "";
			output += "<color=#82d3f9>[" + Time.frameCount + "]</color> ";
			output += "<color=#f9a682>[" + MMTime.FloatToTimeString(Time.time, false, true, true, true) + "]</color> ";
			output +=  message;
			Debug.Log(output);
		}
	}
}