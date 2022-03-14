using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Human,
    Solid,
    EmptySpace,
}

public class ObstacleCollision : MonoBehaviour
{

    public ObjectType type;
    public GameObject particleEffect;
    public GameObject PlayerContainer;
    public string[] audioClip;

    [Header("References")]
    [SerializeField] public Animator animator = null;

    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public float ragdollForce = 500f; // expressed in m/s2

    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();
        ToggleRagdoll(false);
    }
    
    void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach(Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach(Collider collider in ragdollColliders)
        {
            if(collider.gameObject != this.gameObject)
                collider.enabled = state;
        }
    }
  
    void OnTriggerEnter(Collider other)
    {
        // Only execute if is player   
        if(other.gameObject.name != "Player") return;
        Debug.Log("Poof!");

        CharacterState playerState = other.GetComponent<PlayerControl>().state;


        bool displayParticles = false;
        bool emotionalDamage = false;
        bool dodgedObstacle = false;
        bool lungedHuman = false;

        if(audioClip != null && audioClip.Length > 0)
            SoundManager.i.PlayOnce(audioClip[Random.Range(0, audioClip.Length)]);


        //Decides what will happen depending on the object type
        switch (type)
        {
            case ObjectType.Human:
                if(playerState == CharacterState.Lunge) {
                    lungedHuman = true;

                    ToggleRagdoll(true);
                    foreach(Rigidbody rb in ragdollBodies) {
                        rb.AddForce((new Vector3(transform.localPosition.x / 4f, 0.5f, 1f)) * ragdollForce / ragdollBodies.Length);
                    }

                    // cool confetti
                    displayParticles = true;
                } else {
                    // emotional damage
                    emotionalDamage = true;
                }
                break;
            case ObjectType.Solid:
                emotionalDamage = true;
                break;

            case ObjectType.EmptySpace:
                displayParticles = true;
                dodgedObstacle = true;
                break;
        }

        if(displayParticles) {
            GameObject.Instantiate(particleEffect, transform.position, Quaternion.identity);
        }

        if(emotionalDamage) {
            other.GetComponent<PlayerHealth>().PlayerTakeDamage();
        }

        if(dodgedObstacle) {
            PlayerContainer.GetComponent<ScoreCounter>().ChangeDodgeScore();
        }

        if(lungedHuman){
            PlayerContainer.GetComponent<ScoreCounter>().ChangeLungeScore();
        }
    }
}
