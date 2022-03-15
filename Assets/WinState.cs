using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    public float cameraTilt = -10;
    public float duration = 1.2f;
    public float playerSpeed = 8f;

    public RectTransform canvas;

    void Awake() {
        canvas.gameObject.SetActive(false);
    }

    public void Animate() {
        StartCoroutine(DoAnimate());
    }

    IEnumerator DoAnimate() {
        GetComponent<PlayerRailMovement>().speed = playerSpeed;

        float time = duration;

        Transform camera = Camera.main.transform;
        float camA = camera.localEulerAngles.x;
        float camB = cameraTilt;

        camera.SetParent(null, true);

        do {
            Vector3 angles = camera.localEulerAngles;
            angles.x = Mathf.Lerp(camA, camB, 1 - (time/duration));
            camera.localEulerAngles = angles;

            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        } while (time >= 0);

        // show "You Win"
        canvas.gameObject.SetActive(true);

        yield return null;
    }
}
