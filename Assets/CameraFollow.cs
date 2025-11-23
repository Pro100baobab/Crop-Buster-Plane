using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);
    [SerializeField] private float _positionSmoothTime = 0.3f;
    [SerializeField] private float _rotationSmoothTime = 0.2f;

    private Vector3 _velocity = Vector3.zero;
    private Quaternion _targetRotation;

    private void Start()
    {
        if (_target != null)
        {
            _targetRotation = _target.rotation;
        }
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        // Сглаживание позиции
        Vector3 targetPosition = _target.TransformPoint(_offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _positionSmoothTime);

        // Сглаживание вращения (смотрим на самолет)
        _targetRotation = Quaternion.LookRotation(_target.position - transform.position, _target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSmoothTime);
    }
}