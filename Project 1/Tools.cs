using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class Tools
    {
        public DateTime convertStringDate(string date)
        {
            DateTime newDate;
            DateTime.TryParse(date, out newDate);
          
            return newDate;
        }
        public bool checkDates(DateTime sDate, DateTime eDate)
        {
            bool correctInterval = true;

            if(eDate < sDate)
            {
                return !correctInterval;
            }
            return correctInterval;
        }
        public bool checkDateFormat(string date)
        {
            DateTime dDate;
            bool correctFormat = true;
 
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,  out dDate))
            {
                return correctFormat;
            }
            return !correctFormat;
        }
        public DateTime getCurrentDate()
        {
            DateTime today = DateTime.Today;
            return today;
        }

    }
}
