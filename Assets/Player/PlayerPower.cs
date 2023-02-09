using UnityEngine;

namespace Player
{
    public class PlayerPower : MonoBehaviour
    {
        private Vector2 _direction;
        private Transform _forceLocal;
        private bool _isGround;
        private float _powerTime;
        private Rigidbody2D _rigidbody2D;
        private float baseTime;
        private float force;

        private void Start()
        {
            _forceLocal = transform.GetChild(1);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            force = 100;
            baseTime = 10;
            _isGround = false;
        }

        private void Update()
        {
            CheckJumpInput();
            StopCheck();
        }

        private void StopCheck()
        {
            if (_isGround)
            {
                Vector2 velocity = _rigidbody2D.velocity;
                velocity.x = 0;
                _rigidbody2D.velocity = velocity;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("ground"))
                _isGround = true;
            else
                _isGround = false;
        }

        private void CheckJumpInput()
        {
            if (Input.GetKey("w"))
            {
                _direction = transform.position - _forceLocal.position;
                _powerTime += Time.deltaTime;
            }
            else
            {
                if (_powerTime != 0)
                {
                    var pushForce = _direction * ((_powerTime + baseTime) * force);
                    _rigidbody2D.AddForce(pushForce);
                }

                _powerTime = 0;
            }
        }
    }
}