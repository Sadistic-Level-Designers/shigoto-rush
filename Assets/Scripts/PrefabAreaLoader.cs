using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AreaDescriptor {
    public string tag;
    public float duration;
    public float playerSpeed;
    public GameObject obj;
}

public class PrefabAreaLoader : MonoBehaviour
{
    public PrefabArea prev;
    public PrefabArea curr;
    public PrefabArea next;

    public int areaIndex = -1;

    public AreaDescriptor[] areas = new AreaDescriptor[13];

    public static PrefabAreaLoader instance;
    public PrefabAreaLoader() : base() {
        instance = this;
    }

    void Start()
    {
        // LoadArea 3 times
        LoadArea();
        LoadArea();
        LoadArea();
    }

    void Update()
    {
        // Load next area as player advances
        if(next != null && next.hasPlayer && areaIndex < areas.Length) {
            LoadArea();
        }
    }

    void LoadArea() {
        // Unload prev
        if(prev != null)
            PrefabAreaPool.instance.release(prev);

        // Set previous
        prev = curr;

        // Set current
        PlayerRailMovement player = GameObject.FindObjectOfType<PlayerRailMovement>();
        if(areaIndex >= 0) {
            ref AreaDescriptor currDesc = ref areas[areaIndex];
            curr = next;
            Debug.Log("Player in area " + areaIndex);

            // Set player speed
            if(curr != null) {
                currDesc.playerSpeed = curr.GetComponent<BoxCollider>().bounds.size.z / currDesc.duration;
                player.speed = currDesc.playerSpeed;
                player.transform.position = curr.transform.position;
            }
        }

        // Find next
        if(areaIndex < areas.Length - 1) {
            ref AreaDescriptor nextDesc = ref areas[++areaIndex];
            PrefabArea area = PrefabAreaPool.instance.obtain(nextDesc.tag);
            if(area == null) {
                Debug.LogWarning("Not found area with requested tag: " + nextDesc.tag);
                Debug.Break();
            }

            // Load next
            Transform nextOrigin = curr != null ? curr.nextOrigin : this.transform;
            area.transform.position = nextOrigin.position;
            area.transform.rotation = nextOrigin.rotation;
            next = area;
            next.gameObject.SetActive(true);
            nextDesc.obj = next.gameObject;

            // Debug.Log("Loaded area: " + );
        }
    }
}
