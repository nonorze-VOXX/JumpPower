using UnityEngine;

namespace Player
{
    //TODO goal detect
    public class PlayerPower : MonoBehaviour
    {
        public PlayerData playerData;
        public GameObject tpFlag;

        private bool _aWaJumped;
        private Collider2D _collider2D;
        private Vector2 _direction;
        private Transform _forceLocal;
        private RaycastHit2D _raycastHit2D;
        private RaycastHit2D _raycastHit2DDown;
        private Rigidbody2D _rigidbody2D;
        private float _saveCounter;
        private Vector2 tpLocation;


        private void Start()
        {
            tpLocation = Vector2.zero;
            _saveCounter = 0;
            playerData.powerTime = 0;
            _forceLocal = transform.GetChild(1);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            playerData.status = Status.Jumping;
            playerData.gravityDirection = Vector2.down;
            playerData.maxPowerTime = 1.5f;
            _aWaJumped = true;
            if (PlayerPrefs.GetFloat("savePointX") == 0)
            {
                //no played
                PlayerPrefs.SetInt("haveTP", 0);
                PlayerPrefs.SetFloat("savePointX", playerData.playerSafedPosition.x);
                PlayerPrefs.SetFloat("savePointY", playerData.playerSafedPosition.y);
                PlayerPrefs.Save();
                transform.position = playerData.playerSafedPosition;
            }
            else
            {
                //played
                var position = new Vector2(PlayerPrefs.GetFloat("savePointX"), PlayerPrefs.GetFloat("savePointY"));
                transform.position = position;
            }
        }

        private void Update()
        {
            Debug.Log(playerData.status);
            SavePoint();
            CheckMoveInput();
            CheckCollision();
            AddGravity();
            CheckStatus();
            // Stop();
        }


        private void SavePoint()
        {
            _saveCounter += Time.deltaTime;
            Debug.Log("savedX :" + PlayerPrefs.GetFloat("savePointX"));
            if (playerData.status == Status.Idle && _saveCounter > 1)
            {
                var position = transform.position;
                PlayerPrefs.SetFloat("savePointX", position.x);
                PlayerPrefs.SetFloat("savePointY", position.y);
                PlayerPrefs.Save();
                _saveCounter = 0;
            }
        }

        private void CheckStatus()
        {
            if (_rigidbody2D.velocity != Vector2.zero)
                playerData.status = Status.Jumping;
            else
                playerData.status = Status.Idle;
        }


        private void AddGravity()
        {
            if (Vector2.Dot(_rigidbody2D.velocity, playerData.gravityDirection) < playerData.maxSpeed)
                //gravity *deltaTime *timeFix
                _rigidbody2D.AddForce(playerData.gravityDirection * (playerData.gravity * Time.deltaTime * 57));
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
                _rigidbody2D.velocity = Vector2.Reflect(speed, collisionDirection * -playerData.collideWallSpeedDelta);
        }


        private void CheckMoveInput()
        {
            if (Input.GetKey(KeyCode.T) && playerData.status == Status.Idle)
                SaveTp();
            else if (Input.GetKey(KeyCode.P) && playerData.status == Status.Idle) Tp();
            if (Input.GetKey(KeyCode.LeftControl) && playerData.status == Status.Idle)
            {
                //normal move
                if (Input.GetKey(KeyCode.A)) _rigidbody2D.velocity = -Anticlockwise90deg(playerData.gravityDirection);
                else if (Input.GetKey(KeyCode.D))
                    _rigidbody2D.velocity = Anticlockwise90deg(playerData.gravityDirection);
                else _rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                if (!GetJumpInput())
                    _aWaJumped = false;
                if (GetJumpInput() && playerData.status == Status.Idle)
                {
                    if (!_aWaJumped)
                    {
                        _direction = transform.position - _forceLocal.position;
                        playerData.powerTime += Time.deltaTime;
                        if (playerData.powerTime > 1.5) Jump();
                    }
                }
                else if (!GetJumpInput() && playerData.status == Status.Idle)
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

        private bool GetJumpInput()
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
        }

        private void Jump()
        {
            if (playerData.freeAngle)
            {
                //old junp
                _direction = transform.position - _forceLocal.position;
                var speed = _rigidbody2D.velocity;
                speed = _direction * (playerData.powerTime * playerData.timeForce + playerData.baseSpeed);
                _rigidbody2D.velocity = speed;
            }
            else
            {
                var jumpPowerTime = playerData.powerTime;
                //new jump
                var xSpeed = Vector2.zero;
                if (playerData.angle < playerData.nowGravityAngleLockNull)
                    xSpeed = 5 * Anticlockwise90deg(playerData.gravityDirection);
                else if (playerData.angle > playerData.nowGravityAngleLockNull)
                    xSpeed = -5 * Anticlockwise90deg(playerData.gravityDirection);
                if (playerData.powerTime > playerData.maxPowerTime) jumpPowerTime = playerData.maxPowerTime;

                var speed = -playerData.gravityDirection *
                            (playerData.baseSpeed + jumpPowerTime * playerData.timeForce) +
                            xSpeed;
                _rigidbody2D.velocity = speed;
                Debug.Log(speed);
            }

            playerData.powerTime = 0;
            _aWaJumped = true;
        }

        private void Stop()
        {
            if (CheckCollisionDown() && playerData.status == Status.Jumping)
            {
                _rigidbody2D.velocity = Vector2.zero;
                playerData.status = Status.Idle;
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

        private void SaveTp()
        {
            if (PlayerPrefs.GetInt("haveTP") == 1)
            {
                var position = transform.position;
                tpLocation = position;
                tpFlag.transform.position = position;
            }
        }

        private void Tp()
        {
            if (!tpLocation.Equals(Vector2.zero) && PlayerPrefs.GetInt("haveTP") == 1) transform.position = tpLocation;
        }
    }
}