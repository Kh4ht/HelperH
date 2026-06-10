using UnityEngine;

namespace KH
{
    public static class KHTimeManager
    {
        private static float _realTimeScale = 1;
        private static float _multiplier = 1;

        public static void SetTimeScale(float value)
        {
            _realTimeScale = value;

            UpdateTimeScale();
        }

        public static void SetTimeScaleMultiplierForDebugging(float value)
        {
            _multiplier = value;

            UpdateTimeScale();
        }

        public static void Reset()
        {
            _realTimeScale = 1;
            _multiplier = 1;

            UpdateTimeScale();
        }

        private static void UpdateTimeScale()
        {
            Time.timeScale = _realTimeScale * _multiplier;
        }
    }
}