using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    List<Transform> charactersFrame;
    private float typeSpeed = 0.15f;
    TextMeshProUGUI messageText;
    private string text;

    private void Awake()
    {
        charactersFrame = new List<Transform>();
        GetAllCharacters();
        messageText = GetComponent<TextMeshProUGUI>();

        text = DialogueDB.dialoguesDictionary["Mission01"];
        GetCharacterByName("Human").GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(ShowText());
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

    IEnumerator ShowText()
    {
        for(int i = 0; i < text.Length + 1; i++)
        {
            messageText.text = text.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        GetCharacterAnimator(GetCharacterByName("Human")).Play("Human", -1, 0f);
        GetCharacterAnimator(GetCharacterByName("Human")).enabled = false;
    }
}
