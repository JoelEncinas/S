using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        Debug.Log(collision.gameObject.name);
    }
}
