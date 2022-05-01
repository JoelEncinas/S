using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // variables
    [SerializeField] private Vector3 shootPosition;
    [SerializeField] private Vector3 shootPositionOffset = new Vector3(0, 1f, 0);
    [SerializeField] private float fireRate = 0.5f;

    // components
    [SerializeField] private GameObject projectile;
    private Transform playerTransform;

    IEnumerator Start()
    {
        playerTransform = GetComponent<Transform>();
        yield return new WaitForSeconds(fireRate);

        AutoShoot();
    }

    private void Update()
    {
        shootPosition = (Vector3)playerTransform.position + shootPositionOffset;
    }

    private void AutoShoot()
    {
        StartCoroutine(IAutoShoot());
    }

    IEnumerator IAutoShoot()
    {
        projectile = ObjectPool.sharedInstance.GetPooledObject();
        if(projectile != null)
        {
            projectile.transform.position = shootPosition;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        }
        
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(IAutoShoot());
    }
}
