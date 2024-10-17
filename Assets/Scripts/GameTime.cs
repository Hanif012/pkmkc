using UnityEngine;

public partial class GameTime
{

        public int hour;
        public int minute;

        public GameTime(int hour, int minute)
        {
            this.hour = hour;
            this.minute = minute;
        }

        public void AddTime(int hour, int minute)
        {
            this.hour += hour;
            this.minute += minute;
            if (this.minute >= 60)
            {
                this.minute -= 60;
                this.hour++;
            }
        }

        public void GetTime()
        {            
            Debug.Log(hour + ":" + minute);
        }
}
