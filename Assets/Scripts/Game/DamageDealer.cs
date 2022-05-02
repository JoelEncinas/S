using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public int GetDamage()
    {
        return damage;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
