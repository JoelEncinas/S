using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

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
