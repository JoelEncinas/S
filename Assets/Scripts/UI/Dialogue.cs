using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    List<Transform> charactersFrame;
    private float typeSpeed = 0.15f;
    TextMeshProUGUI messageText;
    private string text;
    public bool isDone = false;

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
        SetFrame("enemy");

        charactersFrame = new List<Transform>();
        GetAllCharacters();
        messageText = GetComponent<TextMeshProUGUI>();

        text = DialogueDB.dialoguesDictionary["Mission01"];
        // TODO take text length to calc the yield time
        Debug.Log(text.Length);
        // TODO adapt for other characters
        GetCharacterByName("Robot").GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(ShowText("Robot"));
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

    IEnumerator ShowText(string name)
    {
        for(int i = 0; i < text.Length + 1; i++)
        {
            messageText.text = text.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        GetCharacterAnimator(GetCharacterByName(name)).PlayInFixedTime(name, -1, 0f);
        GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = false;
        GetCharacterByName(name).GetComponent<SpriteRenderer>().enabled = true;
        GetCharacterAnimator(GetCharacterByName(name)).enabled = false;

        yield return new WaitForSeconds(1f);
        isDone = true;
    }

    private void SetFrame(string faction)
    {
        if (faction.Contains("allied"))
        {
            midFrame.sprite = framesList[0];
            leftFrame.sprite = framesList[1];
            rightFrame.sprite = framesList[2];
        }
        if (faction.Contains("enemy"))
        {
            midFrame.sprite = framesList[3];
            leftFrame.sprite = framesList[4];
            rightFrame.sprite = framesList[5];
        }
    }
}
