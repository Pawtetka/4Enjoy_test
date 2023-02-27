using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class DailyBonusService : MonoBehaviour
    {
        [SerializeField] private int firstDayPoints = 2;
        [SerializeField] private int secondDayPoints = 3;

        public int CountCoinsValue()
        {
            return RecursiveCount(CalculateDayNumber());
        }

        private int CalculateDayNumber()
        {
            return 0;
        }

        private int RecursiveCount(int day)
        {
            if (day == 1) return firstDayPoints;
            if (day == 2) return secondDayPoints;

            int firstValue = RecursiveCount(day - 2);
            var secondValue = RecursiveCount(day - 1) * 0.6f;

            return  Convert.ToInt32(Math.Round(firstValue + secondValue, 0, MidpointRounding.AwayFromZero));
        }
    }
}