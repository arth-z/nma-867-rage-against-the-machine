using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    InputAction lookAction;
    InputAction moveAction;
    InputAction clickAction;

    public float sens = 0.2f;
    public float speed = 20f;
    private float h = 0; // horizontal
    private float v = 0; // vertical
    private float f = 0; // forward
    private float s = 0; // side

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookAction = InputSystem.actions.FindAction("Look");
        moveAction = InputSystem.actions.FindAction("Move");
        clickAction = InputSystem.actions.FindAction("Click");
        clickAction.performed += ctx => clicked();
        lookAction.Enable();
        moveAction.Enable();
        clickAction.Enable();
    }

    void Move()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        f = movement.y;
        s = movement.x;

        if (f != 0)
        {
            transform.position += transform.forward * Time.deltaTime * speed * f;
        }

        if (s != 0)
        {
            transform.position += transform.right * Time.deltaTime * speed * s;
        }

    }

    void MouseLook()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        h += lookValue.x * sens;
        v += lookValue.y * sens;

        // Clamp pitch to avoid issues and sign flips at +/- 90
        v = Mathf.Clamp(v, -89, 89);


        // spherical coordinates because euler rotations cause some weeeeeird stuff
        var theta = h * Mathf.Deg2Rad;
        var phi = v * Mathf.Deg2Rad;

        var sinTheta = Mathf.Sin(theta);
        var cosTheta = Mathf.Cos(theta);
        var sinPhi = Mathf.Sin(phi);
        var cosPhi = Mathf.Cos(phi);

        // Convert from spherical to cartesian and directly assign to up. 
        var fwd = new Vector3(cosPhi * sinTheta, sinPhi, cosPhi * cosTheta);
        transform.forward = fwd;

        var up = new Vector3(-sinPhi * sinTheta, cosPhi, -sinPhi * cosTheta);
        transform.rotation = Quaternion.LookRotation(fwd, up);
    }
    private void clicked()
    {
        Vector3 fwd = transform.forward;
        RaycastHit hitInfo;
        
        if (Physics.Raycast(transform.position, fwd, out hitInfo, 200))
        {
            if (hitInfo.collider?.GetComponent<KeyboardKey>())
            {
                hitInfo.collider?.GetComponent<KeyboardKey>().press();
            }
        }

    }

    private void Update()
    {
        MouseLook();
        Move();
    }

    void OnDestroy()
    {
        lookAction?.Disable();
        moveAction?.Disable();
        clickAction?.Disable();
    }

}
