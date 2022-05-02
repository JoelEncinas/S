using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // variables
    private float spawnTime = 3f;
    [SerializeField] private float trackAttack = 5f;
    [SerializeField] private bool isTracking;
    [SerializeField] private bool isLocked;
    [SerializeField] private float focusSpeed = 20f;
    Vector2 focusInitialPosition;

    // lasers
    List<GameObject> lasers;
    List<Color32> laserColors;
    private int laserColorCounter;
    private int lastLaserCounter;
    Vector2 laserLeftInitialPosition;
    Vector2 laserRightInitialPosition;

    private List<string> attacks;

    // Components
    [SerializeField] private GameObject focus;
    SpriteRenderer focusColor;
    int health;

    // Gameobjects
    PlayerController player;

    // Attacks DB
    private string attack1 = "TrackPlayer";

    private void Awake()
    {
        SetupBoss();

        attacks = new List<string>();
        LoadAttacks();
    }

    void Start()
    {
        focus.SetActive(false);
        isTracking = false;
        isLocked = false;

        // start combat
        IEnagage();
    }

    void Update()
    {
        if (isTracking)
            FocusPlayer(player.transform.position);

        LockPlayer();
    }

    private void SetupBoss()
    {
        focus = GameObject.Find("Focus");
        focusColor = focus.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        health = GetComponent<Health>().health;
        lasers = new List<GameObject>
        {
            GameObject.Find("BossLaserSpawnerLeft"),
            GameObject.Find("BossLaserSpawnerRight")
        };
        DisableLasers();
        CreateLaserColors();
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
        focusInitialPosition = focus.transform.position;
        laserLeftInitialPosition = lasers[0].transform.position;
        laserRightInitialPosition = lasers[1].transform.position;

        while (health > 0)
        {
            randomAttack = Random.Range(0, attacks.Count);
            timeUntilNextAttack = DoRandomAttack(randomAttack);

            yield return new WaitForSeconds(timeUntilNextAttack);
            ResetTrackPlayer();
        }
    }

    private float DoRandomAttack(int randomAttack)
    {
        switch (randomAttack)
        {
            case 0:
                StartCoroutine(TrackPlayer());
                return trackAttack;

            // TODO more attacks
        }

        return 0f;
    }

    // Attacks
    IEnumerator TrackPlayer()
    {
        // Attack time 5f

        isTracking = true;
        focus.SetActive(true);
        yield return new WaitForSeconds(2f);

        isTracking = false;
        isLocked = true;
        yield return new WaitForSeconds(1f);
        focus.SetActive(false);

        EnableRandomLaser();
    }

    private void ResetTrackPlayer()
    {
        focus.transform.position = focusInitialPosition;
        isTracking = false;
        isLocked = false;
        focus.SetActive(false);
        ResetLasers();
    }

    private void FocusPlayer(Vector2 playerPosition)
    {
        float delta = focusSpeed * Time.deltaTime;
        focus.transform.position = Vector2.MoveTowards(focus.transform.position, playerPosition, delta);
    }

    private void LockPlayer()
    {
        if (isLocked)
            focusColor.color = Color.red;
        else
            focusColor.color = Color.green;
    }

    private void ResetLasers()
    {
        lasers[0].SetActive(false);
        lasers[1].SetActive(false);

        lasers[0].transform.position = laserLeftInitialPosition;
        lasers[1].transform.position = laserRightInitialPosition;
    }

    private void DisableLasers()
    {
        lasers[0].SetActive(false);
        lasers[1].SetActive(false);
    }

    private void EnableRandomLaser()
    {
        int randomLaser = Random.Range(0, lasers.Count);
        int randomColor;
        int laserAttack;

        do
        {
            randomColor = Random.Range(0, laserColors.Count);
        } while (randomColor == laserColorCounter);

        laserColorCounter = randomColor;

        do
        {
            laserAttack = Random.Range(0, 2);
        } while (laserAttack == lastLaserCounter);

        lastLaserCounter = laserAttack;
        
        lasers[randomLaser].SetActive(true);
        lasers[randomLaser].GetComponent<SpriteRenderer>().color = laserColors[randomColor];

        switch (laserAttack)
        {
            case 0:
                lasers[randomLaser].transform.position = focus.transform.position;
                break;
            case 1:
                lasers[randomLaser].transform.right = focus.transform.position - lasers[randomLaser].transform.position;
                break;
        }
    }

    private void CreateLaserColors()
    {
        laserColorCounter = 0;
        laserColors = new List<Color32>
        {
            new Color32(255, 121, 48, 255),
            new Color32(255, 219, 162, 255),
            new Color32(97, 211, 227, 255),
            new Color32(113, 227, 146, 255)
        };
    }
}
