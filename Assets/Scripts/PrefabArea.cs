using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TransformData {
    public Vector3 position;
    public Quaternion rotation;
}

[RequireComponent(typeof(BoxCollider))]
public class PrefabArea : ResettableBehavior
{
    [HideInInspector] public Transform nextOrigin;
    [HideInInspector] public TransformData original;

    public string[] tags = new string[1];
    
    // Start is called before the first frame update
    void Awake()
    {
        // Save position
        original = new TransformData();
        original.position = transform.position;
        original.rotation = transform.rotation;

        // 
        nextOrigin = transform.Find("NextOrigin");

        // Calculate bounds
        Bounds bounds = new Bounds (transform.position, Vector3.one);
        Renderer[] renderers = GetComponentsInChildren<Renderer> ();
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate (renderer.bounds);
        }

        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 size = bounds.size;
        size.y = 20f;

        Vector3 center = bounds.center - transform.position;
        center.y = size.y / 2f;

        box.size = size;
        box.center = center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasPlayer = false;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player") {
            this.hasPlayer = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Player") {
            this.hasPlayer = false;
        }
    }

    public override void Reset() {

    }
}
