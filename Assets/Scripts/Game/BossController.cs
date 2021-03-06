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
    private List<string> attacks;

    // lasers
    List<GameObject> lasers;
    List<Color32> laserColors;
    [SerializeField] private int laserAttack;
    Vector2 laserLeftInitialPosition;
    Vector2 laserRightInitialPosition;
    [SerializeField] private List<SpriteRenderer> orbs;
    [SerializeField] private GameObject orbsGO;

    // Spawn droids
    [SerializeField] private GameObject pathContainer;
    [SerializeField] private List<Transform> pathsList;
    [SerializeField] private GameObject droid;
    int randomPath;
    bool isDroidActive = true;

    // Components
    [SerializeField] private GameObject focus;
    SpriteRenderer focusColor;
    Health health;
    int healthValue;

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

        healthValue = health.GetHealth();
        DisableOrbs();
    }

    private void SetupBoss()
    {
        // components
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        health = GetComponent<Health>();

        // focus
        focus = GameObject.Find("Focus");
        focusColor = focus.GetComponent<SpriteRenderer>();

        // paths
        pathContainer = GameObject.Find("AddPaths");
        pathsList = new List<Transform>();
        AddPaths();

        // droid
        droid = GameObject.Find("Enemy_boss1_add");
        droid.SetActive(false);

        // lasers
        lasers = new List<GameObject>
        {
            GameObject.Find("BossLaserSpawnerLeft"),
            GameObject.Find("BossLaserSpawnerRight")
        };

        // orbs
        orbsGO = GameObject.Find("Orbs");
        GetAllOrbs();

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
        SpawnDroid();
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

        while (healthValue > 0)
        {
            randomAttack = Random.Range(0, attacks.Count);
            timeUntilNextAttack = DoRandomAttack(randomAttack);

            yield return new WaitForSeconds(timeUntilNextAttack);
            randomPath = Random.Range(0, pathsList.Count);
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
        yield return new WaitForSeconds(0.35f);
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
        lasers[0].transform.rotation = Quaternion.identity;
        lasers[0].transform.rotation = Quaternion.identity;
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

        randomColor = Random.Range(0, GetColorBasedOffHealth());

        if (laserAttack == 1)
            laserAttack = 0;
        else
            laserAttack = 1;
        
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
        laserColors = new List<Color32>
        {
            new Color32(255, 121, 48, 255), // orange
            new Color32(255, 219, 162, 255), // yellow
            new Color32(97, 211, 227, 255), // blue
            new Color32(113, 227, 146, 255) // green
        };
    }

    void SpawnDroid()
    {
        // Time to travel all points +- 20f
        droid.SetActive(true);
        droid.GetComponent<GravityPull>().enabled = true;
        randomPath = Random.Range(0, pathsList.Count);
        droid.transform.position = pathsList[randomPath].transform.GetChild(0).transform.position;
    }

    private void AddPaths()
    {
        for (int i = 0; i < pathContainer.transform.childCount; i++)
        {
            pathsList.Add(pathContainer.transform.GetChild(i));
        }
    }

    public List<Transform> GetCurrentPath()
    {
        List<Transform> waypoints = new List<Transform>();

        for (int i = 0; i < pathsList[randomPath].childCount; i++)
        {
            waypoints.Add(pathsList[randomPath].transform.GetChild(i));
        }

        return waypoints;
    }

    private int GetColorBasedOffHealth()
    {
        if (healthValue >= 120)
            return 4;
        if (healthValue >= 80 && healthValue < 120)
            return 3;
        if (healthValue >= 40 && healthValue < 80)
            return 2;
        if (healthValue >= 0 && healthValue < 40)
            return 1;

        return 0;
    }

    private void DisableOrbs()
    {
        if (healthValue < 120)
            orbs[3].color = new Color32(1, 1, 1, 0);
        if (healthValue < 80)
            orbs[2].color = new Color32(1, 1, 1, 0);
        if (healthValue < 40)
            orbs[1].color = new Color32(1, 1, 1, 0);
        if (healthValue < 10)
            orbs[0].color = new Color32(1, 1, 1, 0);
    }

    private void GetAllOrbs()
    {
        orbs = new List<SpriteRenderer>();
        for(int i = 0; i < 4; i++)
            orbs.Add(orbsGO.transform.GetChild(i).GetComponent<SpriteRenderer>());
    }
}
