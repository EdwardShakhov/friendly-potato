using UnityEngine;
public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool _mayFire;
        [SerializeField] private bool _barIsNotEmpty;
        
        [SerializeField] private string _weaponName;
        [SerializeField] private int _damage;
        [SerializeField] private float _barCapacity;
        [SerializeField] private float _currentBar;
        [SerializeField] private int _maxNumberOfBullets;
        [SerializeField] private int _numberOfBullets;

        [SerializeField] private float _shootDelay;
        [SerializeField] private float _currentDelay;
        [SerializeField] private float _reloadDelay;
        
        [SerializeField] private float _distance;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Rigidbody _bulletProjectile;

        public bool MayFire
        {
            get => _mayFire;
            set => _mayFire = value;
        }
        public bool BarIsNotEmpty
        {
            get => _barIsNotEmpty;
            set => _barIsNotEmpty = value;
        }
        public string WeaponName => _weaponName;
        public int Damage => _damage;
        public float BarCapacity => _barCapacity;
        public float CurrentBar
        {
            get => _currentBar;
            set => _currentBar = value;
        }
        public int NumberOfBullets
        {
            get => _numberOfBullets;
            set => _numberOfBullets = value;
        }
        public float ShootDelay => _shootDelay;
        public float CurrentDelay
        {
            get => _currentDelay;
            set => _currentDelay = value;
        }
        public float ReloadDelay => _reloadDelay;
        public float Distance => _distance;
        public float BulletSpeed => _bulletSpeed;
        public Rigidbody BulletProjectile => _bulletProjectile;
        public int MaxNumberOfBullets => _maxNumberOfBullets;

        protected void Start()
        {
            _currentBar = _barCapacity;
            _mayFire = true;
            _barIsNotEmpty = true;
            _currentDelay = 0;
            _numberOfBullets = _maxNumberOfBullets / 2;
        }
    }