using UnityEngine;

namespace Player
{
    public class PlayerPower : MonoBehaviour
    {
        public PlayerData playerData;
        private Collider2D _collider2D;
        private Vector2 _direction;
        private Transform _forceLocal;
        private Vector2 _gravityDirection;
        private bool _isGround;
        private float _powerTime;
        private RaycastHit2D _raycastHit2D;
        private RaycastHit2D _raycastHit2DDown;
        private Rigidbody2D _rigidbody2D;

        private float force;


        private void Start()
        {
            _forceLocal = transform.GetChild(1);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            force = 1;
            _isGround = false;
            _gravityDirection = Vector2.down;
        }

        private void Update()
        {
            CheckJumpInput();
            StopCheck();
            CheckCollision();
            AddGravity();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.transform.name);
            Debug.Log(collision.transform.position);
            if (collision.transform.tag.Equals("ground"))
                _isGround = true;
            else
                _isGround = false;
        }

        private void AddGravity()
        {
            _rigidbody2D.AddForce(_gravityDirection * playerData.gravity);
        }

        private void CheckCollision()
        {
            CheckCollisionDown();
            CheckCollisionWall(-1);
            CheckCollisionWall(1);
        }


        private void CheckCollisionWall(float direction)
        {
            _raycastHit2D = Physics2D.Raycast(transform.position, Vector2.right * direction,
                _collider2D.bounds.extents.x + playerData.hitDistance,
                1 << LayerMask.NameToLayer("map"));
            Debug.DrawRay(transform.position, Vector2.right * (direction *
                _collider2D.bounds.extents.x + playerData.hitDistance), Color.green);
            if (_raycastHit2D.collider)
            {
                Debug.Log(_raycastHit2D.transform.name);
                Debug.Log("wall touch");
                CollideWall(direction);
            }
        }

        private void CollideWall(float direction)
        {
            var speed = _rigidbody2D.velocity;
            if (speed.x * direction > 0)
            {
                speed.x *= -playerData.collideWallSpeedDelta;
                _rigidbody2D.velocity = speed;
            }
        }

        private void CheckCollisionDown()
        {
            _raycastHit2DDown = Physics2D.Raycast(transform.position, Vector2.down,
                _collider2D.bounds.extents.y,
                1 << LayerMask.NameToLayer("map"));
            if (_raycastHit2DDown.collider)
            {
                Debug.Log("ground touch");
                Debug.Log(_raycastHit2DDown.transform.name);
            }
        }

        private void StopCheck()
        {
            if (_isGround)
            {
                var velocity = _rigidbody2D.velocity;
                velocity.x = 0;
                _rigidbody2D.velocity = velocity;
            }
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
                    var speed = _rigidbody2D.velocity;
                    speed = _direction * ((_powerTime + playerData.baseSpeed) * force);
                    _rigidbody2D.velocity = speed;
                }

                _powerTime = 0;
            }
        }
    }
}