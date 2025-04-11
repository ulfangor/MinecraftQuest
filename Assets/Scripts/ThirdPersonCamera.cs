using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("R�f�rences")]
    [Tooltip("Transform du personnage � suivre.")]
    public Transform target;

    [Header("Param�tres de la cam�ra")]
    [Tooltip("Distance de la cam�ra derri�re le personnage.")]
    public float distance = 5.0f;
    [Tooltip("Hauteur de la cam�ra par rapport au personnage.")]
    public float height = 2.0f;
    [Tooltip("Sensibilit� de la souris.")]
    public float mouseSensitivity = 100.0f;
    [Tooltip("Angle vertical minimum (vers le bas).")]
    public float minVerticalAngle = -20.0f;
    [Tooltip("Angle vertical maximum (vers le haut).")]
    public float maxVerticalAngle = 60.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        // Optionnel : verrouiller et masquer le curseur pour une exp�rience immersive
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialiser les angles avec ceux de la cam�ra actuelle
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // R�cup�rer la rotation de la souris pour un contr�le horizontal et vertical
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        // Calculer la rotation combin�e (pitch, yaw)
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        // D�terminer l'offset de la cam�ra (en arri�re et l�g�rement en hauteur)
        Vector3 offset = new Vector3(0, height, -distance);

        // Placer la cam�ra autour du personnage en appliquant la rotation � l'offset
        transform.position = target.position + rotation * offset;
        // Orienter la cam�ra pour qu'elle regarde toujours le personnage
        transform.LookAt(target.position + Vector3.up * height);
    }
}
