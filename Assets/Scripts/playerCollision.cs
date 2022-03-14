using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Human,
    Solid,
    EmptySpace,
}

public class playerCollision : MonoBehaviour
{

    public ObjectType type;
    public GameObject particleEffect;
    public string[] audioClip;

    [Header("References")]
    [SerializeField] private Animator animator = null;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    public float ragdollForce = 500f; // expressed in m/s2

    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

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

        if(audioClip != null && audioClip.Length > 0)
            SoundManager.i.PlayOnce(audioClip[Random.Range(0, audioClip.Length)]);

    
        //Decides what will happen depending on the object type
        switch (type)
        {
            case ObjectType.Human:
                if(playerState == CharacterState.Lunge) {
                    // TODO: increment attack score
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
                // TODO: increment dodge score
                break;
        }

        if(displayParticles) {
            GameObject.Instantiate(particleEffect, transform.position, Quaternion.identity);
        }

        if(emotionalDamage) {
            other.GetComponent<PlayerHealth>().PlayerTakeDamage();
        }
    }
}
