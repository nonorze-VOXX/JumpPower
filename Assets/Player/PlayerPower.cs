using UnityEngine;

namespace Player
{
    internal enum Status
    {
        Idle,
        Jumping
    }

    //TODO goal detect
    //TODO falling max speed
    public class PlayerPower : MonoBehaviour
    {
        public PlayerData playerData;


        private bool _aWaJumped;
        private Collider2D _collider2D;
        private Vector2 _direction;
        private Transform _forceLocal;
        private RaycastHit2D _raycastHit2D;
        private RaycastHit2D _raycastHit2DDown;
        private Rigidbody2D _rigidbody2D;
        private Status _status;


        private void Start()
        {
            playerData.powerTime = 0;
            _forceLocal = transform.GetChild(1);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _status = Status.Jumping;
            playerData.gravityDirection = Vector2.down;
            _aWaJumped = true;
        }

        private void Update()
        {
            CheckMoveInput();
            CheckCollision();
            AddGravity();
            CheckStatus();
            // Stop();
        }

        private void CheckStatus()
        {
            if (_rigidbody2D.velocity != Vector2.zero)
                _status = Status.Jumping;
            else
                _status = Status.Idle;
        }


        private void AddGravity()
        {
            if (Vector2.Dot(_rigidbody2D.velocity, playerData.gravityDirection) < playerData.maxSpeed)
                _rigidbody2D.AddForce(playerData.gravityDirection * playerData.gravity);
        }

        private void CheckCollision()
        {
            // CheckCollisionDown();
            CollideWall(playerData.gravityDirection);
        }


        private bool CheckCollisionWall(Vector2 wallDirection)
        {
            Vector2 position = transform.position;
            var boundsExtents = wallDirection.x != 0 ? _collider2D.bounds.extents.x : _collider2D.bounds.extents.y;
            var hitDistance = boundsExtents + playerData.hitDistance;

            _raycastHit2D = Physics2D.Raycast(position, wallDirection, hitDistance,
                1 << LayerMask.NameToLayer("map"));

            // Debug.DrawRay(position, wallDirection * hitDistance, Color.green);
            return _raycastHit2D.collider;
        }


        private Vector2 Anticlockwise90deg(Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }

        private Vector2 GetCollideWallDirection(Vector2 groundDirection)
        {
            var rightWall = Anticlockwise90deg(groundDirection);
            var leftWall = rightWall * -1.0f;
            if (CheckCollisionWall(rightWall)) return rightWall;
            if (CheckCollisionWall(leftWall)) return leftWall;
            return Vector2.zero;
        }

        private void CollideWall(Vector2 groundDirection)
        {
            var speed = _rigidbody2D.velocity;
            var collisionDirection = GetCollideWallDirection(groundDirection);
            if (collisionDirection == Vector2.zero) return; // didn't collide the wall

            if (Vector2.Dot(speed, collisionDirection) > 0)
                _rigidbody2D.velocity = Vector2.Reflect(speed, collisionDirection * -0.8f);
        }


        private void CheckMoveInput()
        {
            if (Input.GetKey(KeyCode.LeftControl) && _status == Status.Idle)
            {
                //normal move
                if (Input.GetKey(KeyCode.A)) _rigidbody2D.velocity = -Anticlockwise90deg(playerData.gravityDirection);
                else if (Input.GetKey(KeyCode.D))
                    _rigidbody2D.velocity = Anticlockwise90deg(playerData.gravityDirection);
                else _rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                if (!Input.GetKey("w"))
                    _aWaJumped = false;
                if (Input.GetKey("w") && _status == Status.Idle)
                {
                    if (!_aWaJumped)
                    {
                        _direction = transform.position - _forceLocal.position;
                        playerData.powerTime += Time.deltaTime;
                        if (playerData.powerTime > 1.5) Jump();
                    }
                }
                else if (!Input.GetKey("w") && _status == Status.Idle)
                {
                    if (playerData.powerTime < 0.3 && playerData.powerTime > 0)
                    {
                        playerData.powerTime = 0;
                        Jump();
                    }
                    else
                    {
                        if (playerData.powerTime != 0) Jump();
                    }

                    playerData.powerTime = 0;
                }
            }
        }

        private void Jump()
        {
            //new jump
            var xSpeed = Vector2.zero;
            if (playerData.angle < playerData.nowGravityAngleLockNull)
                xSpeed = 5 * Anticlockwise90deg(playerData.gravityDirection);
            else if (playerData.angle > playerData.nowGravityAngleLockNull)
                xSpeed = -5 * Anticlockwise90deg(playerData.gravityDirection);

            var speed = -playerData.gravityDirection *
                        (playerData.baseSpeed + playerData.powerTime * playerData.timeForce) +
                        xSpeed;
            _rigidbody2D.velocity = speed;

            //old junp
            // var speed = _rigidbody2D.velocity;
            // speed = _direction * (playerData.powerTime * playerData.timeForce + playerData.baseSpeed);
            // _rigidbody2D.velocity = speed;
            playerData.powerTime = 0;
            _aWaJumped = true;
        }

        private void Stop()
        {
            if (CheckCollisionDown() && _status == Status.Jumping)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _status = Status.Idle;
            }
        }

        private bool CheckCollisionDown()
        {
            _raycastHit2DDown = Physics2D.Raycast(transform.position, playerData.gravityDirection,
                _collider2D.bounds.extents.y + playerData.hitDownDistance,
                1 << LayerMask.NameToLayer("map"));
            if (_raycastHit2DDown.collider)
                return true;
            return false;
        }
    }
}