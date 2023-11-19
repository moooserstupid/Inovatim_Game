using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
        [SerializeField] private GameObject dummyPackage;
        [SerializeField] private GameObject throwerSurface;
        [SerializeField] private Transform maxThrowRotation;

        private CharacterInput m_input;
        private Vector2 m_currentAimInput;
        private Coroutine powerUpCoroutine;
        private Vector3 dummyPackageRestPosition;

        private float m_currentPowerUpTimer;
        private bool m_hasPackage = false;
        private bool m_fireInputPressed = false;
        private void Awake()
        {
            m_input = new CharacterInput();
            m_input.PlayerControls.Aim.started += OnAimInput;
            m_input.PlayerControls.Aim.canceled += OnAimInput;
            m_input.PlayerControls.Aim.performed += OnAimInput;

            m_input.PlayerControls.Fire.started += OnFireInput;
            m_input.PlayerControls.Fire.canceled += OnFireInput;
        }

        private void Start()
        {
            m_hasPackage = false;
            dummyPackageRestPosition = dummyPackage.transform.position;
            dummyPackage.SetActive(false);
        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                m_fireInputPressed = true;
                StartCoroutine(RotateThrower());
                
                if (!m_hasPackage) return;
                m_currentPowerUpTimer = 0;
                if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);

                powerUpCoroutine = StartCoroutine(FirePowerUp());
                
               
            }
            else if (context.canceled)
            {
                m_fireInputPressed = false;
                if (!m_hasPackage) return;
                
                float shootPower = initialThrowForce + ((maxThrowForce - initialThrowForce) * (m_currentPowerUpTimer / maxPowerUpDuration));

                if (powerUpCoroutine != null) StopCoroutine(powerUpCoroutine);

                Throw(shootPower);
                
            }
        }

        private void Throw(float forceMagnitude)
        {
            Quaternion throwRotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
            objectToThrow.transform.rotation = throwRotation * attackPoint.transform.rotation;

            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, throwRotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            Vector3 forceDirection = attackPoint.forward;



            RaycastHit hit;

            if (Physics.Raycast(camFollowPos.position, camFollowPos.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }
            

            Vector3 forceToAdd = forceDirection * forceMagnitude + transform.up * throwUpwardForce;

            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

            m_hasPackage = false;
            dummyPackage.SetActive(false);
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

        private void LoadPackage(Vector3 colliderCenter)
        {
            m_hasPackage = true;
            dummyPackage.SetActive(true);
            Vector3 centerLocalPositionRelativeToPackage = dummyPackage.transform.InverseTransformPoint(colliderCenter);
            Debug.Log(centerLocalPositionRelativeToPackage);
            StartCoroutine(StartLoadingPackage(0.2f, centerLocalPositionRelativeToPackage));
        }

        private void OnEnable()
        {
            m_input.PlayerControls.Enable();
        }

        private void OnDisable()
        {
            m_input.PlayerControls.Disable();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Spawner") && !m_hasPackage)
            {

                other.gameObject.GetComponent<SpawnLocation>().TakePackage();
                LoadPackage(other.bounds.center);
            }
        }
        IEnumerator StartLoadingPackage(float duration, Vector3 loadOrigin)
        {
            float timer = 0;
            Vector3 finalPos = dummyPackage.transform.localPosition;
            dummyPackage.transform.localPosition = loadOrigin;
            
            
            while ( timer < duration)
            {
                timer += Time.deltaTime;
                Debug.Log(dummyPackage.transform.localPosition);
                dummyPackage.transform.localPosition = Vector3.Lerp(dummyPackage.transform.localPosition, finalPos, timer / duration);
                yield return null;
            }
            dummyPackage.transform.localPosition = finalPos;
            yield return null;
        }
        IEnumerator FirePowerUp()
        {
            

            
            while (m_currentPowerUpTimer < maxPowerUpDuration)
            {
                m_currentPowerUpTimer += Time.deltaTime;

                yield return null;
            }
            //dummyPackage.transform.rotation = start;
            
        }

        IEnumerator RotateThrower()
        {
            float currentRotationTimer = 0f;
            Quaternion start = throwerSurface.transform.localRotation;
            Quaternion finish = maxThrowRotation.localRotation;

            while (currentRotationTimer < maxPowerUpDuration && m_fireInputPressed)
            {
                currentRotationTimer += Time.deltaTime;
                throwerSurface.transform.localRotation = Quaternion.Slerp(throwerSurface.transform.localRotation, finish, currentRotationTimer / maxPowerUpDuration);

                yield return null;
            }

            throwerSurface.transform.localRotation = start;

            yield return null;
        }
    }

}
