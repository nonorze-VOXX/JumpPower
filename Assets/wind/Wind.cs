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
            main.startLifetime = (_colliderSize.x * Vector2.right).magnitude / _speed;
            var velocityOverLifetime = _particleSystem.velocityOverLifetime;
            SetParticleVelocity(velocityOverLifetime, Vector2.right, _speed);
            var shape = _particleSystem.shape;
            shape.scale = new Vector3(0, _colliderSize.y, 0);
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            print(other.tag);
            if (other.transform.CompareTag("Player"))
            {
                var direction = new Vector2(
                    Mathf.Cos(transform.rotation.eulerAngles.z / 180 * Mathf.PI),
                    Mathf.Sin(transform.rotation.eulerAngles.z / 180 * Mathf.PI));
                var rigibody = other.gameObject.GetComponent<Rigidbody2D>();
                var maxForce = rigibody.velocity.magnitude > 10 ? Vector2.zero : direction * Mathf.Abs(_speed) * 30;
                var nowForce = direction * Mathf.Abs(_speed - rigibody.velocity.magnitude) * 30;
                rigibody.AddForce(nowForce.magnitude > maxForce.magnitude ? maxForce : nowForce);
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