using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiroshiAnimationScript : MonoBehaviour
{
    private Animator DashAnim;
    void Start()
    {
        DashAnim = GetComponent<Animator>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            DashAnim.SetTrigger("DashTR");
        }
    }
}
