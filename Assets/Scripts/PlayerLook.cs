using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCam;
    private float xRotation = 0f;

    public float xSensitivity = 20f;
    public float ySensitivity = 20f;

    public void Look(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;

        // calcolo e applicazione rotazione della camera sopra/sotto
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // calcolo e applicazione rotazione della camera destra/sinistra
        transform.Rotate(mouseX * Time.deltaTime * xSensitivity * Vector3.up);
    }
}
