using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using System;
using TMPro;
public class ThirdPersonCC : MonoBehaviour
{
    public int MaxHealth = 100;
    [SerializeField] HealthBar healthBar;
    [SerializeField] public int currentHealth;
    [SerializeField] private Joystick joyStickScript;

    CharacterController characterController;
    Animator animator;
    int isWalkingHash, isRunningHash, isSneakingHash;
    Vector2 currentMovementInput, lookInput;

    [SerializeField] Vector3 currentMovement, currentRunMovement, currentSneakMovement;

    public bool isMovementPressed, isRunPressed;
    public bool isSneakPressed;

    float rotationFactorPerFrame = 15.0f;
    
    [SerializeField] private float runMultipler = 3f, walkMultipler = 1.5f;
    [SerializeField] private InputManager inputManager;

    public bool canMove, isAimPressed;

    public Transform cameraRootTransform;

    [SerializeField] private int currentMode = 1; //1-Explore, 2-Aiming
    [SerializeField] private CinemachineVirtualCamera TPSFollowCam, TPSAimCam;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject crossHairGO;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform, bulletParent;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimDistance = 10f;
    [SerializeField] private Rig aimRig, pistolRig;
    [SerializeField] private GameObject gunGO;
    [SerializeField] private GameObject shootButton, runButton, sneakButton,ammoUIGroup, reloadButtonGO;
    [SerializeField] private ParticleSystem muzzleFlash,hitEffect;
    // if needed [SerializeField] private CinemachinePOV TPSaimCamPov;

    int moveXAnimationParameter;
    int moveZAnimationParameter;

    [SerializeField] private float animationSmoothTime = 0.1f;

    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;

    [SerializeField] float breakTimeAfterGettingHit = 1.2f;
    [SerializeField] SceneManagable sceneManager;
    [SerializeField] AudioSource hurtSFX, dyingSFX, zombieDamageSFX;

    private void Awake()
    {
        currentHealth = MaxHealth;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isSneakingHash = Animator.StringToHash("isSneaking");

        moveXAnimationParameter = Animator.StringToHash("MoveX");
        moveZAnimationParameter = Animator.StringToHash("MoveZ");
    }

    private void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }


    void Update()
    {
        if (canMove)
        {
            getInputs();
            handleCamera();
            handleMovement();
            handleGravity();
            handleRotation();
            handleAnimation();

            if (isRunPressed)
            {
                characterController.Move(currentRunMovement * Time.deltaTime);
            }
            else if (isSneakPressed)
            {
                characterController.Move(currentSneakMovement * Time.deltaTime);
            }
            else
            {
                characterController.Move(currentMovement * Time.deltaTime);
            }
        }
        else
        {
            ////test
            resetAnimation();
            joyStickScript.resetJoyStick();
        }
    }

    private void resetAnimation()
    {
        animator.SetBool(isWalkingHash,false);
        animator.SetBool(isRunningHash, false);

    }

    private void handleCamera()
    {
        //if Aim is pressed and current mode is not aiming then switch aim
        if(isAimPressed && currentMode != 2)
        {
            StartAim();
        }
        //if Aim is not pressed and current mode is aiming then cancel aim
        else if(!isAimPressed && currentMode == 2)
        {
            CancelAim();
        }
    }

    void getInputs()
    {
        isRunPressed = inputManager.runToggleButton;
        isSneakPressed = inputManager.sneakToggleButton;
        isAimPressed = inputManager.aimToggleButton;
        currentMovementInput = inputManager.joystickInput;
        lookInput = inputManager.lookInput;
    }

    void handleMovement()
    {
        Vector3 movementX = Camera.main.transform.right * currentMovementInput.x;
        Vector3 movementZ = Camera.main.transform.forward * currentMovementInput.y;
        Vector3 movement = movementX + movementZ;

        currentMovement.x = movement.x * walkMultipler;
        currentMovement.z = movement.z * walkMultipler;

        currentRunMovement.x = movement.x * runMultipler;
        currentRunMovement.z = movement.z * runMultipler;

        currentSneakMovement.x = movement.x;
        currentSneakMovement.z = movement.z;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    public void cancelMovement()
    {
        if(joyStickScript) joyStickScript.resetJoyStick();
        currentMovement = Vector3.zero;
        currentRunMovement = Vector3.zero;
        currentSneakMovement = Vector3.zero;
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundGravity = -0.05f;
            currentMovement.y = groundGravity;
            currentRunMovement.y = groundGravity;
            currentSneakMovement.y = groundGravity;
        }

        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
            currentSneakMovement.y += gravity * Time.deltaTime;
            //Debug.Log("Not Grounded");
        }
    }

    void handleRotation()
    {
        //rotate character to move direction
        if(currentMode == 1)
        {
            Vector3 postionToLookAt;
            postionToLookAt.x = currentMovement.x;
            postionToLookAt.y = 0.0f;
            postionToLookAt.z = currentMovement.z;

            Quaternion currentRotation = transform.rotation;

            if (isMovementPressed)
            {
                Quaternion targetRotation = Quaternion.LookRotation(postionToLookAt);

                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);

            }
        }
        //rotate character to camera direction
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void handleAnimation()
    {
        if(currentMode == 1)
        {
            bool isWalking = animator.GetBool(isWalkingHash);
            bool isRunning = animator.GetBool(isRunningHash);
            bool isSneaking = animator.GetBool(isSneakingHash);

            if (isMovementPressed && !isWalking)
            {
                animator.SetBool(isWalkingHash, true);
            }

            else if (!isMovementPressed && isWalking)
            {
                animator.SetBool(isWalkingHash, false);
            }

            if ((isMovementPressed && isRunPressed) && !isRunning)
            {
                animator.SetBool(isRunningHash, true);
            }

            else if ((!isMovementPressed || !isRunPressed) && isRunning)
            {
                animator.SetBool(isRunningHash, false);
            }

            if ((isSneakPressed) && !isSneaking)
            {
                animator.SetBool(isSneakingHash, true);
                cameraRootTransform.localPosition = new Vector3(cameraRootTransform.localPosition.x, 0.87f, cameraRootTransform.localPosition.z);
            }

            else if (!isSneakPressed && isSneaking)
            {
                animator.SetBool(isSneakingHash, false);
                cameraRootTransform.localPosition = new Vector3(cameraRootTransform.localPosition.x, 1.256f, cameraRootTransform.localPosition.z);
            }
        }
        
        else if(currentMode == 2)
        {
            currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector,currentMovementInput,ref animationVelocity,animationSmoothTime);
            animator.SetFloat(moveXAnimationParameter, currentAnimationBlendVector.x);
            animator.SetFloat(moveZAnimationParameter, currentAnimationBlendVector.y);

            //set aim target
            aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;
        }
    }

    //Shooting mechanic
    public void StartAim()
    {
        TPSAimCam.Priority = 40;
        TPSFollowCam.Priority = 10;
        currentMode = 2;
        crossHairGO.SetActive(true);
        animator.SetBool("isAiming", true);

        //
        pistolRig.weight = 1;
        aimRig.weight = 1; //1s pistol arm rig
        animator.SetLayerWeight(1, 1); //1s pistol layer in animator

        gunGO.SetActive(true);
        shootButton.SetActive(true);
        reloadButtonGO.SetActive(true);

        sneakButton.SetActive(false);
        runButton.SetActive(false);

        inputManager.resetRunAndSneak();

        if (animator.GetBool("isSneaking"))
        {
            animator.SetBool(isSneakingHash, false);
            cameraRootTransform.localPosition = new Vector3(cameraRootTransform.localPosition.x, 1.256f, cameraRootTransform.localPosition.z);
        }

        if (animator.GetBool("isRunning"))
        {
            animator.SetBool(isRunningHash, false);
        }
        //
    }

    public void CancelAim()
    {
        TPSAimCam.Priority = 10;
        TPSFollowCam.Priority = 40;
        currentMode = 1;
        crossHairGO.SetActive(false);
        animator.SetBool("isAiming", false);

        //
        pistolRig.weight = 0;
        aimRig.weight = 0; //zeros pistol arm rig
        animator.SetLayerWeight(1, 0); //zeros pistol layer in animator
        gunGO.SetActive(false);
        shootButton.SetActive(false);
        sneakButton.SetActive(true);
        runButton.SetActive(true);
        reloadButtonGO.SetActive(false);
        //
    }

    /////////////////////////////

    public void takeDamage(int damageAmount)
    {
        if (hurtSFX)
            hurtSFX.Play();

        if (zombieDamageSFX)
            zombieDamageSFX.Play();

        currentHealth -= damageAmount;
        healthBar.setHealth(currentHealth);
        hitEffect.Play();
        CancelAim();
        canMove = false;

        if(currentHealth > 0)
        {
            inputManager.resetForDamage();
            animator.SetTrigger("GetHit");
            Invoke(nameof(resetCanMove), breakTimeAfterGettingHit);
        }

        else
        {
            die();
        }
    }

    public void die()
    {
        canMove = false;
        if (dyingSFX)
            dyingSFX.Play();

        animator.SetTrigger("Dead");
        sceneManager.characterDeath();
    }

    public void resetCanMove()
    {
        canMove = true;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                //if (Mathf.Abs(lookInput.x) <= moveInputDeadZone)
                //    return 0f;
                //else
                return lookInput.x;

            case "Mouse Y":
                //if (Mathf.Abs(lookInput.y) <= moveInputDeadZone)
                //    return 0f;
                //else
                return lookInput.y;
        }
        return 0f;
    }

}
