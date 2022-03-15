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
    public GameObject playerContainer;
    public string[] audioClip;

    [Header("References")]
    [SerializeField] public Animator animator = null;

    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public float ragdollForce = 500f; // expressed in m/s2

    void Start()
    {
        playerContainer = GameObject.Find("PlayerContainer");
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        TryGetComponent<Animator>(out animator);
        ToggleRagdoll(false);
    }
    
    void ToggleRagdoll(bool state)
    {
        if(animator != null)
            animator.enabled = !state;

        foreach(Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach(Collider collider in ragdollColliders)
        {
            if(collider.gameObject != this.gameObject && !collider.gameObject.name.Contains("Cube"))
                collider.enabled = state;
        }
    }
  
    void OnTriggerEnter(Collider other)
    {
        // Only execute if is player   
        if(other.gameObject.name != "Player") return;
        Debug.Log("Poof!");

        PlayerControl player = other.GetComponent<PlayerControl>();
        CharacterState playerState = player.state;


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
                Debug.Log("DODGED");
                break;
        }

        if(displayParticles) {
            GameObject.Instantiate(particleEffect, player.transform.position, Quaternion.identity);
        }

        if(emotionalDamage) {
            other.GetComponent<PlayerHealth>().PlayerTakeDamage();
        }

        if(dodgedObstacle) {
            playerContainer.GetComponent<ScoreCounter>().ChangeDodgeScore();
            Debug.Log("Gottem");
        }

        if(lungedHuman){
            playerContainer.GetComponent<ScoreCounter>().ChangeLungeScore();
        }
    }
}
