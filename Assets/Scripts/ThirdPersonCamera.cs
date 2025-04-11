using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Transform du personnage à suivre.")]
    public Transform target;

    [Header("Paramètres de la caméra")]
    [Tooltip("Distance de la caméra derrière le personnage.")]
    public float distance = 5.0f;
    [Tooltip("Hauteur de la caméra par rapport au personnage.")]
    public float height = 2.0f;
    [Tooltip("Sensibilité de la souris.")]
    public float mouseSensitivity = 100.0f;
    [Tooltip("Angle vertical minimum (vers le bas).")]
    public float minVerticalAngle = -20.0f;
    [Tooltip("Angle vertical maximum (vers le haut).")]
    public float maxVerticalAngle = 60.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        // Optionnel : verrouiller et masquer le curseur pour une expérience immersive
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialiser les angles avec ceux de la caméra actuelle
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Récupérer la rotation de la souris pour un contrôle horizontal et vertical
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        // Calculer la rotation combinée (pitch, yaw)
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        // Déterminer l'offset de la caméra (en arrière et légèrement en hauteur)
        Vector3 offset = new Vector3(0, height, -distance);

        // Placer la caméra autour du personnage en appliquant la rotation à l'offset
        transform.position = target.position + rotation * offset;
        // Orienter la caméra pour qu'elle regarde toujours le personnage
        transform.LookAt(target.position + Vector3.up * height);
    }
}
