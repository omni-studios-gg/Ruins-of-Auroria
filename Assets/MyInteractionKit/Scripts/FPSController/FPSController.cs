using UnityEngine;
using Unity.Cinemachine;

namespace MyInteractionKit
{
    public class FPSController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 10f;
        public float jumpForce = 5f;
        public float crouchSpeed = 2.5f;
        public float slideSpeed = 15f;
        public AudioClip[] footstepSounds;
        public float groundDistance = 0.4f;
        public CinemachineVirtualCamera cinemachineCamera;
        public float lookSensitivity = 2f;

        private CharacterController controller;
        private Vector3 velocity;
        [SerializeField]
        private bool isGrounded;
        private bool isCrouching = false;
        private bool isSliding = false;
        private AudioSource audioSource;
        private float originalHeight;
        private float crouchHeight = 0.5f;
        private Vector3 slideDirection;
        private float slideTimer;

        private float xRotation = 0f;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
            originalHeight = controller.height; 

            // Lock the cursor to the center of the screen for a true FPS experience
            Cursor.lockState = CursorLockMode.Locked;

            if (cinemachineCamera != null)
            {
                cinemachineCamera.Follow = transform;
                cinemachineCamera.LookAt = null;  // We handle the camera rotation ourselves
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleJump();
            HandleCrouch();
            HandleSlide();
            HandleFootsteps();
            HandleCameraRotation();
        }

        void HandleMovement()
        {
            // Perform a Raycast downwards to check if we are on the ground layer
            isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            float speed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);

            controller.Move(move * speed * Time.deltaTime);

            if (isSliding)
            {
                controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            }

            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            }
        }

        void HandleCrouch()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
                controller.height = isCrouching ? crouchHeight  : originalHeight;
            }
        }

        void HandleSlide()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && !isSliding && isGrounded)
            {
                isSliding = true;
                slideDirection = transform.forward;
                slideTimer = 0.5f; // Slide duration
            }

            if (isSliding)
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0)
                {
                    isSliding = false;
                }
            }
        }

        void HandleFootsteps()
        {
            if (Input.GetAxis("Vertical") > 0 && isGrounded && !audioSource.isPlaying)
            {
                audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.Play();
            }
        }

        void HandleCameraRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            cinemachineCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}

