using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class DailyBonusService : MonoBehaviour
    {
        [SerializeField] private int firstDayPoints = 2;
        [SerializeField] private int secondDayPoints = 3;

        private const int COEF = 3;
        private const int LASTMONTH = 12;
        private const int FIRSTMONTH = 1;

        public long CountCoinsValue()
        {
            var dayNumber = CalculateDayNumber();
            //return RecursiveCount(3);
            return NonRecursiveCount(dayNumber);
        }

        private int CalculateDayNumber()
        {
            //We need 3, 6, 9 and 12 Months. So, we can check with %, only with number 3 on hardcode :)
            DateTime date = DateTime.Now.Date;
            int resultMonth = 0;
            int resultYear = date.Year;

            var offset = date.Month % COEF;
            if (offset == 0) return date.Day;

            if (date.Month < COEF)
            {
                resultMonth = LASTMONTH;
                resultYear--;
            }
            else resultMonth = date.Month - offset;

            return (date - new DateTime(resultYear, resultMonth, 1)).Days + 1;
        }

        private int RecursiveCount(int day)
        {
            if (day == 1) return firstDayPoints;
            if (day == 2) return secondDayPoints;

            int firstValue = RecursiveCount(day - 2);
            var secondValue = RecursiveCount(day - 1) * 0.6f;

            return  Convert.ToInt32(Math.Round(firstValue + secondValue, 0, MidpointRounding.AwayFromZero));
        }

        private long NonRecursiveCount(int day)
        {
            if (day == 1) return firstDayPoints;
            if (day == 2) return secondDayPoints;
            
            long firstValue = firstDayPoints;
            long secondValue = secondDayPoints;

            for (int i = 3; i <= day; i++)
            {
                var iValue = Convert.ToInt64(Math.Round(firstValue + secondValue * 0.6, 0, MidpointRounding.AwayFromZero));
                if (i == day) return iValue;

                firstValue = secondValue;
                secondValue = iValue;
            }

            return 0;
        }
    }
}