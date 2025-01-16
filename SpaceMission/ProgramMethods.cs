using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using SpaceMission.Core;
using SpaceMission.Enums;
using Spectre.Console;

namespace SpaceMission;

public class ProgramMethods
{
    protected static bool isGameRunning = true;
    private static string welcomeText = "Welcome Pilot!\nSelect Your Ship";

    private static string titleText = @"                                           ____                                    __  __   _               _                 
                                          / ___|   _ __     __ _    ___    ___    |  \/  | (_)  ___   ___  (_)   ___    _ __  
                                          \___ \  | '_ \   / _` |  / __|  / _ \   | |\/| | | | / __| / __| | |  / _ \  | '_ \ 
                                           ___) | | |_) | | (_| | | (__  |  __/   | |  | | | | \__ \ \__ \ | | | (_) | | | | |
                                          |____/  | .__/   \__,_|  \___|  \___|   |_|  |_| |_| |___/ |___/ |_|  \___/  |_| |_|
                                                  |_|
";
    protected static void StartGame()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        TitleTypewriterEffect(titleText);
        Console.ResetColor();

        Thread.Sleep(100);

        Console.WriteLine("|------------ MAIN MENU ------------|");

        var input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {"Start", "Exit"})
        );

        switch (input)
        {
            case "Start":
                Console.Clear();
                StartScreen();
                break;
            case "Exit":
                isGameRunning = false;
                Console.Clear();
                break;
        }
    }

    protected static void StartScreen()
    {
        TypewriterEffect(welcomeText);
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<SpaceshipType>()
                .AddChoices(SpaceshipType.Fighter, SpaceshipType.Scout, SpaceshipType.Cargo)
        );

        TypewriterEffect("Give your ship a name: ");
        string name = Console.ReadLine()!;

        if (name is null or "" or " ")
        {
            TypewriterEffect("Okay then, You will keep the old name for your ship.");

            switch (type)
            {
                case SpaceshipType.Fighter:
                    name = "The Pitbull";
                    break;
                case SpaceshipType.Scout:
                    name = "The Eagle";
                    break;
                case SpaceshipType.Cargo:
                    name = "The Steet";
                    break;
            }
        }

        Spaceship spaceship = new(type, name!);
    }
    private static void TypewriterEffect(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            Console.Write(input[i]);
            Thread.Sleep(60);
        }
    }
    private static void TitleTypewriterEffect(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            Console.Write(input[i]);
            Thread.Sleep(10);
        }
    }
    private static void WaitingEffect()
    {
        bool inputReceived = false;
        int dotCount = 0;
        
        var inputTask = Task.Run(() =>
        {
            var ship = AnsiConsole.Prompt(
                new SelectionPrompt<SpaceshipType>()
                    .AddChoices(SpaceshipType.Fighter, SpaceshipType.Scout, SpaceshipType.Cargo)
            );
            inputReceived = true;
        });

        while (!inputReceived)
        {
            Console.Write(".");
            dotCount++;

            if (dotCount == 3)
            {
                Task.Delay(500).Wait();
                Console.Write("\b\b\b   \b\b\b");
                dotCount = 0;
            }

            Task.Delay(500).Wait();
        }

        inputTask.Wait();
    }
}