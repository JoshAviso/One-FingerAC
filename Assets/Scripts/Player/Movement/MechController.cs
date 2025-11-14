using UnityEngine;
using UnityEngine.InputSystem;

public class MechController : MonoBehaviour
{

    private void OnEnable(){
        EnableInputs();
    }
    
    private void OnDisable(){
        DisableInputs();
    }

    private void Update(){
        ReadInputs();
        DebugInputs();
    }

    #region Input Reading
    [Header("Input Settings")]
    [SerializeField] private bool _toggleBoost = true;

    [Header("Input")]
    [SerializeReference] private InputActionReference _moveInput;
    [SerializeReference] private InputActionReference _lookInput;
    [SerializeReference] private InputActionReference _ascendInput;
    [SerializeReference] private InputActionReference _boostTogleInput;
    [SerializeReference] private InputActionReference _interactButtonInput;
    [SerializeReference] private InputActionReference _primaryActionInput;
    [SerializeReference] private InputActionReference _secondaryActionInput;
    [SerializeReference] private InputActionReference _tertiaryActionInput;
    [SerializeField] private bool _showInputDebug = false;

    [SerializeField] private Camera _playerCam;

    private Vector2 _moveInputDir = Vector2.zero;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _lookDirection = Vector2.zero;
    private bool _ascendPressed = false;
    private bool _boostPressed = false;
    private bool _primaryPressed = false;
    private bool _secondaryPressed = false;
    private bool _tertiaryPressed = false;
    private void ReadInputs(){
        _moveInputDir = _moveInput.action.ReadValue<Vector2>();
        _moveInputDir = _moveInputDir.normalized;
        if(_playerCam != null){
            Vector3 camForward = _playerCam.transform.forward;
            camForward.y = 0f;
            Vector3 camRight = _playerCam.transform.right;
            camRight.y = 0f;

            _moveDirection = (camForward * _moveDirection.y) + (camRight * _moveDirection.x);
        } else _moveDirection = new Vector3(_moveInputDir.x, 0f, _moveInputDir.y);

        _lookDirection = _lookInput.action.ReadValue<Vector2>();
        _ascendPressed = _ascendInput.action.ReadValue<float>() > 0f;
        _boostPressed = _boostTogleInput.action.ReadValue<float>() > 0f;
        _primaryPressed = _primaryActionInput.action.ReadValue<float>() > 0f;
        _secondaryPressed = _secondaryActionInput.action.ReadValue<float>() > 0f;
        _tertiaryPressed = _tertiaryActionInput.action.ReadValue<float>() > 0f;
    }

    private void EnableInputs(){
        _interactButtonInput.action.performed += OnInteractPressed;

        _moveInput.action.Enable();
        _lookInput.action.Enable();
        _ascendInput.action.Enable();
        _boostTogleInput.action.Enable();
        _interactButtonInput.action.Enable();
        _primaryActionInput.action.Enable();
        _secondaryActionInput.action.Enable();
        _tertiaryActionInput.action.Enable();
    }
    private void DisableInputs(){
        _interactButtonInput.action.performed -= OnInteractPressed;

        _moveInput.action.Disable();
        _lookInput.action.Disable();
        _ascendInput.action.Disable();
        _boostTogleInput.action.Disable();
        _interactButtonInput.action.Disable();
        _primaryActionInput.action.Disable();
        _secondaryActionInput.action.Disable();
        _tertiaryActionInput.action.Disable();
    }
    private void OnInteractPressed(InputAction.CallbackContext context){
        Debug.Log("Interact Pressed");
    }
    private void DebugInputs(){
        Debug.Log($"Move: {_moveDirection} | Look: {_lookDirection} | Ascend Pressed: {_ascendPressed}\nPrimary: {_primaryPressed} | Secondary: {_secondaryPressed} | Tertiary: {_tertiaryPressed}");
    }
    #endregion

    #region Input Processing
    private Rigidbody _rb;
    private void ProcessMovement(){
        if(_rb == null) _rb = gameObject.GetComponentInChildren<Rigidbody>();

        _rb.AddForce(_moveDirection * 1f, ForceMode.Impulse);
    }
    private void ProcessLook(){

    }
    private void ProcessAscent(){

    }
    private bool _boostActive = false;
    private bool _boostJustPressed = false;
    private bool _boostCanBeTriggered = true;
    private void ProcessBoostToggle(){
        _boostJustPressed = _boostPressed && _boostCanBeTriggered;
        _boostActive = _toggleBoost ? (_boostPressed && _boostCanBeTriggered ? !_boostActive : _boostActive) : _boostPressed;
        _boostCanBeTriggered = !_boostPressed;
    }
    private void ProcessInteract(){
        Debug.Log("Interacted");
    }
    private void ProcessMainActions(){

    }

    #endregion
    
}
