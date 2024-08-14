using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MeleeGirlSkill : MonoBehaviour
{

    public Transform SpellsOriginTransform;
    public Transform meleeUltCurrentTransform;
    public Transform playerPosition;

    public bool ultIsActive;
    public bool ultIsCooldown;
    public float ultMaxCooldown;
    public float ultCurrentCooldown;
    public Image ultCD;
    public bool isUltAnimation;

    public PlayerLocomotion locomotionData;

    //public bool enemyInUltRange;
    //public float attackrange;
    //public LayerMask enemyLayer;

    //public bool ultHitEnemy;

    public Animator playerAnimator;
    public PlayableDirector playableDirector;

    public GameObject ultCam;

    public AudioSource audioUlt;
    public AudioSource audioUltLoop;
    public AudioClip audioClip;
    public AudioClip audioClipLoop;

    void Start()
    {
        locomotionData = GameObject.Find("PlaYer").gameObject.GetComponent<PlayerLocomotion>();
        playerPosition = GameObject.Find("PlaYer").gameObject.transform;
        playerAnimator = GameObject.Find("PlaYer").gameObject.GetComponent<Animator>();
        playableDirector = GameObject.Find("PlaYer").gameObject.GetComponentInChildren<PlayableDirector>();
        meleeUltCurrentTransform.position = SpellsOriginTransform.position;
        ultCurrentCooldown = ultMaxCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        UseMeleeUltX();
        MeleeUltCooldown();
        //DamageEnemy();
    }


    private void UseMeleeUltX()
    {
        StartCoroutine(UseMeleeUlt());
    }
    IEnumerator UseMeleeUlt()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !locomotionData.isMount && !locomotionData.isSliding && !ultIsCooldown)
        {
            audioUlt.clip = audioClip;
            audioUlt.Play();
            audioUltLoop.clip = audioClipLoop;
            audioUltLoop.Play();

            ultCam.SetActive(true);
            ultIsActive = true;
            ultIsCooldown = true;
            meleeUltCurrentTransform.position = playerPosition.position;
            playerAnimator.Play("Ult D");
            playableDirector.Play();
            isUltAnimation = true;
            yield return new WaitForSeconds(4f);
            isUltAnimation = false;
            yield return new WaitForSeconds(6f);
            audioUlt.Stop();
            audioUltLoop.Stop();
            meleeUltCurrentTransform.position = SpellsOriginTransform.position;
            ultIsActive = false;

        }
    }

    private void MeleeUltCooldown()
    {
        if(ultIsCooldown)
        {
            ultCurrentCooldown -= Time.deltaTime;
            if (ultCurrentCooldown <= 0f)
            {
                ultIsCooldown = false;
                ultCurrentCooldown = ultMaxCooldown;
                if (ultCD != null)
                {
                    ultCD.fillAmount = 0f;
                }
            }
            else
            {
                if (ultCD != null)
                {
                    ultCD.fillAmount = ultCurrentCooldown / ultMaxCooldown;
                }

            }

        }
    }
    //private void DamageEnemy()
    //{
    //    enemyInUltRange = Physics.CheckSphere(transform.position, attackrange, enemyLayer);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, attackrange);
    //}
}
