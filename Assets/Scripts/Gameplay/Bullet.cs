using System;
using UnityEngine;

namespace Gameplay
{
    public class Bullet: MonoBehaviour
    {
        private float _bulletDamage;
        private float _bulletSpeed;
        private Vector2 _startPosition;

        private float _distanceThreshold = 200f;
        
        public void OnSpawn(float damage, float speed)
        {
            _bulletDamage = damage;
            _bulletSpeed = speed;
            _startPosition = transform.position;
        }

        private void Update()
        {
            transform.Translate(new Vector3(0, _bulletSpeed));
            var distance = Vector2.Distance(_startPosition, transform.position);
            if (distance >= _distanceThreshold)
                Destroy(gameObject);
        }

        public float GetDamage()
        {
            return _bulletDamage;
        }
    }
}