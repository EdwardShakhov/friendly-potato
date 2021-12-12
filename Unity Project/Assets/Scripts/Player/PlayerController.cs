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
        private readonly int _speedHash = Animator.StringToHash("speed");
        
        [Header("Player Health")]
        [SerializeField] private int _playerMaxHealth;
        [SerializeField] private int _playerHealth;
        [SerializeField] private ParticleSystem _bloodSfx;
        private const float _destroySfxTime = 4f;
        private readonly int _deathHash = Animator.StringToHash("death");
        
        [Header("Player Weapon")]
        [SerializeField] private Transform _allWeaponsHolder;
        [SerializeField] private GameObject _allWeapons;
        [SerializeField] private List<GameObject> _weapons;
        [SerializeField] private Weapon _activeWeapon;
        [SerializeField] private GameObject _pistol;
        [SerializeField] private GameObject _shotgun;
        
        [Header("Player Level")]
        [SerializeField] private int _playerLevel;
        [SerializeField] private int _playerExperience;
        private const int _playerExperienceToTheNextLevel = 100;
        [SerializeField] private float _playerStatsIncreaseCoeff;
        [SerializeField] private ParticleSystem _LevelUpSfx;

        //getters/setters
        public Transform GameCamera => _gameCamera;
        public int PlayerMaxHealth => _playerMaxHealth;
        public int PlayerHealth
        {
            get => _playerHealth;
            set => _playerHealth = value;
        }
        public GameObject AllWeapons => _allWeapons;
        public List<GameObject> Weapons => _weapons;
        public Weapon ActiveWeapon
        {
            get => _activeWeapon;
            set => _activeWeapon = value;
        }
        public float PlayerStatsIncreaseCoeff => _playerStatsIncreaseCoeff;
        public int PlayerLevel => _playerLevel;
        public int PlayerExperience
        {
            get => _playerExperience;
            set => _playerExperience = value;
        }
        public GameObject Pistol => _pistol;
        public GameObject Shotgun => _shotgun;
        //getters/setters end
        
        protected void Awake()
        {
            _weapons ??= new List<GameObject>();
            _weapons.Add(Instantiate(_pistol, _allWeaponsHolder));
            _weapons.Add(Instantiate(_shotgun,_allWeaponsHolder));
            _activeWeapon = _weapons[0].GetComponent<Weapon>();
            
            _playerAnimator = _playerAnimator ? _playerAnimator : GetComponent<Animator>();

            _playerLevel = 1;
            _playerExperience = 0;
            _playerStatsIncreaseCoeff = 1;
            _playerHealth = _playerMaxHealth;
            GameManager.Instance.IsPlayerDead = false;
        }

        protected void Update()
        {
            GameManager.Instance.GamePause();
            HoldHealthBetweenZeroAndMaxValue();
            LevelUp();

            var direction = GetDirection();
            if (direction.magnitude >= 0.5f)
            {
                var targetAngle = Mathf.Atan2(
                    direction.x, direction.z) * Mathf.Rad2Deg + _gameCamera.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _playerTurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _playerController.Move(moveDir.normalized * (_playerSpeed * Time.deltaTime));
            }
            _playerAnimator.SetFloat(_speedHash, direction.magnitude);
        }
        
        private static Vector3 GetDirection()
        {
            if(!GameManager.Instance.IsPlayerDead)
            {
                return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            }
            return default;
        }
        
        public void DamagePlayer(int damage)
        {
            Destroy(Instantiate(_bloodSfx, 
                gameObject.transform.position + new Vector3(0,1,0), 
                Quaternion.identity).gameObject, _destroySfxTime);
            
            _playerHealth -= damage;
            if (PlayerHealth <= 0)
            {
                _playerAnimator.SetBool(_deathHash, true);
                GameManager.Instance.GameOver();
            }
        }

        protected void HoldHealthBetweenZeroAndMaxValue()
        {
            if (_playerHealth > _playerMaxHealth)
            {
                _playerHealth = _playerMaxHealth;
            }

            if (_playerHealth < 0)
            {
                _playerHealth = 0;
            }
        }
        
        private void LevelUp()
        {
            if (_playerExperience >= _playerExperienceToTheNextLevel)
            {
                _playerLevel++;
                GameManager.Instance.Player.GetComponent<PlayerSound>().LevelUp();
                Destroy(Instantiate(_LevelUpSfx, 
                    gameObject.transform.position + new Vector3(0,1,0), 
                    Quaternion.identity, gameObject.transform).gameObject, _destroySfxTime);
                _playerExperience = 0;
                _playerStatsIncreaseCoeff += 0.1f;
                _playerMaxHealth = (int)(100 * _playerStatsIncreaseCoeff);
                _playerHealth = _playerMaxHealth;
            }
        }
    }
}