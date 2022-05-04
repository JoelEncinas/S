using UnityEngine;

public class BossWarning : MonoBehaviour
{
    UIManager uIManager;

    private void Awake()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        StartCoroutine(uIManager.FlashWarning());
    }
}
