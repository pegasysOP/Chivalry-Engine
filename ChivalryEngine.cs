using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ChivalryEngineCore
{
    public abstract class ChivalryEngine
    {
        private Vector2 windowSize = new Vector2(512, 512);
        private string windowTitle = "New Game";

        private RenderWindow window = new RenderWindow(new VideoMode(512, 512), "New Game");
        private Thread gameLoop;
        private Time gameTime;


        public double LogicFrameTime { get; private set; } = 0;
        public double DrawFrameTime { get { return gameTime.DeltaTime; } }
        public double GameTimer { get { return gameTime.TotalTimeElapsed; } }
        
        public Color BackgroundColour { get; set; } = Color.White;

        public ChivalryEngine(Vector2 windowSize, string windowTitle)
        {
            this.windowSize = windowSize;
            this.windowTitle = windowTitle;

            gameTime = new Time();
            gameLoop = new Thread(GameLoop);
            gameLoop.Start();
        }

        private const float LOGIC_FRAMES_PER_SECOND = 50f;
        private void GameLoop()
        {
            OnLoad();

            window = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), windowTitle);
            window.Closed += (sender, e) => window.Close(); // Closes window when x is pressed

            double timeUntilNextFrame = 0f;
            double timeOfLastFrame = 0f;
            while (window.IsOpen)
            {
                window.DispatchEvents();

                gameTime.Update();
                timeUntilNextFrame -= gameTime.DeltaTime;
                while (timeUntilNextFrame <= 0)
                {
                    timeUntilNextFrame = 1f / LOGIC_FRAMES_PER_SECOND * 1000;
                    LogicFrameTime = gameTime.TotalTimeElapsed - timeOfLastFrame;
                    timeOfLastFrame = gameTime.TotalTimeElapsed;

                    OnUpdate();
                }

                window.Clear(BackgroundColour);
                OnDraw();
                window.Display();
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
