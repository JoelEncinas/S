using System.Collections;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    // variables
    [SerializeField] private Vector3 shootPosition;
    [SerializeField] private Vector3 shootPositionOffset = new Vector3(0, -1f, 0);
    [SerializeField] private float fireRate = 1f;

    // components
    [SerializeField] private GameObject projectile;
    private Transform enemyTransform;
    private string projectilePool = "pooledEnemyProjectiles";

    IEnumerator Start()
    {
        enemyTransform = GetComponent<Transform>();
        yield return new WaitForSeconds(fireRate);

        AutoShoot();
    }

    private void Update()
    {
        shootPosition = (Vector3)enemyTransform.position + shootPositionOffset;
    }

    private void AutoShoot()
    {
        StartCoroutine(IEAutoShoot());
    }

    IEnumerator IEAutoShoot()
    {
        projectile = ObjectPool.sharedInstance.GetPooledObject(projectilePool); 
        if (projectile != null)
        {
            projectile.transform.position = shootPosition;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        }

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(IEAutoShoot());
    }
}
