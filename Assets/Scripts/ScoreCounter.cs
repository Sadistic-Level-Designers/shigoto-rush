using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public float dodgeScore;
    public float lungeScore;
    public float scoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        dodgeScore = 0;
        lungeScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDodgeScore()
    {
        dodgeScore = dodgeScore + scoreAmount;
    }

    public void ChangeLungeScore()
    {
        lungeScore = lungeScore + scoreAmount;
    }
}
