using UnityEngine;

namespace Player
{
    internal enum Wall
    {
        Left = -1,
        Right = 1
    }

    //TODO player rotation when gravity change
    //TODO goal detect
    //TODO picture test
    public class PlayerPower : MonoBehaviour
    {
        public PlayerData playerData;
        private Collider2D _collider2D;
        private Vector2 _direction;
        private Transform _forceLocal;
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
            playerData.gravityDirection = Vector2.down;
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
            _rigidbody2D.AddForce(playerData.gravityDirection * playerData.gravity);
        }

        private void CheckCollision()
        {
            CheckCollisionDown();

            CheckCollisionWall(playerData.gravityDirection, (float)Wall.Left);
            CheckCollisionWall(playerData.gravityDirection, (float)Wall.Right);
        }


        private void CheckCollisionWall(Vector2 mode, float direction)
        {
            var detectDirection = TransGravityToCollisionDirection(mode);
            var position = transform.position;
            if (detectDirection.Equals(Vector2.right))
                _raycastHit2D = Physics2D.Raycast(position, detectDirection * direction,
                    _collider2D.bounds.extents.x + playerData.hitDistance,
                    1 << LayerMask.NameToLayer("map"));
            else
                _raycastHit2D = Physics2D.Raycast(position, detectDirection * direction,
                    _collider2D.bounds.extents.y + playerData.hitDistance,
                    1 << LayerMask.NameToLayer("map"));

            // Debug.DrawRay(transform.position, detectDirection * (direction *
            //     _collider2D.bounds.extents.x + playerData.hitDistance), Color.green);
            if (_raycastHit2D.collider) CollideWall(detectDirection, direction);
        }

        private Vector2 TransGravityToCollisionDirection(Vector2 mode)
        {
            if (mode == Vector2.down)
                return Vector2.right;
            if (mode == Vector2.right)
                return Vector2.up;
            if (mode == Vector2.up)
                return Vector2.right;
            return mode == Vector2.left ? Vector2.up : Vector2.down;
        }

        private void CollideWall(Vector2 mode, float direction)
        {
            var speed = _rigidbody2D.velocity;
            if (mode == Vector2.right)
            {
                if (speed.x * direction > 0)
                {
                    speed.x *= -playerData.collideWallSpeedDelta;
                    _rigidbody2D.velocity = speed;
                }
            }
            else
            {
                if (speed.y * direction > 0)
                {
                    speed.y *= -playerData.collideWallSpeedDelta;
                    _rigidbody2D.velocity = speed;
                }
            }
        }

        private void CheckCollisionDown()
        {
            _raycastHit2DDown = Physics2D.Raycast(transform.position, Vector2.down,
                _collider2D.bounds.extents.y,
                1 << LayerMask.NameToLayer("map"));
            if (_raycastHit2DDown.collider)
            {
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