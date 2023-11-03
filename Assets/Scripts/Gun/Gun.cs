using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private Material indicationMat;
    [SerializeField] private EmissionChanger[] changers;
    [SerializeField] private float transitionDuration;
    [SerializeField] private GameObject chargeVFX;

    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 0.5f;
    [SerializeField] private BulletImpact effectObject;

    private float endScale = 0.05f;

    private float elapsedTime = 0f;

    private float reloadTime = 3f;

    private bool _canShoot = false;

    private void Start()
    {
        chargeVFX.SetActive(false);
        LoadWeapon();
        //Invoke("Shoot", 8);
    }

    public void Shoot()
    {
        if (!_canShoot)
            return;
        ShootBullet();
        _canShoot = false;
        indicationMat.color = Color.red;
        DischargeMaterials();
        Invoke("LoadWeapon", reloadTime);
    }

    private void ShootBullet()
    {
        // Instantiate a bullet at the bulletSpawnPoint's position and rotation
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Add force to shoot the bullet forward (you may need to adjust the force value)
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bullet.blastOffEffectObj = effectObject;
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse); // Adjust the force as needed
        }
    }


    private void DischargeMaterials()
    {
        foreach (EmissionChanger item in changers)
        {
            item.Discharge();
        }
        chargeVFX.SetActive(false);
    }
    
    private void LoadWeapon()
    {
        StartCoroutine(StartTransitions());
    }

    private IEnumerator StartTransitions()
    {
        foreach (EmissionChanger item in changers)
        {
            item.StartTransition();

            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(ChargeVFX());
    }


    private IEnumerator ChargeVFX()
    {
        chargeVFX.transform.localScale = Vector3.zero;
        chargeVFX.SetActive(true);
        elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            float lerpedScale = Mathf.Lerp(0, endScale, t);
            chargeVFX.transform.localScale = new Vector3(lerpedScale, lerpedScale, lerpedScale);
            yield return null;
        }
        indicationMat.color = Color.green;
        _canShoot = true;
    }
}