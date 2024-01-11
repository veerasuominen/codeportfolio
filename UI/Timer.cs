using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace HexKeyGames
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text timerTMP;
        [SerializeField, Tooltip("Seconds")]
        private float timeLimit = 16 * 60;
        private float startTime;
        private readonly string filePath = Application.dataPath + "/minutes.txt";

        public static Timer Instance;

        [SerializeField]
        private SpeakerManager speakerManager;
        public float RemainingTime { get; private set; }
        public string RemainingTimeString { get; private set; }

        [SerializeField]
        List<int> announcementTimes = new List<int>()
        {
            900, 600, 300, 240, 180, 120, 60, 0
        };

        private void Awake()
        {
            Instance = this;

            if (timerTMP == null)
                timerTMP = GetComponent<TMP_Text>();
            if(speakerManager == null)
                speakerManager = FindAnyObjectByType<SpeakerManager>();

            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                if (float.TryParse(text, out float result))
                    timeLimit = result * 60;
            }
            RandomAnnouncementTimes();
            timerTMP.text = "";
        }


        public void StartTimer()
        {
            // TODO: prevent timer from being started twice?
            startTime = Time.timeSinceLevelLoad;
            RemainingTime = timeLimit;
            StartCoroutine(UpdateTimerLoop());
        }


        // Updates timer every in-game second until it reaches zero.
        private IEnumerator UpdateTimerLoop()
        {
            while (RemainingTime > 0f)
            {
                UpdateTimer();
                yield return new WaitForSeconds(1f);
            }

            Game.Instance.OutOfTimeEnding();
        }

        private void CheckForAnnoucement()
        {
            bool timeForAnnoucements;
            int currentTime = (int)Math.Round(RemainingTime);
            if (announcementTimes.Contains(currentTime)){
                timeForAnnoucements = true;
            }
            else
                timeForAnnoucements = false;

            if (timeForAnnoucements)
            {
                speakerManager.TimerChooseAudio(currentTime);

            }
        }

        private void UpdateTimer()
        {
            float elapsedTime = Time.timeSinceLevelLoad - startTime;
            RemainingTime = Mathf.Max(timeLimit - elapsedTime, 0);
            int minutes = Mathf.FloorToInt(RemainingTime / 60);
            int seconds = Mathf.FloorToInt(RemainingTime) - 60 * minutes;
            RemainingTimeString = minutes.ToString("D2") + ":" + seconds.ToString("D2");
            timerTMP.text = RemainingTimeString;
            CheckForAnnoucement();
        }


        /// <summary>
        /// This function adds 4 random times for and annoucement to be played
        /// </summary>
        private void RandomAnnouncementTimes()
        {
            int max = 840;
            int min = 780;
            int RandomTime;

            for (int i = 0; i < 4; i++)
            {
                if(max == 720)
                {
                    announcementTimes.Add(RandomTime = (UnityEngine.Random.Range(max, min)));
                    max = max - (60 * 3);
                    min = min - (60 * 3);
                }
                else
                {
                    announcementTimes.Add(RandomTime = (UnityEngine.Random.Range(max, min)));
                    max = max - (60 * 2);
                    min = min - (60 * 2);
                }
            }
        }
    }
}
