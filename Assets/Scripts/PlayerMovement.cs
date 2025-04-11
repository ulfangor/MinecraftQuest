using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private int isWalkingHash;

    // Vitesse de d�placement en marchant et en courant
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    // Vitesse de rotation pour une transition fluide
    public float rotationSpeed = 10f;

    // R�f�rence � la cam�ra pour orienter le mouvement
    public Transform cameraTransform;

    void Start()
    {
        // Recherche l'Animator dans l'enfant (le personnage)
        animator = GetComponentInChildren<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");

        // D�sactivation du Root Motion pour l'Animator
        animator.applyRootMotion = false;
    }

    void Update()
    {
        // R�cup�re les entr�es clavier pour le d�placement
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z))
            vertical = 1f;
        if (Input.GetKey(KeyCode.S))
            vertical = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
            horizontal = -1f;
        if (Input.GetKey(KeyCode.D))
            horizontal = 1f;

        // Cr�ation du vecteur d'entr�e
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        bool isMoving = inputDirection.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        // Mise � jour des param�tres d'animation
        animator.SetBool(isWalkingHash, isMoving);

        if (isMoving)
        {
            // Calculer la direction "forward" et "right" de la cam�ra sans la composante verticale
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            // Combiner l'input avec les directions de la cam�ra
            Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

            // Choisir la vitesse de d�placement selon que le personnage court ou marche
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            // D�placer le personnage en espace mondial
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

            // Calculer la rotation cible pour regarder dans la direction de d�placement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Appliquer une rotation progressive pour une transition fluide
            // On peut utiliser RotateTowards pour contr�ler l'angle maximum de rotation par frame
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100f);

            // Vous pouvez aussi utiliser Slerp en d�commentant la ligne suivante et en adaptant rotationSpeed :
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
