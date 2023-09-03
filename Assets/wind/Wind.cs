using Unity.VisualScripting;
using UnityEngine;

namespace wind
{
    public class Wind : MonoBehaviour
    {
        [SerializeField] private Vector2 _colliderSize;
        [SerializeField] private float _speed;
        private BoxCollider2D _collider2D;

        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = transform.GetComponentInChildren<ParticleSystem>();
            _collider2D = transform.GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _collider2D.offset = _colliderSize / 2 * Vector2.right;
            _collider2D.size = _colliderSize;
            var main = _particleSystem.main;
            main.startLifetime = (_colliderSize * Vector2.right).normalized.magnitude / _speed;
            var velocityOverLifetime = _particleSystem.velocityOverLifetime;
            SetParticleVelocity(velocityOverLifetime, Vector2.right, _speed);
            var shape = _particleSystem.shape;
            shape.scale = _colliderSize - _colliderSize / 2 * Vector2.right.Abs();
            shape.position = _colliderSize / 4 * Vector2.right;
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            print(other.tag);
            if (other.transform.CompareTag("Player"))
            {
                var direction = new Vector2(
                    Mathf.Sin(transform.rotation.z),
                    Mathf.Cos(transform.rotation.z));
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 100);
            }
        }

        private void SetParticleVelocity(ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime,
            Vector2 direction, float speed)
        {
            var result = direction * speed;
            velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(result.x);
            velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(result.y);
        }
    }
}