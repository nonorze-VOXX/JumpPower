using UnityEngine;

namespace Player
{
    //TODO goal detect
    public class PlayerPower : MonoBehaviour
    {
        public PlayerData playerData;
        public GameObject tpFlag;

        private Collider2D _collider2D;
        private Vector2 _direction;
        private Transform _forceLocal;
        private bool _isPause;
        private Vector2 _pauseSpeed;
        private RaycastHit2D _raycastHit2D;
        private RaycastHit2D _raycastHit2DDown;
        private Rigidbody2D _rigidbody2D;
        private float _saveCounter;


        private void Start()
        {
            _isPause = false;
            _pauseSpeed = Vector2.zero;
            _saveCounter = 0;
            playerData.powerTime = 0;
            _forceLocal = transform.GetChild(1);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            playerData.status = Status.Jumping;
            playerData.gravityDirection = Vector2.down;
            PlayerPrefs.DeleteAll();
            if (PlayerPrefs.GetFloat("savePointX") == 0)
            {
                //no played
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
            AddGravity();
            if (!_isPause)
            {
                SavePoint();
                CheckMoveInput();
                CollideWall();
                CollideGround();
            }
        }


        private void SavePoint()
        {
            _saveCounter += Time.deltaTime;
            if (playerData.status == Status.Idle && _saveCounter > 1)
            {
                var position = transform.position;
                PlayerPrefs.SetFloat("savePointX", position.x);
                PlayerPrefs.SetFloat("savePointY", position.y);
                PlayerPrefs.Save();
                _saveCounter = 0;
            }
        }

        private void AddGravity()
        {
            if (_isPause) _rigidbody2D.velocity = Vector2.zero;
            if (Vector2.Dot(_rigidbody2D.velocity, playerData.gravityDirection) < playerData.maxSpeed &&
                _isPause == false)
                //gravity *deltaTime *timeFix
                _rigidbody2D.AddForce(playerData.gravityDirection * (playerData.gravity * Time.deltaTime * 57));
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

        private void CollideWall()
        {
            var speed = _rigidbody2D.velocity;
            var collisionDirection = GetCollideWallDirection(playerData.gravityDirection);
            if (collisionDirection == Vector2.zero) return; // didn't collide the wall

            if (Vector2.Dot(speed, collisionDirection) > 0)
                _rigidbody2D.velocity = Vector2.Reflect(speed, collisionDirection) * playerData.collideWallSpeedDelta;
        }

        private void teleportInput()
        {
            if (Input.GetKey(KeyCode.P))
                Tp();
        }

        private void walkInput()
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.velocity = -Anticlockwise90deg(playerData.gravityDirection);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.velocity = Anticlockwise90deg(playerData.gravityDirection);
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        private void focusInput()
        {
            if (playerData.powerTime > playerData.maxPowerTime)
            {
                Jump();
            }
            else if (GetJumpInput())
            {
                playerData.powerTime += Time.deltaTime;
            }
            else if (playerData.powerTime != 0)
            {
                if (0 < playerData.powerTime && playerData.powerTime < 0.3)
                    playerData.powerTime = 0;
                Jump();
            }
        }

        private void CheckMoveInput()
        {
            switch (playerData.status)
            {
                case Status.Idle:
                    teleportInput();
                    if (Input.GetKey(KeyCode.LeftControl)) playerData.status = Status.Walk;
                    if (GetJumpInput()) playerData.status = Status.Focus;
                    break;
                case Status.Walk:
                    walkInput();
                    if (!Input.GetKey(KeyCode.LeftControl)) playerData.status = Status.Idle;
                    break;
                case Status.Focus:
                    focusInput(); // state transition is wrapped in
                    break;
                case Status.Jumping:
                    if (_rigidbody2D.velocity == Vector2.zero && !GetJumpInput()) playerData.status = Status.Idle;
                    break;
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
            }

            playerData.powerTime = 0;
            playerData.status = Status.Jumping;
        }

        private void CollideGround()
        {
            var isFalling = Vector2.Dot(_rigidbody2D.velocity, playerData.gravityDirection) > 0;
            var isCollideGround = CheckCollisionWall(playerData.gravityDirection);
            if (isCollideGround && isFalling)
                _rigidbody2D.velocity = Vector2.zero;
        }


        private void Tp()
        {
            var target = tpFlag.transform.position;
            if (!target.Equals(Vector2.zero))
                transform.position = target;
        }

        private void Goal()
        {
            //TODO gravity disappear , maybe status = idle
            //TODO fire work 
        }

        public void Pause()
        {
            if (_isPause)
            {
                (_pauseSpeed, _rigidbody2D.velocity) = (_rigidbody2D.velocity, _pauseSpeed);
            }
            else
            {
                (_pauseSpeed, _rigidbody2D.velocity) = (_rigidbody2D.velocity, _pauseSpeed);
                _rigidbody2D.velocity = Vector2.zero;
            }

            _isPause = !_isPause;
        }
    }
}