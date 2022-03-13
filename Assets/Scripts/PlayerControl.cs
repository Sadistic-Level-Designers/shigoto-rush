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
    private Animator DashAnim;
    public float LaneDistance = 5.0f;

    private CharacterController controller;
    public float speed = 8.0f;
    public int chosenLane = 1; //0 = left, 1 = middle, 2 = right
   

    public CharacterState state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        state = CharacterState.Run;
        DashAnim = GetComponent<Animator>();
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

        // Fixes glitch movment on z axis
        Vector3 tmpPosition = transform.localPosition;
        tmpPosition.z = 0;
        transform.localPosition = tmpPosition;

        controller.Move(moveVector * Time.deltaTime);

        //switches state from run to lunge
        if(Input.GetKeyDown(KeyCode.UpArrow) && state == CharacterState.Run) {
            //Debug.Log("Lunge activated");
            StartCoroutine(DoLunge());

        switch (state)
        {
            case CharacterState.Run:
            DashAnim.SetTrigger("DashTR");
            break;
        }
        switch (state)
        {
            case CharacterState.Lunge:
            DashAnim.SetTrigger("DashTR");
            break;
        }

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
        yield return new WaitForSeconds(2);
        state = CharacterState.Run;
        //Debug.Log("State Run");
        yield return null;

        
    }
}
