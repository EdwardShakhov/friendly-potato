using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _playerController;
    [SerializeField] private Transform _gameCamera;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerTurnSmoothTime;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _weaponHolder;
    //[SerializeField] private int PlayerHealthPoint;
    private float _turnSmoothVelocity; 
    private int _movementHash;

    protected void Start()
    {
        Instantiate(_weapon,_weaponHolder);
        //PlayerHealthPoint = 100;
        _playerAnimator = GetComponent<Animator>();
        _movementHash = Animator.StringToHash("speed");
    }

    protected void Update()
    {
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
}