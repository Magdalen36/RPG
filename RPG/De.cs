using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class De
    {
        public int Maximum { get;}
        public int Minimum { get;}

        public int Lance(int min, int max)
        {
            Random random = new Random();
            int roll = random.Next(min, max);

            return roll;
        }

    }
}
