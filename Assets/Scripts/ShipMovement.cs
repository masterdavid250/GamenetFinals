using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    //public GameObject shipItself;
    //public GameObject ballista;

    [Header("Inherited Values")]
    [Space]

    public float forwardSpeed;
    public float backwardSpeed;
    public float boatRotationSpeed = 100f;
    public float acceleration;
    public float maxVelocity;

    private float currentVelocity = 0f;
    private Rigidbody boatRigidbody;

    public bool isControlEnabled = true;

    //public AudioClip cannonFire;
    //public AudioClip shipImpact;
    //private AudioSource audioSource;

    private void Start()
    {
        boatRigidbody = this.GetComponent<Rigidbody>();
        //isControlEnabled = false;
        //audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isControlEnabled)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            currentVelocity += verticalInput * acceleration * Time.deltaTime;
            currentVelocity = Mathf.Clamp(currentVelocity, -backwardSpeed, maxVelocity);
            float moveSpeed = (verticalInput >= 0f) ? forwardSpeed : backwardSpeed;
            Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
            boatRigidbody.MovePosition(boatRigidbody.position + moveDirection);
            float rotationAmount = horizontalInput * boatRotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
            boatRigidbody.MoveRotation(boatRigidbody.rotation * rotation);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        /*if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            if (this.gameObject != null)
            {
                audioSource.clip = shipImpact;
                audioSource.Play();
            }
            this.gameObject.GetComponentInParent<Stats>().GiveDamage(10);
        }*/
    }
}
