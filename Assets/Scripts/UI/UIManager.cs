using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Gameobjects
    Health playerHealth;
    List<Transform> healthObjects;
    Transform healthContainer;

    // Variables
    [SerializeField] private int health;
    [SerializeField] private int counter;

    void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        healthContainer = GameObject.Find("HealthContainer").GetComponent<Transform>();

        health = playerHealth.health;
        counter = 1;
        healthObjects = new List<Transform>();
        GetHealthObjects();
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
}

