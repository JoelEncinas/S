using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDB 
{
    public enum AllyRaces {
        HUMAN,
        MARTIAN,
        PRINCESS,
        WARRIOR,
        ROBOT,
        DROID
    }

    public enum EnemyRaces
    {
        ALIEN,
        MUTANT,
        ORION,
        CRAB,
        BUG
    }

    public readonly static Dictionary<string, string> dialoguesDictionary =
        new Dictionary<string, string>
        {
            {"Mission01", "Be careful commander. Sector X23-F has been attacked recently."}
        };
}
