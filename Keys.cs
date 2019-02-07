using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class Keys : MonoBehaviour
{

	public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController CharContr;
    // Start is called before the first frame update
    void Start()
    {
        CharContr = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector3.forward * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector3.back * Time.deltaTime);
        }

    }

    void Move(Vector3 movement)
    {

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        CharContr.Move(movement);

    }


}
