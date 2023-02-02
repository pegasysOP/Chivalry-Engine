using SFML.Graphics;
using SFML.Window;

namespace ChivalryEngineCore
{
    public abstract class ChivalryEngine
    {
        private Vector2 windowSize = new Vector2(512, 512);
        private string windowTitle = "New Game";
        private RenderWindow? window = null;
        private Thread? gameLoop = null;

        public Color BackgroundColour = Color.White;

        public ChivalryEngine(Vector2 windowSize, string windowTitle)
        {
            this.windowSize = windowSize;
            this.windowTitle = windowTitle;

            gameLoop = new Thread(GameLoop);
            gameLoop.Start();
        }

        private void GameLoop()
        {
            OnLoad();

            window = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), windowTitle);
            window.Closed += (sender, e) => window.Close(); // Closes window when x is pressed

            while (window.IsOpen)
            {
                OnUpdate();

                window.DispatchEvents();
                window.Clear(BackgroundColour);
                window.Display();

                Thread.Sleep((int)(1f/5f * 1000));
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
    }
}
