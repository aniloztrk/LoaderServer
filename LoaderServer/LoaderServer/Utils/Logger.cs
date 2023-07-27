namespace LoaderServer.Utils;

public class Logger
{
    public static void Warn(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        PrintDate();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("WARN");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("]");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("> ");
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void Print(string prefix, string message, ConsoleColor color = ConsoleColor.Gray)
    {
        PrintDate();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write(prefix);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("]");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("> ");
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void Print(string prefix, string message, ConsoleColor messageColor, ConsoleColor prefixColor)
    {
        PrintDate();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("[");
        Console.ForegroundColor = prefixColor;
        Console.Write(prefix);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("]");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("> ");
        Console.ForegroundColor = messageColor;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void Print(string prefix, string message, ConsoleColor messageColor, ConsoleColor prefixColor, ConsoleColor bracketsColor)
    {
        PrintDate();
        Console.ForegroundColor = bracketsColor;
        Console.Write("[");
        Console.ForegroundColor = prefixColor;
        Console.Write(prefix);
        Console.ForegroundColor = bracketsColor;
        Console.Write("]");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("> ");
        Console.ForegroundColor = messageColor;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void PrintDate()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"[{DateTime.Now.ToString("t")}] ");
        Console.ResetColor();
    }
    
    public static void PrintCommandPrefix()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("> ");
        Console.ResetColor();
    }
}