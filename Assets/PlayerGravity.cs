using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{

    Vector3 moveVector;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;

        if(controller.isGrounded == false)
        {
            moveVector += Physics.gravity;
        }

        controller.Move(moveVector * Time.deltaTime * Time.deltaTime);
    }
}
