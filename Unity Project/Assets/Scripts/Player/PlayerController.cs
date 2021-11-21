using System.Collections.Generic;
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
        private float _turnSmoothVelocity; 
        private int _movementHash;
        
        [Header("Player Health")]
        [SerializeField] private int _playerMaxHealth = 100;
        [SerializeField] private int _playerHealth;
        
        [Header("Player Weapon")]
        [SerializeField] private Transform _allWeaponsHolder;
        [SerializeField] private List<GameObject> _weapons;
        [SerializeField] private Weapon _activeWeapon;
        [SerializeField] private GameObject _pistol;
        [SerializeField] private GameObject _shotgun;

        //getters/setters
        public Transform GameCamera => _gameCamera;
        public int PlayerMaxHealth => _playerMaxHealth;
        public int PlayerHealth
        {
            get => _playerHealth;
            set => _playerHealth = value;
        }
        public List<GameObject> Weapons => _weapons;
        public Weapon ActiveWeapon
        {
            get => _activeWeapon;
            set => _activeWeapon = value;
        }
        //getters/setters end
        
        protected void Awake()
        {
            _weapons ??= new List<GameObject>();
            _weapons.Add(Instantiate(_pistol, _allWeaponsHolder));
            _weapons.Add(Instantiate(_shotgun,_allWeaponsHolder));
            _activeWeapon = _weapons[0].GetComponent<Weapon>();
            
            _playerAnimator = _playerAnimator ? _playerAnimator : GetComponent<Animator>();
            _movementHash = Animator.StringToHash("speed");
            _playerHealth = _playerMaxHealth;
            GameManager.Instance.IsPlayerDead = false;
        }

        protected void Update()
        {
            GameManager.Instance.GamePause();
            var direction = GetDirection();
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
        
        private Vector3 GetDirection()
        {
            if(!GameManager.Instance.IsPlayerDead)
            {
                return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            }
            return default;
        }
        
        public void DamagePlayer(int damage)
        {
            _playerHealth -= damage;
            if (PlayerHealth <= 0)
            {
                _playerAnimator.SetBool("death", true);
                GameManager.Instance.GameOver();
            }
        }
    }
}