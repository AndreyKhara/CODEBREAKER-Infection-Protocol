using UnityEngine;
using Cysharp.Threading.Tasks;
using System;


namespace CDB.Character.Enemy
{
    public class CorruptByte : EnemyBase
    {
        [SerializeField] private Transform _trf;
        [SerializeField] private Transform _playerTrf;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _stopDistance;
        [SerializeField] private float _coolDawnAttack = 1.2f;
        private bool _coolDawn = false;


        // TO DO : FindObjectWithType
        protected override void Start()
        {
            base.Start();
            _playerTrf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_coolDawn)
            {
                if (other.CompareTag("Player"))
                {
                    _coolDawn = true;
                    Attack(other.GetComponent<IHealth>());
                    StartCoolDawn().Forget();
                }
            }
        }

        

        private async UniTask StartCoolDawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_coolDawnAttack), ignoreTimeScale: false);
            _coolDawn = false;
        }


        private void FixedUpdate()
        {
            Vector3 dir = _playerTrf.position - _trf.position;
            dir.y = 0f;
            float dist = dir.magnitude;
            if (dist > _stopDistance)
            {
                // поворот
                if (dir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(dir.normalized);
                    _trf.rotation = Quaternion.RotateTowards(_trf.rotation, targetRot, _rotateSpeed * Time.fixedDeltaTime);
                }

                Vector3 newPos = _rb.position + _trf.forward * Speed * Time.fixedDeltaTime;
                _rb.MovePosition(newPos);

            }
        }
    }
}

