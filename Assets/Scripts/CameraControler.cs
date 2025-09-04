using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Camera))]
public class CameraControler : MonoBehaviour
{

    [SerializeField] private float _moveSpeed =10;
    [SerializeField] private float _scrollSpeed =10;
    [SerializeField] private float _minZoomSize = 2;
    [SerializeField] private float _maxZoomSize = 6;
    [SerializeField] private Bounds _bounds;

    [SerializeField]private Camera _camera;
    private Vector2 _moveVec;
    private float _scroll;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        InputSystem.actions.FindAction("Move").performed+= OnMoveperformed;
        InputSystem.actions.FindAction("Move").canceled+= OnMovecanceled;
        InputSystem.actions.FindAction("Scroll").performed+= OnScrollperformed;
        InputSystem.actions.FindAction("Scroll").canceled+= OnScrollCanceled;
        
    }

    private void OnScrollperformed(InputAction.CallbackContext obj) {
        _scroll =((Vector2)obj.ReadValueAsObject()).y;
    }
    private void OnScrollCanceled(InputAction.CallbackContext obj) {
        _scroll = 0;
    }

    private void OnMovecanceled(InputAction.CallbackContext obj)
    {
        _moveVec = Vector2.zero;
    }

    private void OnMoveperformed(InputAction.CallbackContext obj)
    {
        _moveVec= (Vector2)obj.ReadValueAsObject();
    }

    public Vector2 GetCameraDelta() {
        return _camera.transform.position - _bounds.center;
    }

    // Update is called once per frame
    void Update() {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + _scroll * _scrollSpeed * Time.deltaTime,
            _minZoomSize, _maxZoomSize);

        float minX = _bounds.min.x + (_camera.orthographicSize * _camera.aspect);
        float maxX = _bounds.max.x - (_camera.orthographicSize * _camera.aspect);
        float minY = _bounds.min.y + _camera.orthographicSize  ;
        float maxY = _bounds.max.y - _camera.orthographicSize  ;
        if (minX > maxX) {
            minX =_bounds.center.x;
            maxX =_bounds.center.x;
        }
        if (minY > maxY) {
            minY =_bounds.center.y;
            maxY =_bounds.center.y;
        }
        //minX += _bounds.center.x;
        //maxX += _bounds.center.x;
        //minY += _bounds.center.y;
        //maxY += _bounds.center.y;
        Vector3 newpos =transform.position+_moveSpeed * Time.deltaTime* (Vector3)_moveVec;
        transform.position = new Vector3(Mathf.Clamp(newpos.x, minX, maxX), Mathf.Clamp(newpos.y,minY,maxY), -10);

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        
    }
}