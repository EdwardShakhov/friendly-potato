using UnityEngine;
public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool _mayFire;
        [SerializeField] private bool _barIsNotEmpty;

        [SerializeField] private int _damage;
        [SerializeField] private float _barCapacity;
        [SerializeField] private float _distance;
        [SerializeField] private float _bulletSpeed;
        
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _currentDelay;
        [SerializeField] private float _currentBar;
        [SerializeField] private string _weaponName;
        
        [SerializeField] private Rigidbody _bulletProjectile;

        public string WeaponName => _weaponName;

        //getters/setters
        public float ShootDelay => _shootDelay;

        public float BarCapacity => _barCapacity;

        public float CurrentBar
        {
            get => _currentBar;
            set => _currentBar = value;
        }
        
        public int Damage => _damage;

        public float Distance => _distance;
        public float CurrentDelay
        {
            get => _currentDelay;
            set => _currentDelay = value;
        }

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
        
        public float BulletSpeed => _bulletSpeed;

        public Rigidbody BulletProjectile => _bulletProjectile;
        //getters/setters end

        
        protected void Start()
        {
            _currentBar = _barCapacity;
            _mayFire = true;
            _barIsNotEmpty = true;
            _currentDelay = 0;
        }
    }