using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (collision.gameObject.name.Contains("Enemy")) 
            cameraShake.Play();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit(); 
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            gameObject.SetActive(false);
    }
}
