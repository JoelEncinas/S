using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    List<Transform> charactersFrame;
    private float typeSpeed = 0.05f; // tiome between each character is typed (lower number = faster type)
    TextMeshProUGUI messageText;
    private string text;
    public bool isDone = true; // CHANGE TO ACTIVATE DIALOGUE
    CanvasGroup canvasGroup;

    // text frames
    [SerializeField] private List<Sprite> framesList;
    Image midFrame;
    Image leftFrame;
    Image rightFrame;

    private void Awake()
    {
        // text frames
        midFrame = GameObject.Find("MessageWrapperMid").GetComponent<Image>();
        leftFrame = GameObject.Find("MessageWrapperLeft").GetComponent<Image>();
        rightFrame = GameObject.Find("MessageWrapperRight").GetComponent<Image>();
        canvasGroup = midFrame.GetComponent<CanvasGroup>();

        charactersFrame = new List<Transform>();
        GetAllCharacters();
        messageText = GetComponent<TextMeshProUGUI>();
    }

    private void GetAllCharacters()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            charactersFrame.Add(transform.GetChild(i));
        }
    }

    private Transform GetCharacterByName(string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (name.Contains(transform.GetChild(i).name))
                return transform.GetChild(i);
        }

        return null;
    }

    private Animator GetCharacterAnimator(Transform gameObject)
    {
        return gameObject.GetComponent<Animator>();
    }

    public float ShowMessage(string message, string name, bool hasToFadeOut)
    {
        text = message;
        SetFrame(SetNameByFaction(name));
        GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(ShowText(name, hasToFadeOut));

        // calc stage flow
        Debug.Log(CalculateTextTime(text.Length));
        return CalculateTextTime(text.Length);
    }

    private float CalculateTextTime(int length)
    {
        return text.Length * typeSpeed + 2f;
    }

    IEnumerator ShowText(string name, bool hasToFadeOut)
    {
        GetCharacterAnimator(GetCharacterByName(name)).enabled = true;
        GetCharacterAnimator(GetCharacterByName(name)).PlayInFixedTime(name, -1, 0f);

        for (int i = 0; i < text.Length + 1; i++)
        {
            messageText.text = text.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        GetCharacterAnimator(GetCharacterByName(name)).PlayInFixedTime(name, -1, 0f);
        GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = false;
        GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = true;
        GetCharacterAnimator(GetCharacterByName(name)).enabled = false;

        if(hasToFadeOut)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(FadeOutDialogue(name));
            yield return new WaitForSeconds(1f);
            GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = false;

            isDone = true;
            ResetFadeOut(name);
        }
    }

    private string SetNameByFaction(string name)
    {
        if (name.Contains(DialogueDB.AllyRaces.HUMAN.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.MARTIAN.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.PRINCESS.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.ROBOT.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.WARRIOR.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.DROID.ToString()) ||
            name.Contains(DialogueDB.AllyRaces.BUG.ToString()))
        {
            return DialogueDB.Factions.ALLY.ToString();
        }
        if (name.Contains(DialogueDB.EnemyRaces.ALIEN.ToString()) ||
            name.Contains(DialogueDB.EnemyRaces.CRAB.ToString()) ||
            name.Contains(DialogueDB.EnemyRaces.MUTANT.ToString()) ||
            name.Contains(DialogueDB.EnemyRaces.ORION.ToString()))
        {
            return DialogueDB.Factions.ENEMY.ToString();
        }

        return null;
    }

    private void SetFrame(string faction)
    {
        if (faction.Contains(DialogueDB.Factions.ALLY.ToString()))
        {
            midFrame.sprite = framesList[0];
            leftFrame.sprite = framesList[1];
            rightFrame.sprite = framesList[2];
        }
        if (faction.Contains(DialogueDB.Factions.ENEMY.ToString()))
        {
            midFrame.sprite = framesList[3];
            leftFrame.sprite = framesList[4];
            rightFrame.sprite = framesList[5];
        }
    }

    IEnumerator FadeOutDialogue(string name)
    {
        float counter = 0;
        float animationTime = 1f;
        float alphaValue = 1;

        do
        {
            canvasGroup.alpha = alphaValue;
            GetCharacterByName(name).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);
            yield return new WaitForSeconds(0.2f);
            alphaValue -= 0.2f;
            counter += 0.2f;

        } while (counter < animationTime);
    }

    private void ResetFadeOut(string name)
    {
        canvasGroup.alpha = 1;
        GetCharacterByName(name).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
