using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference toggleAction;
    [SerializeField] private float speed = 1.5f;
    
    void OnEnable()
    {
        // Enable player InputActions
        move.action.Enable();
    }

    void OnDisable()
    {
        // Disable player InputActions
        move.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // TODO - This should have a speed scalar
        gameObject.transform.Translate(
            move.action.ReadValue<Vector2>().normalized * (speed * Time.deltaTime), 
            Space.World);
    }
}
