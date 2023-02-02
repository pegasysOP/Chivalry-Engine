namespace ChivalryEngineCore
{
    public class Log
    {
        public static void Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"msg> {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"wrn> {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"err> {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
