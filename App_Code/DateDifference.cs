using System;
using System.Collections.Generic;
using System.Text;


    public class DateDifference
    {
        /// <summary>
        /// defining Number of days in month; index 0=> january and 11=> December
        /// february contain either 28 or 29 days, that's why here value is -1
        /// </summary>
        private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        /// <summary>
        /// contain from date
        /// </summary>
        private DateTime fromDate;

        /// <summary>
        /// contain To Date
        /// </summary>
        private DateTime toDate;

        /// <summary>
        /// this three variable for output representation..
        /// </summary>
        private int year;
        private int month;
        private int day;
        string strMonths = "";
        public DateDifference(DateTime d1, DateTime d2)
        {
            int increment;
            this.fromDate = d2;
            this.toDate = d1;
            increment = 0;

            if (this.fromDate.Day > this.toDate.Day)
            {
                increment = this.monthDay[this.fromDate.Month - 1];
            }
            /// if it is february month
            /// if it's to day is less then from day
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(this.fromDate.Year))
                {
                    // leap year february contain 29 days
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {
                day = (this.toDate.Day + increment) - this.fromDate.Day;
                increment = 1;
            }
            else
            {
                day = this.toDate.Day - this.fromDate.Day;
            }
            ///month calculation
            if ((this.fromDate.Month + increment) > this.toDate.Month)
            {
                this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);
                increment = 1;
            }
            else
            {
                this.month = (this.toDate.Month) - (this.fromDate.Month + increment);
                increment = 0;
            }
            /// year calculation
            this.year = (this.toDate.Year - (this.fromDate.Year + increment)) * 12;
            this.month = this.year + this.month; strMonths = this.month + " Months ";
        }
        public override string ToString()
        {
            return strMonths;
        }
        public int Years
        {
            get
            {
                return this.year;
            }
        }
        public int Months
        {
            get
            {
                return this.month;
            }
        }
        public int Days
        {
            get
            {
                return this.day;
            }
        }
    }