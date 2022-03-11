using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Callbacks;

public class PrefabAreaPool : MonoBehaviour
{
    public static PrefabAreaPool instance;

    [PostProcessSceneAttribute(0)]
    public static void OnPostprocessScene() {
        instance.loadedAreas = new PrefabArea[instance.preloadAreas.Length];

        // Load all area prefabs
        for(int i = 0; i < instance.preloadAreas.Length; ++i) {
            PrefabArea area = Instantiate(instance.preloadAreas[i], Vector3.zero, Quaternion.identity, instance.transform);
            area.gameObject.SetActive(false);
            instance.loadedAreas[i] = area;
        }

        instance.preloadAreas = null;
    }

    public PrefabArea[] preloadAreas;

    protected PrefabArea[] loadedAreas;

    public PrefabAreaPool() : base() {
        instance = this;
    }

    public PrefabArea obtain(string tag) {
        List<PrefabArea> candidates = new List<PrefabArea>();

        foreach(PrefabArea a in loadedAreas) {
            string found = a.tags.ToList().FirstOrDefault<string>(b => b.Contains(tag));
            if(found != null && !a.gameObject.activeSelf) {
                candidates.Add(a);
            }
        }

        if(candidates.Count > 0)
            return candidates[Random.Range(0, candidates.Count)];
        else
            return null;
    }

    public void release(PrefabArea a) {
        a.gameObject.SetActive(false);
        a.Reset();
        a.transform.position = a.original.position;
        a.transform.rotation = a.original.rotation;
    }
}