using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class UI_clock : MonoBehaviour
{
    // public Transform pictogramWheel;
    // public Transform hourNeedle;

    [Tooltip("Set to true to use global 00:00 digital time. Leave false for US standard.")]
    public bool militaryTime;
    public SuperTextMesh slot01, slot02, slot03, slot04;

    public SuperTextMesh textAM_PM;
    public Image imageAM_PM;
    public Animator animAM_PM;

    void OnEnable()
    {
        GAME_clock_manager.OnMinuteTick += UpdateClockValues;
    }
    void OnDisable()
    {
        GAME_clock_manager.OnMinuteTick -= UpdateClockValues;
    }

    // Use StringBuilder for more efficiency and less garbage strings, given how often the clock updates
    StringBuilder clockTimeBuilder;

    bool AM;

    void UpdateTextAMPM()
    {
        if (AM)
        {
            textAM_PM.text = "AM";
        }
        else
        {
            textAM_PM.text = "PM";
        }
    }

    private void Start()
    {
        if (militaryTime)
        {
            animAM_PM.enabled = false;
            imageAM_PM.enabled = false;
            textAM_PM.enabled = false;
        }

        // if it's noon or after, set to PM
        if (GAME_clock_manager.Instance.inGameTime.TimeMet(new GameTime(12, 0))){
            AM = false;
        }
        else
        {
            AM = true;
        }
        UpdateTextAMPM();
    }

    void UpdateClockValues()
    {
        //myFlowchart.SetIntegerVariable (gameHourGlobal, inGameTime.hour);
        //myFlowchart.SetIntegerVariable (gameMinuteGlobal, inGameTime.minute);

        int hour = GAME_clock_manager.Instance.inGameTime.hour;
        int minute = GAME_clock_manager.Instance.inGameTime.minute;

        if(hour > 11 && AM)
        {
            AM = false;
            if (animAM_PM.enabled)
            {
                animAM_PM.SetTrigger("triggerAMPM");
            }
        }
        if(hour <= 11 && !AM)
        {
            AM = true;
            if (animAM_PM.enabled)
            {
                animAM_PM.SetTrigger("triggerAMPM");
            }
        }        

        if (!militaryTime)
        {
            if (hour > 12)
            {
                hour -= 12;
            }else if(hour == 0)
            {
                hour = 12;
            }
        }

        clockTimeBuilder = new StringBuilder();

        clockTimeBuilder.Append(hour.ToString("D2")).Append(minute.ToString("D2"));
        slot01.text = clockTimeBuilder.ToString().Substring(0, 1);
        slot02.text = clockTimeBuilder.ToString().Substring(1, 1);
        slot03.text = clockTimeBuilder.ToString().Substring(2, 1);
        slot04.text = clockTimeBuilder.ToString().Substring(3, 1);
    }
    /*
    private void LateUpdate()
    {
        // Set the pictogram wheel to rotate in the opposite direction of the hour needle.
        // TODO: Set up with AM PM
        pictogramWheel.transform.rotation = Quaternion.Euler(0f, 180f, hourNeedle.transform.localEulerAngles.z);

    }*/

    /* My original means of clock animator:

    // Modified from Unity tutorial 

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f;
        //secondsToDegrees = 360f / 60f;

    public Transform hours, minutes; //seconds removed
    public bool analog;

    void UIClockUpdate()
    {
        if (analog)
        {
            hours.localRotation =
                Quaternion.Euler(0f, 0f, WORLD_time_manager.Instance.inGameTime.hour * -hoursToDegrees);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f, WORLD_time_manager.Instance.inGameTime.minute * -minutesToDegrees);
            seconds.localRotation =
                Quaternion.Euler(0f, 0f, (float)timespan.TotalSeconds * -secondsToDegrees);
        }
        else
        {
            DateTime time = DateTime.Now;
            hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -hoursToDegrees);
            minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -minutesToDegrees);
            // seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -secondsToDegrees);
        }

      // Debug.Log("Tick");
    }*/
}