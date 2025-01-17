using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Diagnostics;
using SpaceMission.Core;
using SpaceMission.Enums;
using Spectre.Console;
using System.Xml.Schema;

namespace SpaceMission;

public class ProgramMethods
{
    protected static bool isGameRunning = true;
    private static string welcomeText = "Welcome Pilot!\nSelect The Type Of Your Spaceship";

    private static string titleText = @"                                                   ____                                    __  __   _               _                 
                                                  / ___|   _ __     __ _    ___    ___    |  \/  | (_)  ___   ___  (_)   ___    _ __  
                                                  \___ \  | '_ \   / _` |  / __|  / _ \   | |\/| | | | / __| / __| | |  / _ \  | '_ \ 
                                                   ___) | | |_) | | (_| | | (__  |  __/   | |  | | | | \__ \ \__ \ | | | (_) | | | | |
                                                  |____/  | .__/   \__,_|  \___|  \___|   |_|  |_| |_| |___/ |___/ |_|  \___/  |_| |_|
                                                          |_|
";
    protected static void StartGame()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        TitleTypewriterEffect($"\t{titleText}");
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

    private static void StartScreen()
    {
        TypewriterEffect($"{welcomeText}\n");
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<SpaceshipType>()
                .AddChoices(SpaceshipType.Fighter, SpaceshipType.Scout, SpaceshipType.Cargo)
        );

        Console.SetCursorPosition(0, 1);

        TypewriterEffect("Give your ship a name:\t\t\t\b\b\b\b\b\b\b\b\b\b\b");
        string name = Console.ReadLine()!;

        if (name is null or "" or " ")
        {
            Console.SetCursorPosition(0, 1);
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
        
        Console.SetCursorPosition(0, 1);
        TypewriterEffect("Great! You are all set. Let`s send you to the space!");

        Thread.Sleep(1000);
        Console.Clear();

        Progress();

        Thread.Sleep(1000);

        TypewriterEffect("\nIgnition!");

        Thread.Sleep(1000);

        Console.Clear();

        Countdown();

        Spaceship spaceship = new(type, name!);
    }
    private static void TypewriterEffect(string input)
    {
        Random rnd = new();

        for (int i = 0; i < input.Length; i++)
        {
            Console.Write(input[i]);

            if (input[i] == '.' || input[i] == ',' || input[i] == '!')
                Thread.Sleep(180);
            else if (input[i] == ' ')
                Thread.Sleep(50);
            else
                Thread.Sleep(90);
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
        int dotCount = 0;
        int animationDuration = 4000;
        int elapsed = 0;
        int delay = 500;
        
        while (elapsed < animationDuration)
        {
            Console.Write('.');
            dotCount++;

            if (dotCount == 3)
            {
                Thread.Sleep(delay);
                Console.Write("\b\b\b   \b\b\b");
                dotCount = 0;
            }

            Thread.Sleep(delay);
            elapsed += delay;
        }

        Console.Write("\r   \r");
    }

    protected static void GameScreen()
    {
        var panel = new Panel("Hello");
        panel.Header = new PanelHeader("------------ GAME MENU ------------");
        panel.Border = BoxBorder.Heavy;

        AnsiConsole.Write(panel);
    }

    protected static void MainMenu()
    {

    }

    private static void Progress()
    {
        AnsiConsole.Write(
            new FigletText("Systems Check")
            .Centered()
            .Color(Color.Green));

        Thread.Sleep(1000);

        AnsiConsole.Progress()
        .Start(ctx => 
        {
            // Define tasks
            var task1 = ctx.AddTask("[green]Safety Systems Check[/]");
            var task2 = ctx.AddTask("[green]Weapon Systems Check[/]");
            var task3 = ctx.AddTask("[green]Engine System Check[/]");
            var task4 = ctx.AddTask("[green]Life Support Systems Check[/]");

            while(!ctx.IsFinished) 
            {
                if (!task1.IsFinished) task1.Increment(1.5);
                if (!task2.IsFinished) task2.Increment(0.9);
                if (!task3.IsFinished) task3.Increment(0.8);
                if (!task4.IsFinished) task4.Increment(1.0);

                Thread.Sleep(100);
            }
        });

        AnsiConsole.MarkupLine("[bold green]All systems are operational![/]");
    }
    private static void Countdown()
    {
        for (int i = 10; i > -1; i--)
        {
            AnsiConsole.Write(
                new FigletText(i.ToString())
                .Centered()
                .Color(Color.Green)
            );

            Thread.Sleep(1000);
            Console.Clear();   
        }
    } 
}