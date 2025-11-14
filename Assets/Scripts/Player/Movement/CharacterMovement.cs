
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeReference] private InputActionAsset _characterInputMap;
    [SerializeField] private float _boostDetectTime = 1f;
    [SerializeField] private MovementStats _moveStats;

#region "Input"
    private InputAction _horizontalInput;
    private InputAction _upMove;
    private InputAction _downMove;

    private Vector3 _moveDirection;

#endregion
    void Start()
    {
        _characterInputMap.Enable();    
        
        _horizontalInput = _characterInputMap.FindAction("HorizontalMovement");

        _upMove = _characterInputMap.FindAction("Ascend");
        _downMove = _characterInputMap.FindAction("Descend");
    
        _rigidbody = gameObject.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementInput();

        if(_moveDirection.sqrMagnitude > 0)
            Debug.Log($"Move Input: {_moveDirection}");
    }
    void FixedUpdate(){
        ApplyMovement();
        LimitVelocity();
    }

    private Vector2 _lastFrameHorizontalInput = Vector2.zero;
    private float _timeOfLastInput = 0f;
    private bool _jumpWasTriggered = false;
    private void CheckMovementInput(){
        
        Vector2 _currentFrameHorizontalInput = _horizontalInput.ReadValue<Vector2>();
        
        _moveDirection.x = _currentFrameHorizontalInput.x;
        _moveDirection.z = _currentFrameHorizontalInput.y;

        Vector3 currVelocity = _rigidbody.linearVelocity;
        if(Mathf.Sign(_currentFrameHorizontalInput.y) != Mathf.Sign(_lastFrameHorizontalInput.y) && _currentFrameHorizontalInput.y != 0f)
            currVelocity.z = 0f;
        if(Mathf.Sign(_currentFrameHorizontalInput.x) != Mathf.Sign(_lastFrameHorizontalInput.x) && _currentFrameHorizontalInput.x != 0f)
            currVelocity.x = 0f;

        _rigidbody.linearVelocity = currVelocity;
        
        float upMove = _upMove.ReadValue<float>();
        if(upMove == 1){
            if(_moveDirection.sqrMagnitude > 0 && _moveDirection.y <= 0)
                _moveDirection = Vector3.zero;
            _moveDirection.y += 1;
            _timeOfLastInput = Time.time;
            if(_rigidbody != null) _rigidbody.useGravity = false;
            _jumpWasTriggered = true;
        }
        if(upMove == 0 && _jumpWasTriggered){
            if(_rigidbody != null) _rigidbody.useGravity = true;
            _jumpWasTriggered = false;
            _moveDirection.y = 0f;
        }
        if(_downMove.triggered){
            if(_moveDirection.sqrMagnitude > 0 && _moveDirection.y >= 0)
                _moveDirection = Vector3.zero;
            _moveDirection.y -= 1;
            _timeOfLastInput = Time.time;
        }
        
        _lastFrameHorizontalInput = _currentFrameHorizontalInput;
        
        // if(Time.time - _timeOfLastInput > _boostDetectTime || _moveDirection.sqrMagnitude > 4f)
            // _moveDirection = Vector3.zero;
            
    }

    private void LimitVelocity(){
        if(_rigidbody == null) return;

        Vector3 rbVelocity = _rigidbody.linearVelocity;

        if(rbVelocity.y > _moveStats._maxVerticalSpeed) {
            rbVelocity.y = _moveStats._maxVerticalSpeed;
            _rigidbody.linearVelocity = rbVelocity;
        }

        Vector3 horizontalVelocity = _rigidbody.linearVelocity;
        horizontalVelocity.y = 0;
        
        if(horizontalVelocity.sqrMagnitude > _moveStats._maxHorizontalSpeed * _moveStats._maxHorizontalSpeed){
            Vector3 horizontalDir = horizontalVelocity.normalized;
            Vector3 limitedVelocity = horizontalDir * _moveStats._maxHorizontalSpeed;
            float yVelocity = _rigidbody.linearVelocity.y;
            limitedVelocity.y = yVelocity;

            _rigidbody.linearVelocity = limitedVelocity;
        }
    }

    private Rigidbody _rigidbody;
    private void ApplyMovement(){
        if(_rigidbody == null) return;

        _rigidbody.AddForce(_moveDirection * 1f, ForceMode.Impulse);
    }
}
