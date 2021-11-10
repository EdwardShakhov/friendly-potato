using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement")]
        [SerializeField] private CharacterController _playerController;
        [SerializeField] private Transform _gameCamera;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _playerTurnSmoothTime;
        
        [Header("Player Health")]
        [SerializeField] private int _playerMaxHealth = 1000; 
        public int PlayerMaxHealth => _playerMaxHealth;
        [SerializeField] private int _playerHealth; 
        public int PlayerHealth => _playerHealth;

        [Header("Player Weapon")]
        [SerializeField] private GameObject _weapon;
        public GameObject Weapon => _weapon;
        [SerializeField] private Transform _weaponHolder;
        
        private float _turnSmoothVelocity; 
        private int _movementHash;
        
        protected void Awake()
        {
            Instantiate(_weapon,_weaponHolder);
            _playerAnimator = _playerAnimator ? _playerAnimator : GetComponent<Animator>();
            _movementHash = Animator.StringToHash("speed");
            _playerHealth = _playerMaxHealth;
            GameManager.Instance.IsPlayerDead = false;
        }

        protected void Update()
        {
            GameManager.Instance.GamePause();
            var direction = PlayerInput.GetDirection();
            if (direction.magnitude >= 0.5f)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _gameCamera.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _playerTurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _playerController.Move(moveDir.normalized * (_playerSpeed * Time.deltaTime));
            }
            _playerAnimator.SetFloat(_movementHash, direction.magnitude);
        }
        
        public void DamagePlayer(int damage)
        {
            _playerHealth -= damage;
            if (PlayerHealth <= 0)
            {
                GameManager.Instance.IsPlayerDead = true;
                _playerAnimator.SetBool("death", true);
                GameManager.Instance.GameOver();
            }
        }

    }
}