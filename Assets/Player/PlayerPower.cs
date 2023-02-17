using UnityEngine;

namespace Player
{
    internal enum Wall
    {
        Left = -1,
        Right = 1
    }

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
            CollideWall(playerData.gravityDirection);
        }


        private bool CheckCollisionWall(Vector2 wallDirection)
        {
            Vector2 position = transform.position;
            float boundsExtents = wallDirection.x != 0 ?
                _collider2D.bounds.extents.x : _collider2D.bounds.extents.y;
            float hitDistance = boundsExtents + playerData.hitDistance;

            _raycastHit2D = Physics2D.Raycast(position, wallDirection, hitDistance,
                1 << LayerMask.NameToLayer("map"));

            // Debug.DrawRay(position, wallDirection * hitDistance, Color.green);
            return _raycastHit2D.collider;
        }

        private Vector2 anticlockwise90deg(Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }
        
        private Vector2 GetCollideWallDirection(Vector2 groundDirection)
        {
            Vector2 rightWall = anticlockwise90deg(groundDirection);
            Vector2 leftWall = rightWall * -1.0f;
            if(CheckCollisionWall(rightWall)) return rightWall;
            if(CheckCollisionWall(leftWall)) return leftWall;
            return Vector2.zero;
        }

        private void CollideWall(Vector2 groundDirection)
        {
            Vector2 speed = _rigidbody2D.velocity;
            Vector2 collisionDirection = GetCollideWallDirection(groundDirection);
            if (collisionDirection == Vector2.zero) return; // didn't collide the wall

            if (Vector2.Dot(speed, collisionDirection) > 0)
                _rigidbody2D.velocity = Vector2.Reflect(speed, collisionDirection * -1.0f);
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