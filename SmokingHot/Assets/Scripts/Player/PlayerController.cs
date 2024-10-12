using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCamera;

    private CharacterController controller;

    private Vector3 movement;
    private float playerSpeed = 6.0f;

    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;
    }

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get input for movement along X and Z axes
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        // Normalize movement so diagonal movement isn't faster
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        controller.Move(playerSpeed * Time.deltaTime * movement);

        RotateToMouse();
    }

    void RotateToMouse()
    {
        // Get the mouse position in screen space and convert to world space
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        // Check if the ray intersects the ground plane
        if (groundPlane.Raycast(ray, out rayLength))
        {
            // Get the point on the ground plane where the ray intersects
            Vector3 pointToLook = ray.GetPoint(rayLength);

            // Calculate the direction to the point
            Vector3 direction = pointToLook - transform.position;
            direction.y = 0f; // Ignore Y-axis for rotation, keep it horizontal

            // Rotate the player to face the point
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void OnUseCigarette()
    {
        gameManager.PlayerWantToConsumeCigarette();
    }

    public void OnUseAlcool()
    {
        gameManager.PlayerWantToConsumeAlcool();
    }
}
