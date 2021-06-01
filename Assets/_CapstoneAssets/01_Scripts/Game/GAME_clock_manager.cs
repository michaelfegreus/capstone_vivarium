using TMPro;
using System.Text;
using UnityEngine;
using Fungus;

public class GAME_clock_manager : Singleton<GAME_clock_manager> {

	//public Flowchart myFlowchart; -- Phasing out Fungus as the time keeper. I think this script should just hold the time variables as a part of the WORLD_manager singleton.

	// Fungus variables
	//static readonly string gameHourGlobal = "gameHour";
	//static readonly string gameMinuteGlobal = "gameMinute";
	//static readonly string gameDayGlobal = "gameDay";

    

	// Event message to send to Fungus flowcharts on the increment of the day counter and the start of a new day.
	string newDayEventMessage = "DayStart";

	//[Header("Time of Day")]
	public GameTime inGameTime;

    // Format inGameTime properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            inGameTime.OnValidate();
        }
    }

    /*[Tooltip("How many hours have elapsed this day.")]
	public int gameHour = 0;
	[Tooltip("How many minutes have elapsed since the turn of the last hour.")]
	public int gameMinute = 0;*/
    [Tooltip("How many real-world seconds there are in an in-game minute.")]
	public float secondsInGameMinute = 1f;

	// Trigger to notify End of Day bool in articy global variables.
	bool gameEndOfDay;

    [HideInInspector]
	public float secondsElapsed = 0f;

    //public MonoBehaviour postProcessingBehavior;
    public DAYNIGHT_posteffect daylightPostScript;

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

    void Start () {

        // If you set either the hourOfTime, or minuteOfHour variables in the Fungus Global Variables
        // then overwrite the local corresponding variables with those. Useful as a testing option.
        /*if (myFlowchart.GetIntegerVariable (gameHourGlobal) != 0 || myFlowchart.GetIntegerVariable (gameMinuteGlobal) != 0) {
			inGameTime = new GameTime (myFlowchart.GetIntegerVariable (gameHourGlobal), myFlowchart.GetIntegerVariable (gameMinuteGlobal));
		} else {
			inGameTime = new GameTime (debugTime.x, debugTime.y);
		}*/

        //inGameTime = new GameTime(myFlowchart.GetIntegerVariable(gameHourGlobal), myFlowchart.GetIntegerVariable(gameMinuteGlobal));

        // Setup daylight post script delegate for the minute tick.
        OnMinuteTick += daylightPostScript.DaylightMinuteUpdate;

        // Run delegate methods for starting a new day.
        if(OnDayStart != null) { 
            OnDayStart();
        }
    }


	
	void Update () {
		RunGameClock ();
		// TimeOfDayColor(); // Set a post processing profile on the camera to recolor the screen depending on time of deay.
	}
    /*
	void TimeOfDayColor(){
		if (postProcessingBehavior != null) {
			// Between 9pm and 6am, make it dark.
			if (inGameTime.hour < 7 || inGameTime.hour > 19) {
				postProcessingBehavior.enabled = true;
			} else {
				postProcessingBehavior.enabled = false;
			}
		}
	}*/

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
		if (secondsElapsed > secondsInGameMinute) {
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
		//myFlowchart.SetIntegerVariable (gameHourGlobal, inGameTime.hour);
		//myFlowchart.SetIntegerVariable (gameMinuteGlobal, inGameTime.minute);

		clockTimeBuilder = new StringBuilder ();

		if (clockTextMesh != null) {
			clockTimeBuilder.Append (inGameTime.hour.ToString ("D2")).Append (" : ").Append (inGameTime.minute.ToString ("D2"));
			clockTextMesh.text = clockTimeBuilder.ToString();
			//clockTextMesh.text = gameHour.ToString("D2") + " : " + gameMinute.ToString("D2");
		}
	}

	void IncrementDay(){
		// Here, this should increment the Flowchart day counter variable.
		
        //int currentDayCount = 1 + myFlowchart.GetIntegerVariable(gameDayGlobal);
		//myFlowchart.SetIntegerVariable (gameDayGlobal, currentDayCount);
		
        // Broadcast event message for new day
		Flowchart.BroadcastFungusMessage (newDayEventMessage);
		Debug.Log ("End of day.");
		// Notify articy:draft global variable about the end of day.
		gameEndOfDay = true;
        //ArticyDatabase.DefaultGlobalVariables.SetVariableByString("World.endOfDay", gameEndOfDay);

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
}