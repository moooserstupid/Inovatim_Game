using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private float startMovementSpeed = 3.0f;
        [SerializeField] private float maxMovementSpeed = 5f;
        [SerializeField] private float accelerationDuration = 0.5f;

        [SerializeField] private float groundOffset;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float gravity = -9.81f;

        private CharacterInput m_input;
        private CharacterController m_characterController;

        private Vector2 m_currentMovementInput;
        private Vector3 m_currentMovement;
        private Vector3 m_moveDirection;
        private Vector3 m_spherePosition;
        private Vector3 m_sphereVelocity;

        //private float m_rotationFactorPerFrame = 1.0f;
        private float m_accelerationTime = 0f;
        private float m_currentSpeed = 0f;
        private bool m_isMovementPressed;

        private void Awake()
        {
            m_input = new CharacterInput();
            m_characterController = GetComponent<CharacterController>();
            m_input.PlayerControls.Move.started += OnMovementInput;
            m_input.PlayerControls.Move.canceled += OnMovementInput;
            m_input.PlayerControls.Move.performed += OnMovementInput;
            
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {

            m_currentMovementInput = context.ReadValue<Vector2>();
            m_currentMovement.x = m_currentMovementInput.x;
            m_currentMovement.z = m_currentMovementInput.y;
            m_isMovementPressed = m_currentMovementInput.x != 0 || m_currentMovementInput.y != 0;

            
            
        }

        private bool IsGrounded()
        {
            m_spherePosition = new Vector3(transform.position.x, transform.position.y + groundOffset, transform.position.z);
            if (Physics.CheckSphere(m_spherePosition, m_characterController.radius - 0.05f, groundLayerMask)) return true;
            return false;
        }
        private void ImplementGravity()
        {
            if (!IsGrounded()) m_sphereVelocity.y += gravity * Time.deltaTime;
            else if (m_sphereVelocity.y < 0) m_sphereVelocity.y = -2;

            m_characterController.Move(m_sphereVelocity * Time.deltaTime);
        }

        private void HandleRotation()
        {

        }

        private void GetDirectionAndMove()
        {
            if (m_isMovementPressed)
            {
                m_accelerationTime += Time.deltaTime;
                if (m_currentSpeed < maxMovementSpeed)
                {
                    m_currentSpeed = Mathf.Lerp(m_currentSpeed, maxMovementSpeed, m_accelerationTime / accelerationDuration);
                }
            } else
            {
                m_accelerationTime = 0;
            }
            m_moveDirection = transform.forward * m_currentMovement.z + transform.right * m_currentMovement.x;
            m_characterController.Move(m_moveDirection * m_currentSpeed * Time.deltaTime);
        }

        private void Update()
        {
            GetDirectionAndMove();
            ImplementGravity();
        }

        private void OnEnable()
        {
            m_input.PlayerControls.Enable();
        }

        private void OnDisable()
        {
            m_input.PlayerControls.Disable();
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(m_spherePosition, m_characterController.radius - 0.05f);
        //}
    }

}
