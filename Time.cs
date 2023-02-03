using SFML.System;

namespace ChivalryEngineCore
{
    public class Time
    {
        private Clock clock;
        private double timeScale = 1f;

        public double TimeScale { get { return timeScale; } set { timeScale = (value >= 0 ? value : 0d); } }
        public double DeltaTime { get; private set; } = 0f;
        public double TotalTimeElapsed { get; private set; } = 0f;

        public Time()
        {
            clock = new Clock();
        }

        public void Update()
        {
            double newTotalTimeElapsed = clock.ElapsedTime.AsMilliseconds() * TimeScale;
            DeltaTime = (newTotalTimeElapsed - TotalTimeElapsed) * TimeScale;
            TotalTimeElapsed = newTotalTimeElapsed;
        }
    }
}
