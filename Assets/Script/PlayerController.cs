using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject camera;
    private Vector3 offset;

    public float speed = 12.0f;
    public float cameraSpeed = 3.0f;
    private float posX;
    private float posY;
    private Vector3 movement;

    public float jumpForce = 3.0f;
    public bool isGrounded = false;
    public float jumpButton = 0.0f;

    private int points = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        offset = camera.transform.position - transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, posX, 0f) * cameraSpeed);
        rb.AddForce(transform.forward * posY * speed);
    }

    private void LateUpdate() //For camera movements
    {
        camera.transform.position = transform.position;
        camera.transform.Rotate(new Vector3(0f, posX, 0f) * cameraSpeed, Space.World);
        camera.transform.Translate(new Vector3(0, 0, offset.z));

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        posX = movementVector.x;
        posY = movementVector.y;
        
    }

    private void cameraControl()
    {


    }

    IEnumerator increaseSpeed()
    {
        speed += 5f;
        yield return new WaitForSeconds(4f);
        speed -= 5f;
        yield return null;
    }

    /*private void OnJump(InputValue jumpValue)
    {
        jumpButton = jumpValue.Get<float>();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PowerUp")
        {
            points++;
            Debug.Log("Puntuación " + points);
            Destroy(other.gameObject);
            StartCoroutine(increaseSpeed());
        }
    }
}
