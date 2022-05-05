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

    public readonly static Dictionary<string, string> tutorial =
        new Dictionary<string, string>
        {
            {"Part1", "Welcome to the trainning program cadet. Here you will learn the basic protocols in order to survive out there."},
            {"Part2", "You can steer the spaceship with AWSD keys. Try it out!"},
            {"Part3", "The main cannon is completely automatic so you don't have to worry too much about that."},
            {"Part4", "Enough talking! Let's see how skilled you are! Watch out!"}
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
