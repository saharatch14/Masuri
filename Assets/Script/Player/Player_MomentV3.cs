using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MomentV3 : MonoBehaviour
{
    public bool m_walk = false;
    public float speed = 5.0f;
    public float sensitivity = 3f;
    public GameObject eyes;


    private float moveFB, moveLR, rotX, rotY, vertVelocity;
    public float jumpforce = 4f;
    private CharacterController _charCont;

    private bool hasJumped;

    // Start is called before the first frame update
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetPlayerSteal();
        if (Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
        }
        Cursor.visible = false;
        Gravity();
    }
    void Movement()
    {
        moveLR = Input.GetAxis("Horizontal") * speed;
        moveFB = Input.GetAxis("Vertical") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        rotY = Mathf.Clamp(rotY, -90f, 90f);
        transform.Rotate(0, rotX, 0);

        movement = transform.rotation * movement;

        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        _charCont.Move(movement * Time.deltaTime);
    }

    /*private void Jump()
    {
        //vertVelocity = jumpforce;
        print("jump");
        hasJumped = true;
    }*/

    private void Gravity()
    {
        if(_charCont.isGrounded == true)
        {
            if(hasJumped == false)
            {
                vertVelocity = Physics.gravity.y;
            }
            else
            {
                vertVelocity = jumpforce;
            }
            
        }
        else
        {
            vertVelocity += Physics.gravity.y * Time.deltaTime;
            vertVelocity = Mathf.Clamp(vertVelocity, -50f, jumpforce);
            hasJumped = false;
        }
    }

    public int GetPlayerSteal()
    {
        bool forward = Input.GetKey(KeyCode.W);
        bool left = Input.GetKey(KeyCode.A);
        bool down = Input.GetKey(KeyCode.S);
        bool right = Input.GetKey(KeyCode.D);

        if (forward == true || left == true || right == true || down == true)
        {
            m_walk = true;
            return 0;
        }
        else
        {
            m_walk = false;
            return 1;
        }
    }

}