using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // variables
    private float trackTime = 5f;
    private float spawnTime = 3f;
    private float trackAttack = 10f;
    private bool isTracking;
    private List<string> attacks;
    private Vector2 playerPosition;

    // Components
    GameObject focus;
    int health;

    // Gameobjects
    PlayerController player;

    // Attacks DB
    private string attack1 = "TrackPlayer";

    private void Awake()
    {
        SetupUI();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        health = GetComponent<Health>().health;

        LoadAttacks();
    }

    void Start()
    {
        focus.SetActive(false);
        isTracking = false;

        // start combat
        StartCoroutine(Engage());
    }

    void Update()
    {
        if (isTracking)
            playerPosition = player.transform.position;
    }

    private void SetupUI()
    {
        focus = GameObject.Find("Focus");
    }

    private void LoadAttacks()
    {
        attacks.Add(attack1);
    }

    // Engage with player
    private void IEnagage()
    {
        StartCoroutine(Engage());
    }

    IEnumerator Engage()
    {
        int randomAttack;
        float timeUntilNextAttack;

        yield return new WaitForSeconds(spawnTime);

        while(health > 0)
        {
            randomAttack = Random.Range(0, attacks.Count);
            timeUntilNextAttack = DoRandomAttack(randomAttack);

            yield return new WaitForSeconds(timeUntilNextAttack);
        }
    }

    private float DoRandomAttack(int randomAttack)
    {
        switch (randomAttack)
        {
            case 1:
                ITrackPlayer();
                return trackAttack;

            // TODO more attacks
        }

        return 0f;
    }

    // Attacks
    private void ITrackPlayer()
    {
        StartCoroutine(TrackPlayer());
    }

    IEnumerator TrackPlayer()
    {
        yield return new WaitForSeconds(trackTime);
    }
}
