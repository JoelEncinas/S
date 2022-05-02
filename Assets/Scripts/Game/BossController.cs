using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // UI

    // Components
    GameObject focus;

    private void Awake()
    {
        SetupUI();
    }

    void Start()
    {
        focus.SetActive(false);
    }

    void Update()
    {
        
    }

    private void SetupUI()
    {
        focus = GameObject.Find("Focus");
    }
}
