using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
    {
        Run,
        Lunge,
        Dodge,
    }

public class PlayerControl : MonoBehaviour
{
    public Animator DashAnim;
    public float LaneDistance = 5.0f;

    private CharacterController controller;
    public float speed = 8.0f;
    public int chosenLane = 1; //0 = left, 1 = middle, 2 = right
    //public float rotspeed = 45;
    //Vector3 currentEulerAngles;
    //float x;
    //float y;
    //float z;

    public CharacterState state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        state = CharacterState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if(Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);



        Vector3 targetPosition = Vector3.zero;
        if (chosenLane == 0)
            targetPosition += Vector3.left * LaneDistance;
        else if (chosenLane == 2)
            targetPosition += Vector3.right * LaneDistance;

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition.x - transform.position.x) * speed;


        controller.SimpleMove(moveVector);
        // Fixes glitch movment on z axis
        Vector3 tmpPosition = transform.localPosition;
        tmpPosition.z = 0;
        transform.localPosition = tmpPosition;

        // Makes player dash when going left
        if(Input.GetKeyDown(KeyCode.LeftArrow) && state == CharacterState.Run)
        {
            //StartCoroutine(DoLunge());
        }
        // Makes player dash when going right
        if(Input.GetKeyDown(KeyCode.RightArrow) && state == CharacterState.Run)
        {
            //StartCoroutine(DoLunge());
        }
        //switches state from run to lunge
        if(Input.GetKeyDown(KeyCode.UpArrow) && state == CharacterState.Run) 
        {
            //Debug.Log("Lunge activated");
            StartCoroutine(DoLunge());
        }
   

            
    }

    void MoveLane(bool goingRight)
    {
        chosenLane += (goingRight) ? 1 : -1;
        chosenLane = Mathf.Clamp(chosenLane, 0, 2);
    }

    IEnumerator DoLunge()
    {
        state = CharacterState.Lunge;
        //Debug.Log ("State Lunge ");

        //activate lunge animation here
        DashAnim.SetTrigger("DashTR");
        yield return new WaitForSeconds(1);
        DashAnim.SetTrigger("RunTR");
        state = CharacterState.Run;
        //Debug.Log("State Run");
        yield return null;

        
    }


}
