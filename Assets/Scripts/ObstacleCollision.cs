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
    public string[] audioClip;

    [Header("References")]
    [SerializeField] public Animator animator = null;

    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;

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

        if(audioClip != null && audioClip.Length > 0)
            SoundManager.i.PlayOnce(audioClip[Random.Range(0, audioClip.Length)]);


        //Decides what will happen depending on the object type
        switch (type)
        {
            case ObjectType.Human:
                if(playerState == CharacterState.Lunge) {
                    // TODO: increment attack score
                    ToggleRagdoll(true);

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
            // damage to player
            // decrease health etc
        }
    }
}
