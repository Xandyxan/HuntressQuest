using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [Header("Ground Check stuff")]
    [SerializeField] float gravity = -13.0f;
    float velocityY = 0.0f;

    private float actualWalkSpeedX, actualWalkSpeedZ;
    private float walkSpeedZ = 2.0f;
    private float runSpeedZ = 4.0f;
    private float backWalkSpeedZ = 35f;
    private float crouchSpeedZ = 30f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    [Header("Axis Values")]
    private float rawAxisZ, rawAxisX;
    private float axisZ, axisX;

    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;

    //Character States
    private bool isRunning, isCrouched, isWalkingZ, isWalkingX;

    private PlayerView playerView;

    [Header("Other")]
    //[SerializeField] SelectionManager selectionManager;

    private bool canMove, limitedMovement;
    private bool usingCellphone;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        actualWalkSpeedZ = walkSpeedZ;
        playerView = GetComponent<PlayerView>();

        canMove = true;
        limitedMovement = false;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        if (usingCellphone)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            UpdateMovement();
        }


    }

    private void InputProcessing()
    {
        rawAxisX = Input.GetAxisRaw("Horizontal");
        rawAxisZ = Input.GetAxisRaw("Vertical");

        axisX = Input.GetAxis("Horizontal");
        axisZ = Input.GetAxis("Vertical");

        isWalkingZ = Mathf.Abs(rawAxisZ) > 0.01f ? true : false;
        isWalkingX = Mathf.Abs(rawAxisX) > 0.01f ? true : false;

        //if (Input.GetAxis("Horizontal") >= 0 || Input.GetAxis("Vertical") >= 0) { UpdateCollider(); }

        if (!limitedMovement)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                if (isCrouched) isCrouched = false;
                else isCrouched = true;
                isRunning = false;
                playerView.SetIsCrouching(isCrouched);
            }

            if (Input.GetButton("Run"))
            {
                isRunning = true;
                isCrouched = false;
            }
            else isRunning = false;

            if (Input.GetAxisRaw("Vertical") <= 0f) isRunning = false;
        }
    }

    private void UpdateMovement()
    {
        InputProcessing();

        //Get RawMovement and normalize
        Vector2 targetDir = new Vector2(rawAxisX, rawAxisZ);
        targetDir.Normalize();

        //Pass raw values to make a smooth transtion
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Apply gravity
        if (characterController.isGrounded) velocityY = 0.0f;
        velocityY += gravity * Time.deltaTime;

        SmoothWalkSpeedZ();

        //Update the character movements
        Vector3 velocity = (transform.forward * currentDir.y * actualWalkSpeedZ) + (transform.right * currentDir.x * actualWalkSpeedX) + Vector3.up * velocityY;
        characterController.Move(velocity * 5 * Time.deltaTime);

        //SetAnimators();
    }

    private void SmoothWalkSpeedZ() //It's to make velocity changes in a smooth way getting the smooth currentDir
    {
        if (!isCrouched && !isRunning)
        {
            if (currentDir.y > -0.0001f && currentDir.y < 0.0001f) //From any state to -> To idle stand state
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, 0, Time.deltaTime * 10f);

            else if (currentDir.y <= 0f) //From any state to -> To walkingBack stand state
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, backWalkSpeedZ, Time.deltaTime * 10f);

            else if (currentDir.y > 0f) //From any state to -> To walk forward stand state
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, walkSpeedZ, Time.deltaTime * 10f);

            actualWalkSpeedX = Mathf.Lerp(actualWalkSpeedX, walkSpeedZ, Time.deltaTime * 50f);
        }
        else if (isRunning && !isCrouched)
        {
            if (currentDir.y > 0f) //Se está correndo em pé
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, runSpeedZ, Time.deltaTime * 2f);

            actualWalkSpeedX = Mathf.Lerp(actualWalkSpeedX, runSpeedZ, Time.deltaTime * 2f);
        }
        else if (isCrouched && !isRunning)
        {
            if (currentDir.y > -0.0001f && currentDir.y < 0.0001f) //From any state to -> To idle crouch state
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, crouchSpeedZ, Time.deltaTime * 50f);

            else if (currentDir.y > 0f && isCrouched && !isRunning) //From any state to -> To walk crouch state
                actualWalkSpeedZ = Mathf.Lerp(actualWalkSpeedZ, crouchSpeedZ, Time.deltaTime * 50f);

            actualWalkSpeedX = Mathf.Lerp(actualWalkSpeedX, crouchSpeedZ, Time.deltaTime * 50f);
        }



        if (limitedMovement)
        {
            actualWalkSpeedZ = Mathf.Clamp(actualWalkSpeedZ, 0, 0.5f);
            actualWalkSpeedX = Mathf.Clamp(actualWalkSpeedX, 0, 0.5f);
        }
        else
        {
            actualWalkSpeedZ = Mathf.Clamp(actualWalkSpeedZ, 0, runSpeedZ);
            actualWalkSpeedX = Mathf.Clamp(actualWalkSpeedX, 0, runSpeedZ);
        }
    }

    private void SetAnimators()
    {
        if (!limitedMovement)
        {
            animator.SetBool("isWalkingZ", isWalkingZ);
            animator.SetBool("isWalkingX", isWalkingX);
            animator.SetBool("isCrouched", isCrouched); 

            animator.SetFloat("VelocityZ", axisZ * actualWalkSpeedZ);
            animator.SetFloat("VelocityX", axisX * actualWalkSpeedX);
        }
        else
        {
            animator.SetBool("isWalkingZ", isWalkingZ);
            animator.SetBool("isWalkingX", isWalkingX);

            animator.SetFloat("VelocityZ", axisZ * actualWalkSpeedZ * 3.7f);
            animator.SetFloat("VelocityX", axisX * actualWalkSpeedX * 1.7f);
        }

    }

    private void UpdateCollider()
    {
        if (!isCrouched)
        {
            characterController.stepOffset = 0.150f;
            characterController.center = new Vector3(0, 0.0775f, 0);
            characterController.radius = 0.03f;
            characterController.height = 0.155f;
        }
        else
        {
            characterController.stepOffset = 0f;
            characterController.center = new Vector3(0, 0.0775f, 0.02f);
            characterController.radius = 0.033f;
            characterController.height = 0.155f;
        }
    }

    //getters
    public bool GetIsRunning() { return this.isRunning; }
    public bool GetIsWalking()
    {
        if (this.currentDir.y >= 0.05f) return true;
        else return false;
    }
    public bool GetIsCrouched() { return this.isCrouched; }

    // Turn Controller On and Off
    public void TurnPlayerControllerOn()
    {
        canMove = true;
        usingCellphone = false;
        this.enabled = true;
        // print(" PCtrl ON");
    }

    public void PlayerDuringDialogueOn() { limitedMovement = true; }

    public void PlayerDuringDialogueOff() { limitedMovement = false; }
}
