using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class AircraftController : MonoBehaviour
{
    [Header("Physics References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _physicsRoot;

    [Header("Control Sensitivity")]
    [SerializeField] private float _pitchTorque = 8000f;
    [SerializeField] private float _rollTorque = 6000f;
    [SerializeField] private float _yawTorque = 4000f;

    [Header("Engine")]
    [SerializeField] private float _maxThrust = 100000f;
    [SerializeField] private float _throttleSensitivity = 0.3f;
    private float _currentThrottle = 0f;

    [Header("Aerodynamics")]
    [SerializeField] private float _liftFactor = 1.2f;
    [SerializeField] private float _stabilizationStrength = 15f;

    // Input значения
    private float _pitchInput;
    private float _rollInput;
    private float _yawInput;
    private float _throttleInput;

    private AircraftControls _controls;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        _controls = new AircraftControls();

        // Настройка физики дляп полета
        SetupRigidbody();
    }

    private void SetupRigidbody()
    {
        _rb.mass = 1000f;           // масса для самолета
        _rb.linearDamping = 0.1f;            // Аэродинамическое сопротивление
        _rb.angularDamping = 2f;       // Сопротивление вращению
        _rb.useGravity = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void OnEnable()
    {
        _controls.AirCraft.Enable();
        _controls.AirCraft.Pitch.performed += OnPitch;
        _controls.AirCraft.Pitch.canceled += OnPitch;
        _controls.AirCraft.Roll.performed += OnRoll;
        _controls.AirCraft.Roll.canceled += OnRoll;
        _controls.AirCraft.Yaw.performed += OnYaw;
        _controls.AirCraft.Yaw.canceled += OnYaw;
        _controls.AirCraft.Throttle.performed += OnThrottle;
        _controls.AirCraft.Throttle.canceled += OnThrottle;
    }

    private void OnDisable()
    {
        _controls.AirCraft.Disable();
        _controls.AirCraft.Pitch.performed -= OnPitch;
        _controls.AirCraft.Pitch.canceled -= OnPitch;
        _controls.AirCraft.Roll.performed -= OnRoll;
        _controls.AirCraft.Roll.canceled -= OnRoll;
        _controls.AirCraft.Yaw.performed -= OnYaw;
        _controls.AirCraft.Yaw.canceled -= OnYaw;
        _controls.AirCraft.Throttle.performed -= OnThrottle;
        _controls.AirCraft.Throttle.canceled -= OnThrottle;
    }

    private void OnPitch(InputAction.CallbackContext ctx) => _pitchInput = ctx.ReadValue<Vector2>().y;
    private void OnRoll(InputAction.CallbackContext ctx) => _rollInput = ctx.ReadValue<Vector2>().x;
    private void OnYaw(InputAction.CallbackContext ctx) => _yawInput = ctx.ReadValue<Vector2>().x;
    private void OnThrottle(InputAction.CallbackContext ctx) => _throttleInput = ctx.ReadValue<Vector2>().y;

    private void FixedUpdate()
    {
        HandleEngine();
        HandleAerodynamics();
    }

    private void HandleEngine()
    {
        _currentThrottle = Mathf.Clamp01(_currentThrottle + _throttleInput * _throttleSensitivity * Time.fixedDeltaTime);

        // Тяга применяется вперед относительно самолета
        Vector3 thrust = _physicsRoot.forward * (_currentThrottle * _maxThrust);
        _rb.AddForce(thrust, ForceMode.Force);
    }

    private void HandleAerodynamics()
    {
        // Управляющие моменты - поворачивают самолет
        ApplyControlTorque();

        // Аэродинамические силы - меняют траекторию полета
        HandleLiftAndDrag();

        // Стабилизация - возвращает к горизонтальному положению
        HandleStabilization();
    }

    private void ApplyControlTorque()
    {
        Vector3 torque = Vector3.zero;

        // Pitch - вращение вокруг правой оси (нос вверх/вниз)
        torque += _physicsRoot.right * (_pitchInput * _pitchTorque * Time.fixedDeltaTime);

        // Roll - вращение вокруг передней оси (крен)
        torque += _physicsRoot.forward * (_rollInput * _rollTorque * Time.fixedDeltaTime);

        // Yaw - вращение вокруг верхней оси (рыскание)
        torque += _physicsRoot.up * (_yawInput * _yawTorque * Time.fixedDeltaTime);

        _rb.AddTorque(torque, ForceMode.Force);
    }

    private void HandleLiftAndDrag()
    {
        Vector3 localVelocity = _physicsRoot.InverseTransformDirection(_rb.linearVelocity);
        float speed = _rb.linearVelocity.magnitude;

        if (speed > 5f) // Минимальная скорость для аэродинамики
        {
            // Подъемная сила зависит от скорости и угла атаки
            float angleOfAttack = Mathf.Atan2(-localVelocity.y, localVelocity.z);
            Vector3 liftDirection = _physicsRoot.up;
            float liftMagnitude = speed * speed * Mathf.Sin(angleOfAttack) * _liftFactor;
            Vector3 liftForce = liftDirection * liftMagnitude;

            _rb.AddForce(liftForce, ForceMode.Force);

            // автоматический поворот при крене
            HandleBankedTurn();
        }
    }

    private void HandleBankedTurn()
    {
        // Получаем угол крена
        float bankAngle = transform.eulerAngles.z;
        if (bankAngle > 180f) bankAngle -= 360f;

        // Если есть крен, создаем центростремительную силу для поворота
        if (Mathf.Abs(bankAngle) > 10f)
        {
            // Направление поворота - перпендикулярно подъемной силе и вектору тяги
            Vector3 turnDirection = Vector3.Cross(_physicsRoot.up, Vector3.up).normalized;

            // Сила поворота пропорциональна углу крена и скорости
            float turnStrength = Mathf.Abs(bankAngle) / 90f * _rb.linearVelocity.magnitude * 50f;
            Vector3 turnForce = turnDirection * Mathf.Sign(bankAngle) * turnStrength;

            _rb.AddForce(turnForce, ForceMode.Force);

            // Автоматическое рыскание для координации поворота
            float coordinatedYaw = -bankAngle * 0.01f;
            Vector3 yawTorque = _physicsRoot.up * coordinatedYaw;
            _rb.AddTorque(yawTorque, ForceMode.Force);
        }
    }

    private void HandleStabilization()
    {
        // Стабилизация крена при отсутствии управления
        if (Mathf.Abs(_rollInput) < 0.1f)
        {
            float currentRoll = transform.eulerAngles.z;
            if (currentRoll > 180f) currentRoll -= 360f;

            float stabilizationTorque = -currentRoll * _stabilizationStrength * Time.fixedDeltaTime;
            _rb.AddTorque(_physicsRoot.forward * stabilizationTorque, ForceMode.Force);
        }
    }

    public float GetThrottle() => _currentThrottle;
    public float GetSpeed() => _rb.linearVelocity.magnitude;
    public float GetAltitude() => _physicsRoot.position.y;
}