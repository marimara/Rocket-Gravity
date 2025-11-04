using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float force = 1000f;
    [SerializeField] float rotationForce;
    [SerializeField] UnityEvent OnThrust;
    [SerializeField] UnityEvent OnStopThrust;
    Rigidbody rb;
    bool isThrusting;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationForce * Time.fixedDeltaTime);
            rb.freezeRotation = false;
        }
        else if (rotationInput > 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * -rotationForce * Time.fixedDeltaTime);
            rb.freezeRotation = false;
        }
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            
            rb.AddRelativeForce(Vector3.up * force * Time.fixedDeltaTime);
            if (!isThrusting)
            {
                OnThrust.Invoke();
            }
            isThrusting = true;

        }
        else
        {
            OnStopThrust.Invoke();
            isThrusting= false;

        }
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
}
