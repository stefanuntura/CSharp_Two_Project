using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation
{
    class PlayerEffect
    {
        public String Title;
        public bool GoodEffect;
        public int TimeSpan;
        public PlayerEffect(String effectString, bool goodEffect, int timeSpan)
        {
            Title = effectString;
            GoodEffect = goodEffect;
            TimeSpan = timeSpan;
        }
    }
}
