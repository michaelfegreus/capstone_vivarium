using UnityEngine;
using System;

[Serializable]
public class GameTime {

    // Human-readable 00:00 European clock format that will be converted to the hour and minute variables.
    [Tooltip("Set an in-game time value. Set hour 0 - 23, and minute 0-59 using 00:00 format.")]
    [SerializeField] string inGameTime = "00:00";

    // TODO: Make these private and just set up get/set functions
    [HideInInspector] public int hour;
    [HideInInspector] public int minute;

    // Format the time as 00:00 European clock
    // Unfortunately you'll have to call this function from outside classes dealing with this. That's because only components like Monobehaviors interact with OnValidate.
    [ExecuteInEditMode]
    public void OnValidate()
    {
        string hourString;
        string minuteString;
        // Set up char array, start them all off at 0.
        char[] clockCharacters = new char[4];
        for (int i = 0; i < clockCharacters.Length; i++)
        {
            clockCharacters[i] = '0';
        }
        // Run a while loop to get the first four numbers of the public setGameTime string to put into our clock.
        int x = 0;
        int currentClockChar = 0;
        while(x < inGameTime.Length && currentClockChar < 4)
        {
            if (Char.IsNumber(inGameTime[x]))
            {
                clockCharacters[currentClockChar] = inGameTime[x];
                currentClockChar++; 
            }
            x++;
        }
        // If you only input three numbers, set it up with a leading 0. Otherwise, go straight to xx:xx format.
        if (currentClockChar == 3)
        {
            hourString = "0" + (clockCharacters[0].ToString());
            minuteString = (clockCharacters[1].ToString()) + (clockCharacters[2].ToString());
        }
        else
        {
            hourString = (clockCharacters[0].ToString()) + (clockCharacters[1].ToString());
            minuteString = (clockCharacters[2].ToString()) + (clockCharacters[3].ToString());
        }
        // Set up the hour and minute variables.
        hour = Int32.Parse(hourString);
        minute = Int32.Parse(minuteString);

        /* 
        // Then clamp these values to reflect real clock times.
        // hour = Mathf.Clamp(hour, 0, 23);
        // minute = Mathf.Clamp(minute, 0, 59);*/

        // Instead of clamping these values, just zero them out if they aren't valid times.
        if ((hour < 0 || hour > 23) || (minute < 0 || minute > 59))
        {
            //Debug.LogWarning("Invalid time. Set hour 0 - 23, and minute 0-59 using 00:00 format.");
            hour = 0;
            minute = 0;
        }

        // Set a formatted version of the string of the string in the inspector.
        inGameTime = hour.ToString("D2") + ":" + minute.ToString("D2");
    }

    // Display time horizontally
    /*
    void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        hour =  EditorGUILayout.IntField("Hour", hour);
        minute = EditorGUILayout.IntField("Minute", minute);
        EditorGUILayout.EndHorizontal();
    }*/

    public GameTime(int setHour, int setMinute){
		hour = setHour;
		minute = setMinute;
   	}

	/// <summary>>
	/// Returns True if this GameTime is equal to or greater than the other comparing GameTime.
	/// Useful for checking if the clock has reached a certain part of the day.</summary>
	public bool TimeMet(GameTime other){
		bool met = false;
		if (this.hour > other.hour) {
			met = true;
		} else if (this.hour == other.hour) {
			if (this.minute >= other.minute) {
				met = true;
			}
		}
		return met;
	}

    /// <summary>>
    /// Returns True if this GameTime is exactly equal to the comparing time.
    /// </summary>
    /// 
    public bool TimeEquals(GameTime other)
    {
        bool equalTime = false;
        if (this.hour == other.hour && this.minute == other.minute)
        {
            equalTime = true;
        }
        return equalTime;
    }

    /// <summary>>
    /// Returns the GameTime converted to minutes into the day.
    /// </summary>
    public int ThisTimeInMinutes()
    {
        return (hour * 60) + minute;
    }

	/// <summary>>
	/// Returns True if this GameTime is equal the other comparing GameTime.</summary>
	/*public override bool Equals(GameTime other){
		if (this.hour == other.hour && this.minute == other.minute) {
			return true;
		} else {
			return false;
		}
	}*/



	/*Getters and setters. (I think public properties are preferred in Unity development?)
	  ublic void SetTime(int setHour, int setMinute){
		hour = setHour;
		minute = setMinute;
	}

	public int GetHour(){
		return hour;
	}

	public int GetMinute(){
		return minute;
	}*/


}
