using System;
using System.Collections;
using DG.Tweening;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Enemy : FactoryObject
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ParticleSystem onKillVfx;
        [SerializeField] private ParticleSystem onBorderAchieveVfx;
        [SerializeField] private ParticleSystem onHitVfx;
        
        public UnityEvent onBorderAchieve;
        public UnityEvent onEnemyKilled;

        private float _speed;
        private float _hp;

        private Coroutine actionCoroutine;

        public override FactoryObject Init(BaseData data)
        {
            EnemyFactoryData eData = (EnemyFactoryData) data;
            _speed = Random.Range(eData.EnemySpeed.x, eData.EnemySpeed.y);
            _hp = Random.Range(eData.EnemyHp.x, eData.EnemyHp.y);
            GoToPlayer();
            return base.Init(data);
        }

        protected virtual void GoToPlayer()
        {
            actionCoroutine = StartCoroutine(ActionCoroutine());
            IEnumerator ActionCoroutine()
            {
                var walkSpeed = _speed / 10;
                while (true)
                {
//                    Debug.Log(_speed);
                    transform.Translate(new Vector3(0, -walkSpeed));
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.layer)
            {
                case 14:
                   OnHit(other.GetComponent<Bullet>());
                    break;
                case 13:
                    OnBorderAchieved();
                    break;
            }
        }

        private void OnBorderAchieved()
        {
            if (actionCoroutine != null)
                StopCoroutine(actionCoroutine);
            if (onBorderAchieveVfx != null)
                Instantiate(onBorderAchieveVfx);
            onBorderAchieve.Invoke();
        }

        private void OnHit(Bullet bullet)
        {
            _hp -= bullet.GetDamage();
            if (onHitVfx != null)
                Instantiate(onHitVfx);
            if (_hp <= 0)
                OnKill();
            Destroy(bullet.gameObject);
        }
        private void OnKill()
        {
            if (actionCoroutine != null)
                StopCoroutine(actionCoroutine);
            if (onKillVfx != null)
                Instantiate(onKillVfx);
            onEnemyKilled.Invoke();
            Destroy(gameObject);
        }
        
        
    }
