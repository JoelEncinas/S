using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Laser"))
            collision.gameObject.SetActive(false);
    }
}
