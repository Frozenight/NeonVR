using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGun : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private float transitionDuration;

    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 0.5f;
    [SerializeField] private BulletImpact effectObject;

    private float elapsedTime = 0f;

    private float reloadTime = 3f;

    private bool _canShoot = false;

    private void Start()
    {
        LoadWeapon();
    }

    public void Shoot(Transform player)
    {
        if (!_canShoot)
            return;
        ShootBullet(player);
        _canShoot = false;
        Invoke("LoadWeapon", reloadTime);
    }

    private void ShootBullet(Transform player)
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bullet.blastOffEffectObj = effectObject;
        if (bulletRigidbody != null)
        {
            // Calculate the direction towards the player's feet
            Vector3 feetPosition = new Vector3(player.position.x, bulletSpawnPoint.position.y, player.position.z);
            Vector3 directionToFeet = (feetPosition - bulletSpawnPoint.position).normalized;

            // Apply force in the direction of the feet
            bulletRigidbody.AddForce(directionToFeet * bulletSpeed, ForceMode.Impulse);
        }
    }

    private void LoadWeapon()
    {
        StartCoroutine(ChargeVFX());
    }


    private IEnumerator ChargeVFX()
    {
        elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canShoot = true;
    }
}
