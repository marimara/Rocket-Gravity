using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float force = 1000f;
    [SerializeField] float rotationForce;
    Rigidbody rb;

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
        }
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
}
