using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Spin : MonoBehaviour


{
    private float torque = 4;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float turnInput = Keyboard.current.spaceKey.isPressed ? 1f : 0f;

        rb.AddTorque(Vector3.forward,  torque * turnInput * Time.fixedDeltaTime);
    }

    {
    }
}