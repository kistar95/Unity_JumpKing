using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private Camera _mainCamera;
    private float _cameraHeight;
    private float _cameraWidth;
    private Vector2 _currentCameraPos;
    private Vector2 _currentCell;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraHeight = _mainCamera.orthographicSize * 2;
        _cameraWidth = _mainCamera.aspect * _cameraHeight;
        _currentCameraPos = _mainCamera.transform.position;
        _currentCell = Vector2.zero;
    }

    private void Update()
    {
        Vector2 _playerCell = new Vector2((int)(_playerTransform.position.x / _cameraWidth), (int)(_playerTransform.position.y / (_cameraHeight / 2)));

        if (_playerCell.y != _currentCell.y)
        {
            _currentCell = _playerCell;
            MoveCamera(_playerCell);
        }
    }

    private void MoveCamera(Vector2 inCell)
    {
        Vector3 _targetPos = new Vector3(transform.position.x, _cameraHeight * inCell.y, transform.position.z);

        transform.position = _targetPos;
    }
}
