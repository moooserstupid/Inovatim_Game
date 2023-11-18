using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Player
{

    public class PlayerAiming : MonoBehaviour
    {
        [Header("Throwing Parameters")]
        [SerializeField] private float maxPowerUpDuration = 0.5f;
        [SerializeField] private float initialThrowForce = 5f;
        [SerializeField] private float maxThrowForce = 10f;
        [SerializeField] private float throwUpwardForce = 0f;

        [Header("Camera Aim Parameters")]
        [SerializeField] private Cinemachine.AxisState xAxis, yAxis;
        [SerializeField] private Transform camFollowPos;
        [SerializeField] private Transform attackPoint;

        [Header("Projectile")]
        [SerializeField] private GameObject objectToThrow;

        private CharacterInput m_input;
        private Vector2 m_currentAimInput;
        private Coroutine powerUpCoroutine;

        private float currentPowerUpTimer;
        private void Awake()
        {
            m_input = new CharacterInput();
            m_input.PlayerControls.Aim.started += OnAimInput;
            m_input.PlayerControls.Aim.canceled += OnAimInput;
            m_input.PlayerControls.Aim.performed += OnAimInput;

            m_input.PlayerControls.Fire.started += OnFireInput;
            m_input.PlayerControls.Fire.canceled += OnFireInput;
        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                currentPowerUpTimer = 0;
                if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);

                powerUpCoroutine = StartCoroutine(FirePowerUp());
            }
            else if (context.canceled)
            {
                
                float shootPower = initialThrowForce + ((maxThrowForce - initialThrowForce) * (currentPowerUpTimer / maxPowerUpDuration));

                if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);

                Throw(shootPower);
                //Debug.Log(currentPowerUpTimer);
            }
        }

        private void Throw(float forceMagnitude)
        {
            Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
            objectToThrow.transform.rotation = q * attackPoint.transform.rotation;

            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, q);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            Vector3 forceDirection = attackPoint.forward;



            RaycastHit hit;

            if (Physics.Raycast(camFollowPos.position, camFollowPos.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
                Debug.Log(hit.transform.name);
            }
            

            Vector3 forceToAdd = forceDirection * forceMagnitude + transform.up * throwUpwardForce;

            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        }
        private void OnAimInput(InputAction.CallbackContext context)
        {
            m_currentAimInput = context.ReadValue<Vector2>();
            xAxis.m_InputAxisValue = m_currentAimInput.x;
            yAxis.m_InputAxisValue = m_currentAimInput.y;
        }
        
        private void Update()
        {
            Debug.DrawRay(camFollowPos.position, camFollowPos.forward * 500.0f, Color.red);
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
        }

        private void OnEnable()
        {
            m_input.PlayerControls.Enable();
        }

        private void OnDisable()
        {
            m_input.PlayerControls.Disable();
        }

        IEnumerator FirePowerUp()
        {
            
            while (currentPowerUpTimer < maxPowerUpDuration)
            {
                currentPowerUpTimer += Time.deltaTime;
                yield return null;
            }
        }
    }

}
