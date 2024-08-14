using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocomotion : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private BoxCollider playerHurtbox;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float rotationSpeed;

    public float turnSmoothTime = 0.1f;
    public float turnSmothVelocity;
    float targetAngleget;

    public float ySpeed;

    public AxisState xAxis;
    public AxisState yAxis;
    public Transform cameraLookAt;
    private Camera mainCamera;
    public float turnSpeed = 15f;

    [SerializeField] GameObject weapon;

    public playerHP playerHP;

    public float attackMagtitude;


    public bool isSliding;
    public bool slidingIsCooldown;
    public float slidingMaxCooldown;
    public float slidingCurrentCooldown;
    public Image slidngCD;
    public bool resetRotation;

    public bool isMount;
    public bool wolfIsCooldown;
    public float wolfMaxCooldown;
    public float wolfCurrentCooldown;
    public Image wolfCD;
    public GameObject wolf;
    public GameObject wolfPlayerModel;
    private Animator wolfAnimator;
    public float mountSpeed;


    //public GameObject kick1;
    //public GameObject kick2;
    //public GameObject kick3;

    public ParticleSystem[] k1;
    public ParticleSystem[] k2;
    public ParticleSystem[] k3;

    public MeleeGirlSkill meleeGirlUlt;

    [Header("AUDIO")]
    public AudioSource audioSrc;
    public AudioSource audioVoice;
    public AudioClip audioRunning;
    public AudioClip audioKick;
    public AudioClip audioSliding;
    public AudioClip audioVoiceattack1;
    public AudioClip audioVoiceattack2;
    public AudioClip audioVoiceattack3;
    public AudioClip audioGirlUlt;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        wolfAnimator = GameObject.Find("WolfMount").GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerHP = GetComponent<playerHP>();
        playerHurtbox = GetComponent<BoxCollider>();
        weapon.SetActive(false);
        slidingCurrentCooldown = slidingMaxCooldown;
        wolfCurrentCooldown = wolfMaxCooldown;
        meleeGirlUlt = GameObject.Find("UltSkill").gameObject.GetComponent<MeleeGirlSkill>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHP.currentplayerHP <= 0)
        {
            animator.Play("Death");
            Debug.Log("player dead");
        }



        PlayerAttack();
        StartCoroutine(PlayerEvade());
        SlidingCooldown();
        
        PlayerMovement();
        PlayerIFRAME();

        Mount();
        WolfCooldown();
    }

    IEnumerator PlayerEvade()
    {
        if (Input.GetMouseButton(1) && !isMount && !isSliding && !slidingIsCooldown)
        {
            animator.Play("Slide");
            isSliding = true;
            slidingIsCooldown = true;
            yield return new WaitForSeconds(2f);
            isSliding = false;
            
        }
    }

    private void SlidingCooldown()
    {
        if(slidingIsCooldown)
        {
            slidingCurrentCooldown -= Time.deltaTime;
            if (slidingCurrentCooldown <= 0f)
            {
                slidingIsCooldown = false;
                slidingCurrentCooldown = slidingMaxCooldown;
                if (slidngCD != null)
                {
                    slidngCD.fillAmount = 0f;
                }
            }
            else
            {
                if (slidngCD != null)
                {
                    slidngCD.fillAmount = slidingCurrentCooldown/slidingMaxCooldown;
                }
            }


        }
    }

    private void PlayerIFRAME()
    {
        if(isSliding)
        {
            playerHurtbox.enabled = false;
        }
        else
        {
            playerHurtbox.enabled = true;
        }
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButton(0) &&!isSliding && !isMount && !animator.GetBool("Combo1") && !meleeGirlUlt.isUltAnimation && playerHP.currentplayerHP >0)
        {
            //animator.SetFloat("AttackMagtitude", 0f);
            animator.Play("Attack1");
            
        }

        if (Input.GetMouseButton(0) && !isSliding && !isMount && animator.GetBool("Combo1") && !animator.GetBool("Combo2") && !meleeGirlUlt.isUltAnimation && playerHP.currentplayerHP > 0)
        {
            //animator.SetFloat("AttackMagtitude", 0.5f);
            animator.Play("Attack2");
            

        }

        if (Input.GetMouseButton(0) && !isSliding && !isMount && animator.GetBool("Combo2") && animator.GetBool("Combo1") && !meleeGirlUlt.isUltAnimation && playerHP.currentplayerHP > 0)
        {
            //animator.SetFloat("AttackMagtitude", 1f, 0.05f, Time.deltaTime);
            animator.Play("Attack3");
            
        }


    }

    private void AudioGirlUlt()
    {
        audioSrc.clip = audioGirlUlt; audioSrc.Play();
    }

    private void AudioRunning()
    {
        audioSrc.clip = audioRunning;
        audioSrc.Play();
    }

    private void AudioSliding()
    {
        audioSrc.clip = audioSliding; audioSrc.Play();
    }
    private void AudioKick()
    {
        audioSrc.clip = audioKick; audioSrc.Play();
    }
    private void AudioVoiceKick1()
    {
        audioVoice.clip = audioVoiceattack1; audioVoice.Play();
    }
    private void AudioVoiceKick2()
    {
        audioVoice.clip = audioVoiceattack2; audioVoice.Play();
    }
    private void AudioVoiceKick3()
    {
        audioVoice.clip = audioVoiceattack3; audioVoice.Play();
    }

    private void EndCamUlt()
    {
        meleeGirlUlt.ultCam.SetActive(false);
    }
    private void RestartMoveRotationTrue()
    {
        resetRotation = true;
    }

    private void RestartMoveRotationFalse()
    {
        resetRotation = false;
    }

    private void PlayerComboEnter1()
    {
        animator.SetBool("Combo1", true);
    }

    //private void PlayerComboExit1()
    //{
    //    animator.SetBool("Combo1", false);
    //}

    private void PlayerComboEnter2()
    {
        animator.SetBool("Combo2", true);
    }

    //private void PlayerComboExit2()
    //{
    //    animator.SetBool("Combo2", false);
    //}

    private void PlayerComboReset()
    {
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
    }

    private void PlayerAttackEnter()
    {
        weapon.SetActive(true);
    }

    private void PlayerAttackEnd()
    {
        weapon.SetActive(false);
    }

    private void Kick1()
    {
        //k1.Play();
        foreach (var particle in k1)
        {
            particle.Emit(1);
        }
    }

    private void Kick2()
    {
        //k2.Play();
        foreach (var particle in k2)
        {
            particle.Emit(1);
        }
    }

    private void Kick3()
    {
        //k3.Play();
        foreach (var particle in k3)
        {
            particle.Emit(1);
        }
    }

    //private void NoKick()
    //{
    //    kick1.SetActive(false); kick2.SetActive(false);kick3.SetActive(false);
    //}



    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        ySpeed += Physics.gravity.y * Time.deltaTime;



        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            wolfAnimator.SetBool("IsMoving", true);

            //PlayerRotationMove();

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            if (resetRotation)
            {
                transform.rotation = transform.rotation;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            

        }
        else
        {
            animator.SetBool("IsMoving", false);
            wolfAnimator.SetBool("IsMoving", false);
        }
    }

    //private void PlayerMovementNOPE()
    //{
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");
    //    Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    //    float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


    //    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    //    {
    //        inputMagnitude /= 2;
    //    }

    //    animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

    //    ySpeed += Physics.gravity.y * Time.deltaTime;

    //    if (movementDirection.magnitude > 0)
    //    {
    //        float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
    //        targetAngleget = targetAngle;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmothVelocity, turnSmoothTime);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //    }

    //    if (movementDirection != Vector3.zero)
    //    {
    //        animator.SetBool("IsMoving", true);

    //        ////PlayerRotationMove();

    //        //Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

    //        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        animator.SetBool("IsMoving", false);
    //    }




    //}

    //private void PlayerRotationMove()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        xAxis.Update(Time.fixedDeltaTime);
    //        yAxis.Update(Time.fixedDeltaTime);

    //        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

    //        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    //    }
    //}


    private void Mount()
    {

        StartCoroutine(IsMountSwitch());

        if(isMount)
        {
            wolfPlayerModel.SetActive(false);
            wolf.SetActive(true);
        }
        else
        {
            wolfPlayerModel.SetActive(true);
            wolf.SetActive(false);
        }
    }

    private void OnAnimatorMove()
    {
        if (isMount)
        {
            mountSpeed = 10;
        }
        else
        {
            mountSpeed = 1;
        }

        Vector3 velocity = animator.deltaPosition * mountSpeed;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);
    }

    private void WolfCooldown()
    {
        if (wolfIsCooldown)
        {
            wolfCurrentCooldown -= Time.deltaTime;
            if (wolfCurrentCooldown <= 0f)
            {
                wolfIsCooldown = false;
                wolfCurrentCooldown = wolfMaxCooldown;
                if (wolfCD != null)
                {
                    wolfCD.fillAmount = 0f;
                }
            }
            else
            {
                if (wolfCD != null)
                {
                    wolfCD.fillAmount = wolfCurrentCooldown / wolfMaxCooldown;
                }
            }


        }
    }

    IEnumerator IsMountSwitch()
    {
        if (isMount)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isSliding)
            {
                
                isMount = false;
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            if(!isMount)
            {
                if (Input.GetKeyDown(KeyCode.X) && !isSliding && wolfCurrentCooldown == wolfMaxCooldown)
                {
                    wolfIsCooldown = true;
                    isMount = true;
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}
