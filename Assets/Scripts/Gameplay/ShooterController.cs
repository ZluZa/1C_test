using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private bool _ableToShoot = true;

    public void Shoot(float damage, float speed, float delay, Transform spawnParent)
    {
        if (_ableToShoot)
        {
            var bullet = Instantiate(_bulletPrefab, _startPoint);
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localScale = Vector3.one;
            bullet.OnSpawn(damage, speed);
            bullet.transform.SetParent(spawnParent);
            _ableToShoot = false;
            StartCoroutine(ShootDelay(delay));
        }
    }

    public IEnumerator ShootDelay(float delay)
    {
        while (!_ableToShoot)
        {
            yield return new WaitForSeconds(delay);
            _ableToShoot = true;
            yield return new WaitForEndOfFrame();
        }
    }
}