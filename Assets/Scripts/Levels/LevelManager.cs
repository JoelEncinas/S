using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Dialogue dialogueManager;
    float counter = 0;

    // player health
    Health playerHealth;
    [SerializeField] private int health;

    private void Awake()
    {
        dialogueManager = GameObject.Find("Message").GetComponent<Dialogue>();

        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0f);
        if(SceneManager.GetActiveScene().name.Contains("Mission00"))
            StartCoroutine(Tutorial());
    }

    private void Update()
    {
        health = playerHealth.GetHealth();
    }

    IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(2f);

        counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part1"], DialogueDB.AllyRaces.HUMAN.ToString(), false);
        yield return new WaitForSeconds
            (dialogueManager.ShowMessage(DialogueDB.tutorial["Part1"], DialogueDB.AllyRaces.HUMAN.ToString(), false));

        counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part2"], DialogueDB.AllyRaces.HUMAN.ToString(), false);
        yield return new WaitForSeconds(
            dialogueManager.ShowMessage(DialogueDB.tutorial["Part2"], DialogueDB.AllyRaces.HUMAN.ToString(), false));

        counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part3"], DialogueDB.AllyRaces.HUMAN.ToString(), false);
        yield return new WaitForSeconds(
            dialogueManager.ShowMessage(DialogueDB.tutorial["Part3"], DialogueDB.AllyRaces.HUMAN.ToString(), false));

        counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part4"], DialogueDB.AllyRaces.HUMAN.ToString(), true);
        yield return new WaitForSeconds(
            dialogueManager.ShowMessage(DialogueDB.tutorial["Part4"], DialogueDB.AllyRaces.HUMAN.ToString(), false));

        Debug.Log(counter);

        // 28f then spawn enemy

        yield return new WaitForSeconds(10f);

        if (health < 3)
        {
            counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part5"], DialogueDB.AllyRaces.HUMAN.ToString(), false);
            yield return new WaitForSeconds(
                dialogueManager.ShowMessage(DialogueDB.tutorial["Part5"], DialogueDB.AllyRaces.HUMAN.ToString(), false));
        }
        else
        {
            counter += dialogueManager.ShowMessage(DialogueDB.tutorial["Part6"], DialogueDB.AllyRaces.HUMAN.ToString(), false);
            yield return new WaitForSeconds(
                dialogueManager.ShowMessage(DialogueDB.tutorial["Part6"], DialogueDB.AllyRaces.HUMAN.ToString(), false));
        }
    }
}
