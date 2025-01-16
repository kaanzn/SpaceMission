using Microsoft.VisualBasic;
using SpaceMission.Enums;
using Spectre.Console;

namespace SpaceMission.Core;

internal static class MissionSystem
{
    internal static void CreateMission()
    { 
        Random rnd = new();
        int AvailableMissions = rnd.Next(2, 5);
        MissionType[] _missions = new MissionType[AvailableMissions];

        for (int i = 0; i < AvailableMissions; i++)
        {
            int missionType = rnd.Next(1, 5);
            switch (missionType)
            {
                case 1:
                    _missions[i] = MissionType.HuntEnemy;
                    break;
                case 2:
                    _missions[i] = MissionType.ScoutBase;
                    break;
                case 3:
                    _missions[i] = MissionType.Trade;
                    break;
                case 4:
                    _missions[i] = MissionType.Transportation;
                    break;
            }

        }
        var input = AnsiConsole.Prompt(
            new SelectionPrompt<MissionType>()
                .Title("Available Missions")

                .AddChoices(_missions)
         );
    }  
}