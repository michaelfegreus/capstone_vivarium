using TMPro;
using System.Text;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers;

public class ClockManager : Singleton<ClockManager> {

	//[Header("Time of Day")]
	public GameTime inGameTime;

    // Format inGameTime properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            inGameTime.OnValidate();

            // Day Cycle range validates
            {
                morning.startTime.OnValidate();
                morning.peakStartTime.OnValidate();
                morning.peakEndTime.OnValidate();
                morning.endTime.OnValidate();

                midday.startTime.OnValidate();
                midday.peakStartTime.OnValidate();
                midday.peakEndTime.OnValidate();
                midday.endTime.OnValidate();

                evening.startTime.OnValidate();
                evening.peakStartTime.OnValidate();
                evening.peakEndTime.OnValidate();
                evening.endTime.OnValidate();

                night.startTime.OnValidate();
                night.peakStartTime.OnValidate();
                night.peakEndTime.OnValidate();
                night.endTime.OnValidate();
            }
        }
    }

    [Tooltip("How many real-world seconds there are in an in-game minute.")]
	public float secondsInGameMinute = 1f;

	// Trigger to notify End of Day bool in articy global variables.
	bool gameEndOfDay;

    [HideInInspector]
	public float secondsElapsed = 0f;

	//public Text clockText;
	public TextMeshProUGUI clockTextMesh;

    // Event delegate system for change of time of day block. Used by things like the audio manager.
    public delegate void TimeBlockChangeAction();
    public static event TimeBlockChangeAction OnTimeBlockChange;

    // Event delegate system for new day start. Used by things like the Fungs Event Handler.
    public delegate void DayStart();
    public static event DayStart OnDayStart;

    // Event delegate system for in-game minute tick. This checks events on the tick of every in-game minute...in other words, every seconds. This might be inefficient or heavy in the profiler, we'll find out.
    public delegate void MinuteTick();
    public static event MinuteTick OnMinuteTick;

    // Day time periods to reference for lighting, post processing, ambient sound, and more.
    public DayCycleRange morning;
    public DayCycleRange midday;
    public DayCycleRange evening;
    public DayCycleRange night;

    void Start () {

        // Run delegate methods for starting a new day.
        if(OnDayStart != null) { 
            OnDayStart();
        }

        // Setup delegate methods for each of the day cycle ranges.
        morning.SubscribeToMinuteTick();
        midday.SubscribeToMinuteTick();
        evening.SubscribeToMinuteTick();
        night.SubscribeToMinuteTick();
    }
	
	void Update () {
		RunGameClock ();
        // TimeOfDayColor(); // Set a post processing profile on the camera to recolor the screen depending on time of deay.

        // update the time pool readout for the music
        MusicTimeUpdate();
	}

    void MusicTimeUpdate()
    {
        // morning
        if (inGameTime.hour > morning.startTime.hour && inGameTime.hour < morning.endTime.hour)
        {
            Josh.MusicPlayer.instance.currentTime = Josh.MusicPlayer.PossibleTimes.Morning;
        } 

        // midday
        if (inGameTime.hour > midday.startTime.hour && inGameTime.hour < midday.endTime.hour)
        {
            Josh.MusicPlayer.instance.currentTime = Josh.MusicPlayer.PossibleTimes.Midday;
        }

        // evening
        if (inGameTime.hour > evening.startTime.hour && inGameTime.hour < evening.endTime.hour)
        {
            Josh.MusicPlayer.instance.currentTime = Josh.MusicPlayer.PossibleTimes.Evening;
        }

        // night
        if (inGameTime.hour > night.startTime.hour && inGameTime.hour < night.endTime.hour)
        {
            Josh.MusicPlayer.instance.currentTime = Josh.MusicPlayer.PossibleTimes.Night;
        }

    }

    void RunGameClock (){
		// Change this if there is an update to time that needs to be reflected in articy:draft global variables and the HUD
		bool clockUpdated = false;
		// Reset End of Day variable in articy:draft global variables if it's currently set to true;
		if (gameEndOfDay) {
			gameEndOfDay = false;
			//ArticyDatabase.DefaultGlobalVariables.SetVariableByString ("World.endOfDay", gameEndOfDay);
		}

		secondsElapsed += Time.smoothDeltaTime; // I'm not completely sure what smoothDeltaTime functions like, but I'll try it.
		// When the real-time timer reaches the amount of seconds needed to fulfill one in-game minute...
		if (secondsElapsed > secondsInGameMinute) 
        {
			// Account for a game minute by subtracting the respective amount of seconds
			secondsElapsed -= secondsInGameMinute;
			inGameTime.minute += 1; // Increment the game minutes
            
			clockUpdated = true;
			// When you reach enough minutes to fulfill an hour...
			if (inGameTime.minute > 59) {
				inGameTime.minute = 0;
				inGameTime.hour += 1;   // Increment the game hours
                // If the hour reaches the end of the day...
                if (inGameTime.hour > 23) {
					inGameTime.hour = 0;
					IncrementDay ();
				}
			}
            // Run Minute Tick delegate functions to check for game events and such.
            if (OnMinuteTick != null)
            {
                OnMinuteTick();
            }
        }

		if (clockUpdated) {
			UpdateClockValues ();
            //Debug.Log("Clock updated");
		}
	}

	// Use StringBuilder for more efficiency and less garbage strings, given how often the clock updates
	StringBuilder clockTimeBuilder;

	void UpdateClockValues(){

		clockTimeBuilder = new StringBuilder ();

		if (clockTextMesh != null) {
			clockTimeBuilder.Append (inGameTime.hour.ToString ("D2")).Append (" : ").Append (inGameTime.minute.ToString ("D2"));
			clockTextMesh.text = clockTimeBuilder.ToString();
		}
	}

	void IncrementDay(){
		
		Debug.Log ("End of day.");
		gameEndOfDay = true;



        // Run delegate methods for starting a new day.
        if (OnDayStart != null)
        {
            OnDayStart();
        }
	}

	public void SetNewTime(int newHour, int newMinute, bool newDay){
		if (newDay) {
			IncrementDay ();
		}

		inGameTime.hour = newHour;
		inGameTime.minute = newMinute;

        UpdateClockValues ();
	}

    public void StartNewDay(int newHour)
    {
        IncrementDay();

        inGameTime.hour = newHour;
        inGameTime.minute = 0;

        UpdateClockValues();
    }
}