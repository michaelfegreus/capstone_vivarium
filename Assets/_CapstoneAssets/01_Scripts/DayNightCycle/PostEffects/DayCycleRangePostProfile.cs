using System;
using UnityEngine.Rendering;

[Serializable]
public class DayCycleRangePostProfile : DayCycleRange
{
    //This is the first constructor for the class.
    //It calls the parent constructor immediately, even
    //before it runs.
    public DayCycleRangePostProfile()
    {

    }

    public Volume postVolume;

    //This is the second constructor for the class.
    //It specifies which parent constructor will be called
    //using the "base" keyword.
    /*
    public DayCycleRangePostProfile(Volume setPostVolume, GameTime setStartTime, GameTime setPeakStartTime, GameTime setPeakEndTime, GameTime setEndTime) : base(GameTime setStartTime, GameTime setPeakStartTime, GameTime setPeakEndTime, GameTime setEndTime)
    {

    }*/

    /*
    public DayCycleRangePostProfile(Volume setPostVolume, GameTime setStartTime, GameTime setPeakStartTime, GameTime setPeakEndTime, GameTime setEndTime)
    {
        postVolume = setPostVolume;
        startTime = setStartTime;
        peakStartTime = setPeakStartTime;
        peakEndTime = setPeakEndTime;
        endTime = setEndTime;
    }*/

    /* public Volume postVolume; //{ get; set; }
     public GameTime startTime; //{ get; set; }
     public GameTime peakStartTime; //{ get; set; }
     public GameTime peakEndTime;
     public GameTime endTime; //{ get; set; }*/
}