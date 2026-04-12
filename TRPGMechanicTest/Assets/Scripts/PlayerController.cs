using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxMovementPerTurn = 10;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float aimRotationSpeed = 0.1f;
    
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference toggleAction;
    [SerializeField] private InputActionReference resetTurnMovementAction;
    
    public UnityEvent<float> moveEvent = new();
    
    private float _currentTurnTotalMovement;
    private Vector2 _resetLocation;
    private float _currentAimRotation;
    // TODO - This should be reworked into an enum or index to allow for a player to have multiple moves
    private bool _isMoving;
    
    void OnEnable()
    {
        // Enable default InputActions 
        move.action.Enable();
        toggleAction.action.Enable();
        resetTurnMovementAction.action.Enable();
        

    }

    void OnDisable()
    {
        // Disable player InputActions
        move.action.Disable();
        toggleAction.action.Disable();
        resetTurnMovementAction.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (toggleAction.action.triggered && _isMoving)
        {
                _isMoving = false;
        }
        else if(toggleAction.action.triggered && !_isMoving)
        {
            _isMoving = true;
        }
        
        if (_isMoving)
        {
            Move();

            if (resetTurnMovementAction.action.triggered)
            {
                ResetTurnMovement();
            }
        }
        else
        {
            ShootAction();
        }


    }

    private void Move()
    {
        Vector2 movementVector = move.action.ReadValue<Vector2>().normalized;
        float moveDistance = movementVector.magnitude * (speed * Time.deltaTime);
        
        Debug.Log(moveDistance + _currentTurnTotalMovement);
        Debug.Log(maxMovementPerTurn);
        
        if (moveDistance + _currentTurnTotalMovement < maxMovementPerTurn)
        {
            _currentTurnTotalMovement += moveDistance;
            gameObject.transform.Translate(
                movementVector * (speed * Time.deltaTime), 
                Space.World);
            moveEvent.Invoke(moveDistance);
        }
    }

    private void ShootAction()
    {
        float aimDirectionChange = move.action.ReadValue<Vector2>().y * (aimRotationSpeed * Time.deltaTime);
        _currentAimRotation += aimDirectionChange;
        Debug.Log(_currentAimDirection);
        
        
    }

    private void ResetTurnMovement()
    {
        _currentTurnTotalMovement = 0;
        transform.position = _resetLocation;
    }

    private void StartTurn()
    {
        _currentTurnTotalMovement = 0;
        _resetLocation = transform.position;
    }
    

}
