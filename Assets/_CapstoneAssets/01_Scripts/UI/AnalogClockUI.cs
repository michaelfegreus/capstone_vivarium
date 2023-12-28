using UnityEngine;
using System;

public class AnalogClockUI : MonoBehaviour
{
    // Courtesty of https://answers.unity.com/questions/1271151/programming-accurate-clock-hands.html
    // Edited by ME

    private const float
        hours24ToDegrees = 360f / 24f,
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;

    public Transform hours, minutes, seconds, hours24;
   
    public float m_timeAmplifier = 20f;
    public DayCycle m_customTime = new DayCycle();

    [Serializable]
    public class DayCycle
    {
        [SerializeField]
        private float m_hours, m_minutes, m_seconds;

        public float Hours { get { return m_hours; } set { m_hours = Mathf.Repeat(value, 24.0f); } }
        public float Minutes { get { return m_minutes; } set { m_minutes = Mathf.Repeat(value, 60.0f); } }
        public float Seconds { get { return m_seconds; } set { m_seconds = Mathf.Repeat(value, 60.0f); } }

        private const float m_hourInSeconds = 3600.0f,
                            m_minuteInSeconds = 60.0f;

        public void AddSeconds(float durationInSeconds)
        {
            //Hours += durationInSeconds / m_hourInSeconds;
            //Minutes += durationInSeconds / m_minuteInSeconds;
            Seconds += durationInSeconds;
        }

        public void AddMinutes(float durationInMinutes) { AddSeconds(durationInMinutes * m_minuteInSeconds); }
        public void AddHours(float durationInHours) { AddSeconds(durationInHours * m_hourInSeconds); }

        public void OverrideCustomTime(int overrideHours, int overrideMinutes)
        {
            m_hours = overrideHours;
            m_minutes = overrideMinutes;
        }

        public void SetupHands()
        {
            Minutes += Seconds / 60f; // I don't know why this doesn't work at different Seconds in Game Minute
            Hours += Minutes / 60f;
            
        }
    }

        void Start()
        {
            m_customTime.OverrideCustomTime(ClockManager.Instance.inGameTime.hour, ClockManager.Instance.inGameTime.minute);
        }

        private void LateUpdate()
        {
            m_customTime.OverrideCustomTime(ClockManager.Instance.inGameTime.hour, ClockManager.Instance.inGameTime.minute);
            m_customTime.AddSeconds((Time.smoothDeltaTime / ClockManager.Instance.secondsInGameMinute) * 60f);
            m_customTime.SetupHands();

            hours24.localRotation =
                        Quaternion.Euler(0f, 180f, m_customTime.Hours * -hours24ToDegrees);
            hours.localRotation =
                Quaternion.Euler(0f, 0f, m_customTime.Hours * -hoursToDegrees);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f, m_customTime.Minutes * -minutesToDegrees);
            seconds.localRotation =
                Quaternion.Euler(0f, 0f, m_customTime.Seconds * -secondsToDegrees);

        }
    
}

