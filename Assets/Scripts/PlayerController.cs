using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float steerSpeed = 20;
    public float jumpspeed = 20;
    public float shiftMult = 2;

    private Vector3 startPosition;
    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("hello world");
    }

    void HandleMovement()
     {
        float steertValue = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float gasValue = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float jumpValue = Input.GetAxis("Jump") * jumpspeed * Time.deltaTime;

        steertValue *= Mathf.Sign(gasValue);
        if(gasValue == 0){
            steertValue = 0;
        }

        Vector3 positionChange = Vector3.forward * gasValue;

        if (Input.GetKey(KeyCode.LeftShift)){
            steertValue *= shiftMult;
            positionChange *= shiftMult;
        }

        transform.Translate(Vector3.up * jumpValue);
        transform.Rotate(Vector3.up, steertValue);
        transform.Translate(positionChange);

    }

    void HandleResetPosition()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftShift)){
                ResetPosition(startPosition);
            } else {
                ResetPosition();
            }
        }
    }

    void ResetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    void ResetPosition()
    {
        Vector3 newPos = transform.position;
        newPos.y = 10;
        ResetPosition(newPos);
    }


    // Update is called once per frame
    void Update()
    {
        HandleResetPosition();
        HandleMovement();
    }
}
