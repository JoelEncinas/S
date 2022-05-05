using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Gameobjects
    Health playerHealth;
    List<Transform> healthObjects;
    Transform healthContainer;
    GameObject warningImage;

    // Variables
    [SerializeField] private int health;
    [SerializeField] private int counter;

    // Dialogue window
    GameObject dialogueWindow;
    Dialogue dialogueManager;

    void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        healthContainer = GameObject.Find("HealthContainer").GetComponent<Transform>();
        warningImage = GameObject.Find("Warning");
        dialogueWindow = GameObject.Find("AllyMessageWrapper");
        dialogueManager = GameObject.Find("Message").GetComponent<Dialogue>();

        health = playerHealth.GetHealth();
        counter = 1;
        healthObjects = new List<Transform>();
        GetHealthObjects();
    }

    private void Update()
    {
        if (dialogueManager.isDone)
        {
            dialogueWindow.SetActive(false);
            dialogueManager.isDone = false;
        }
    }

    private void GetHealthObjects()
    {
        for(int i = 0; i < health; i++)
            healthObjects.Add(healthContainer.transform.GetChild(i));
    }

    public void LoseHealth(int damage)
    {
        if(damage > health)
        {
            for (int i = 0; i < healthObjects.Count; i++)
                healthObjects[i].gameObject.SetActive(false);
        }
        if(damage == 1)
        {
            healthObjects[health - counter].gameObject.SetActive(false);
            counter++;
        } 
    }

     public IEnumerator FlashWarning()
    {
        warningImage.GetComponent<Image>().enabled = true;

        float animationTime = 4f;
        float fadeTime = 1f;
        float animationCounter = 0;

        do
        {
            warningImage.GetComponent<Image>().CrossFadeAlpha(0, fadeTime, false);
            yield return new WaitForSeconds(fadeTime);

            warningImage.GetComponent<Image>().CrossFadeAlpha(1, fadeTime, false);
            yield return new WaitForSeconds(fadeTime);

            animationCounter += 2f;
        } while (animationCounter <= animationTime);

        warningImage.GetComponent<Image>().CrossFadeAlpha(0, fadeTime, false);
        warningImage.GetComponent<Image>().enabled = false;
    }
}
