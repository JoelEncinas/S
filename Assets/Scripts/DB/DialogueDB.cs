using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDB 
{
    public enum Factions
    {
        ALLY,
        ENEMY
    }

    public enum AllyRaces {
        HUMAN,
        MARTIAN,
        PRINCESS,
        WARRIOR,
        ROBOT,
        DROID,
        BUG
    }

    public enum EnemyRaces
    {
        ALIEN,
        MUTANT,
        ORION,
        CRAB
    }

    public readonly static Dictionary<string, string> dialoguesDictionary =
        new Dictionary<string, string>
        {
            {"Mission01", "Be careful commander. Sector X23-F has been attacked recently."}
        };

    public readonly static Dictionary<string, Dictionary<string, string>> missionsDictionary =
    new Dictionary<string, Dictionary<string, string>>
    {
        { "Mission00", new Dictionary<string, string>
            {
                {"title", "Training"},
                {"content", " Cadet combat simulation. "},
            }
        },
        { "Mission01", new Dictionary<string, string>
            {
                {"title", "Mission 01"},
                {"content", " Reconnaissance and Surveillance of Sector X23-F. "},
            }
        }
    };
}
