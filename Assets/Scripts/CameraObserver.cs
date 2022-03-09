
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraObserver : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        ObservableBehavior observable;

        if(other.transform.TryGetComponent<ObservableBehavior>(out observable)) {
            observable.enabled = true;
        }
    }

    void OnTriggerExit(Collider other) {
        ObservableBehavior observable;

        if(other.transform.TryGetComponent<ObservableBehavior>(out observable)) {
            observable.enabled = false;
        }
    }
}
