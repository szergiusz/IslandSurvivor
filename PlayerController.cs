using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("ControlScripts/ FPSInput")]

public class PlayerController : MonoBehaviour
{

    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public float speed = 6f;
    private float gravity = -9.8f;

    
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}
