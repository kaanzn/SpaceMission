using Spectre.Console;
using SpaceMission.Core;
using SpaceMission.Enums;
using System.Text;

namespace SpaceMission;

public class Program : ProgramMethods
{
    public static void Main()
    {
        Console.Clear();
        // Setting the console
        Console.Title = "Space Mission";
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.Unicode;

        //Screen size check
        do
        {
            if (Console.WindowHeight < 50 || Console.WindowWidth < 101)
            {
                Console.SetCursorPosition(0, Console.WindowHeight/2 - 1);
                AnsiConsole.Write(new Align(new Panel(new Markup("Wrong Size")), HorizontalAlignment.Center, VerticalAlignment.Middle));

            }
            else
                break;
            Thread.Sleep(1000);
        }
        while (true);

        Console.Clear();

        StartGame();
        // Game will be implemented
    }
}