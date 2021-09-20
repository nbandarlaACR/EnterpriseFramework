using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.Framework.GenericLib
{
    public class DateUtil
    {

        public static String GetCurrentMonth_MMM_Format()
        {
            DateTime now = DateTime.Now;
            return now.ToString("MMM");           
        }
        public static String GetNextMonth_MMM_Format()
        {
            DateTime now = DateTime.Now;
            now = now.AddMonths(1);
            return now.ToString("MMM");           
        }
        public static string GetNextMonthDate()
        {          
            DateTime now = DateTime.Now;
            Console.WriteLine("current date 2 is " + now.ToString().Split(" ")[0]);
            now = now.AddMonths(1);
            Console.WriteLine("next month dateis " + now.ToString().Split(" ")[0]);
            DateTime dateFormat = now;
            string format = "dd/MM/yyyy";
            string nextMonthDate = dateFormat.ToString(format);            
            return nextMonthDate;
            //return now.ToString().Split(" ")[0];
        }
    }
}
