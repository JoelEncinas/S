using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;
    CameraShake cameraShake;
    UIManager uIManager;
    PlayerController player;

    private void Awake()
    {
        if (gameObject.name.Contains("Player"))
        {
            cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
            uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (collision.gameObject.name.Contains("Enemy") 
            || collision.gameObject.name.Contains("Boss"))
        {
            PlayerHit(damageDealer.GetDamage());
        }

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            if (!collision.gameObject.name.Contains("Boss"))
                damageDealer.Destroy();   
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            gameObject.SetActive(false);
    }

    void PlayerHit(int damage)
    {
        cameraShake.Play();
        player.IMakePlayerImmune();
        if (health > 0)
            uIManager.LoseHealth(damage);
    }
}
