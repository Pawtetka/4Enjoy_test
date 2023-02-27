using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

namespace DefaultNamespace
{
    public class LivesService : MonoBehaviour
    {
        [SerializeField] private int livesMax;
        [SerializeField] private int livesRechargeIntervalInSeconds;
        
        public Action<string> TimerValueUpdated;
        public Action<int> LivesCountUpdated;
        
        private int _currentLivesCount;
        public int CurrentLivesCount => _currentLivesCount;
        public int LivesMax => livesMax;

        private DateTime nextLifeTime;
        private DateTime lastLifeTime;
        private bool isRestoring = false;
        private Coroutine restoringCoroutine;

        private const string LivesPlayerPref = "currentLives";
        private const string NextLifePlayerPref = "nextLifeTime";
        private const string LastLifePlayerPref = "lastLifeTime";

        public void Awake()
        {
            if (!PlayerPrefs.HasKey(LivesPlayerPref)) PlayerPrefs.SetInt(LivesPlayerPref, _currentLivesCount);
            LoadData();
        }

        public void Start()
        {
            if (restoringCoroutine != null) StopCoroutine(restoringCoroutine);
            restoringCoroutine = StartCoroutine(RestoreLive());
        }

        #region Public_Methods

        public void RefillLives()
        {
            UpdateLivesCount(livesMax);
            UpdateTimer();
            StopCoroutine(restoringCoroutine);
            isRestoring = false;
        }

        public void UseLife()
        {
            UpdateLivesCount(_currentLivesCount - 1);
        }

        #endregion

        #region Private_Methods

        private void UpdateLivesCount(int newValue)
        {
            if(newValue > livesMax || newValue < 0) return;
            
            _currentLivesCount = newValue;
            LivesCountUpdated?.Invoke(_currentLivesCount);

            if (!isRestoring)
            {
                nextLifeTime = AddDuration(DateTime.Now, livesRechargeIntervalInSeconds);
                if (restoringCoroutine != null) StopCoroutine(restoringCoroutine);
                restoringCoroutine = StartCoroutine(RestoreLive());
            }
        }

        private void UpdateTimer()
        {
            if (_currentLivesCount >= livesMax)
            {
                TimerValueUpdated?.Invoke("Full");
                return;
            }

            TimeSpan time = nextLifeTime - DateTime.Now;
            string timeValue = $"{time.Minutes:D2}:{time.Seconds:D2}";
            TimerValueUpdated?.Invoke(timeValue);
        }

        private void LoadData()
        {
            _currentLivesCount = PlayerPrefs.GetInt(LivesPlayerPref);
            nextLifeTime = StringToDateTime(PlayerPrefs.GetString(NextLifePlayerPref));
            lastLifeTime = StringToDateTime(PlayerPrefs.GetString(LastLifePlayerPref));
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(LivesPlayerPref, _currentLivesCount);
            PlayerPrefs.SetString(NextLifePlayerPref, nextLifeTime.ToString(CultureInfo.CurrentCulture));
            PlayerPrefs.SetString(LastLifePlayerPref, lastLifeTime.ToString(CultureInfo.CurrentCulture));
        }

        private DateTime StringToDateTime(string value)
        {
            if(String.IsNullOrEmpty(value)) return DateTime.Now;
            
            return DateTime.Parse(value);
        }

        private DateTime AddDuration(DateTime dateTime, int duration)
        {
            return dateTime.AddSeconds(duration);
        }

        private IEnumerator RestoreLive()
        {
            UpdateTimer();
            isRestoring = true;
            bool isLivesAdding = false;

            while (_currentLivesCount < livesMax)
            {
                DateTime currentDateTime = DateTime.Now;
                DateTime nextDateTime = nextLifeTime;
                isLivesAdding = false;

                while (currentDateTime > nextDateTime)
                {
                    if (_currentLivesCount < livesMax)
                    {
                        isLivesAdding = true;
                        UpdateLivesCount(_currentLivesCount + 1);
                        DateTime timeToAdd = lastLifeTime > nextDateTime ? lastLifeTime : nextDateTime;
                        nextDateTime = AddDuration(timeToAdd, livesRechargeIntervalInSeconds);
                    }
                    else break;
                }

                if (isLivesAdding)
                {
                    lastLifeTime = DateTime.Now;
                    nextLifeTime = nextDateTime;
                }
                
                UpdateTimer();
                SaveData();
                yield return null;
            }

            isRestoring = false;
        }

        #endregion
        
        
    }
}