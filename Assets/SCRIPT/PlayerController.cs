using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 4; // Distance between lanes

    public float jumpForce;
    public float gravity = -20;
    
    private bool isRolling = false;
    private float rollDuration = 0.7f; // Duration of roll
    private float originalHeight;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    void Update()
    {
        direction.z = forwardSpeed;

        //increase Speed
        if (forwardSpeed < maxSpeed)
        forwardSpeed += 0.1f * Time.deltaTime;


        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isRolling)
            {
                StartCoroutine(Roll());
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        // Lane switching logic
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }

        // Calculate future position
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 1000 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private IEnumerator Roll()
    {
        isRolling = true;

        // Reduce character height for rolling effect
        controller.height = originalHeight / 2;
        
        // Optional: Apply roll forward effect
        direction.y = -5f; // Push character down slightly
        direction.z += 2f; // Increase speed for a moment

        yield return new WaitForSeconds(rollDuration);

        // Reset character height after rolling
        controller.height = originalHeight;
        isRolling = false;
    }
}
