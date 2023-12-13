using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isRunning;
    private double maxStamina = 100f;
    private double currentStamina = 100f;
    private Vector3 lastPosition;
    public float thresholdChange = 0.05f;
    public Image staminaBar;

    private bool IsGrounded;
    public float gravity = -9.8f;

    public float jumpHeight = 2f;

    private bool sprinting = false;
    private bool crouching = false;

    private float crouchTimer;
    private bool lerpCrouch;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
        InvokeRepeating("CheckForMovement", 0.001f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        var diffVector = transform.position - lastPosition;
        currentStamina = Mathf.Clamp((float)currentStamina, 0, (float)maxStamina);
        IsGrounded = controller.isGrounded;

        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if (isRunning && diffVector.magnitude >= thresholdChange)
        {
            currentStamina -= 0.1f;
            lastPosition = transform.position;
        }
        else
        {
            currentStamina += 0.1f;
        }

        

        UpdateStaminaUI();
    }

    //receive the inputs for our InputManager.cs and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection * speed * Time.deltaTime));
        playerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void UpdateStaminaUI()
    {
        float fillStamina = staminaBar.fillAmount;
        double staminaFraction = currentStamina / maxStamina;
        {
            staminaBar.fillAmount = (float)staminaFraction;
        }
    }
    public void Jump()
    {
        if(IsGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight = -3f * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed *= 2;
            isRunning = true;
        }
        else
        {
            speed = 5;
            isRunning = false;
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
}
