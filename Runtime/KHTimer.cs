using System;
using UnityEngine;

namespace KH
{
    [Serializable]
    public class KHTimer
    {
        private const double TIMER_MAX_VALUE = double.MaxValue - 100;
        private double timer = 0;

        /// <summary>Called on Update() to run the timer</summary>
        public void Update()
        {
            // Prevents the timer from overflowing and becoming negative. The 100 is just a buffer to prevent it from getting too close to double.MaxValue, which could cause issues with the DidExceed() method.
            if (timer < TIMER_MAX_VALUE) 
            {
                timer += Time.deltaTime;
            }
        }

        /// <returns>True: if the timer exceeded the <paramref name="duration"/></returns>
        public bool DidExceed(double duration)
        {
            if (duration < 0)
            {
                KHDebug.LogWarning("KHTimer.DidExceed() was given a negative duration value. It will always return true.");
                return true;
            }
            else if (duration > TIMER_MAX_VALUE)
            {
                KHDebug.LogWarning("KHTimer.DidExceed() was given a duration value that exceeded double.MaxValue. It will always return false.");
                return false;
            }

            return timer >= duration;
        }

        /// <summary>
        /// Restarts the timer and optionally gives it a headstart. A headstart is a value that the timer will start at instead of 0. For example, if you want the timer to start at 0.5 seconds, you would give it a headstart of 0.5. This can be useful for things like cooldowns, where you want the timer to start at a certain point instead of 0.
        /// </summary>
        /// <param name="timerHeadstart"></param>
        public void ResetTimer(double timerHeadstart = 0)
        {
            if (timerHeadstart < 0)
            {
                timer = 0;
                KHDebug.LogWarning("KHTimer.Restart() was given a negative timerHeadstart value. It has been set to 0 instead.");
            }
            else if (timerHeadstart > TIMER_MAX_VALUE)
            {
                timer = double.MaxValue;
                KHDebug.LogWarning("KHTimer.Restart() was given a timerHeadstart value that exceeded double.MaxValue. It has been set to double.MaxValue instead.");   
            }
            else
            {
                timer = 0 + timerHeadstart;
            }
        }
    }
}