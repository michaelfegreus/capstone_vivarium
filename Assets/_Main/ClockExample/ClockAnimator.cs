using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour
{
    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;

    public Transform hours, minutes, seconds;
    public bool analog = true; // Smooth == true, Rigid TickTock == false.

    [Header("Custom Time")]
    public bool useCustomTime = true;
    public float m_timeAmplifier = 20f;
    public DayCycle m_customTime = new DayCycle();

    [Serializable]
    public class DayCycle
    {
        [SerializeField]
        private float m_hours = 7, m_minutes = 5, m_seconds = 23;

        public float Hours { get { return m_hours; } set { m_hours = Mathf.Repeat( value, 24.0f );  } }
        public float Minutes { get { return m_minutes; } set { m_minutes = Mathf.Repeat( value, 60.0f ); } }
        public float Seconds { get { return m_seconds; } set { m_seconds = Mathf.Repeat( value, 60.0f ); } }

        private const float m_hourInSeconds = 3600.0f,
                            m_minuteInSeconds = 60.0f;

        public void AddSeconds(float durationInSeconds)
        {
            Hours += durationInSeconds / m_hourInSeconds;
            Minutes += durationInSeconds / m_minuteInSeconds;
            Seconds += durationInSeconds;
        }

        public void AddMinutes(float durationInMinutes) { AddSeconds( durationInMinutes * m_minuteInSeconds ); }
        public void AddHours( float durationInHours ) { AddSeconds( durationInHours * m_hourInSeconds ); }
    }

    void Update()
    {
        if( useCustomTime )
        {
            m_customTime.AddSeconds( Time.deltaTime * m_timeAmplifier );
            Debug.LogFormat( "{0};{1};{2}", m_customTime.Hours.ToString( "00" ), m_customTime.Minutes.ToString( "00" ), m_customTime.Seconds.ToString( "00" ) );

            if( analog )
            {
                hours.localRotation =
                    Quaternion.Euler( 0f, 0f, m_customTime.Hours * -hoursToDegrees );
                minutes.localRotation =
                    Quaternion.Euler( 0f, 0f, m_customTime.Minutes * -minutesToDegrees );
                seconds.localRotation =
                    Quaternion.Euler( 0f, 0f, m_customTime.Seconds * -secondsToDegrees );
            }
            else
            {
                hours.localRotation =
                    Quaternion.Euler( 0f, 0f, Mathf.RoundToInt( m_customTime.Hours ) * -hoursToDegrees );
                minutes.localRotation =
                    Quaternion.Euler( 0f, 0f, Mathf.RoundToInt( m_customTime.Minutes ) * -minutesToDegrees );
                seconds.localRotation =
                    Quaternion.Euler( 0f, 0f, Mathf.RoundToInt( m_customTime.Seconds ) * -secondsToDegrees );
            }
        }
        else 
        {
            if( analog )
            {
                TimeSpan timespan = DateTime.Now.TimeOfDay;
                hours.localRotation =
                    Quaternion.Euler( 0f, 0f, ( float )timespan.TotalHours * -hoursToDegrees );
                minutes.localRotation =
                    Quaternion.Euler( 0f, 0f, ( float )timespan.TotalMinutes * -minutesToDegrees );
                seconds.localRotation =
                    Quaternion.Euler( 0f, 0f, ( float )timespan.TotalSeconds * -secondsToDegrees );
            }
            else
            {
                DateTime time = DateTime.Now;
                hours.localRotation = Quaternion.Euler( 0f, 0f, time.Hour * -hoursToDegrees );
                minutes.localRotation = Quaternion.Euler( 0f, 0f, time.Minute * -minutesToDegrees );
                seconds.localRotation = Quaternion.Euler( 0f, 0f, time.Second * -secondsToDegrees );
            }

            DateTime currentTime = DateTime.Now;
            Debug.LogFormat( "{0};{1};{2}", currentTime.Hour.ToString( "00" ), currentTime.Minute.ToString( "00" ), currentTime.Second.ToString( "00" ) );
        }
    }
}

