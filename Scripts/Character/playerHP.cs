using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.SceneView;

public class playerHP : MonoBehaviour
{
    public WORLDDATA worldData;

    public float currentplayerHP;
    public float maxplayerHP;
    public Animator animator;

    public Slider sliderHP;
    public Transform respawnPoint;
    public GameObject respawnButton;
    public GameObject player;
    public BoxCollider playerboxCollider;

    [SerializeField] bool playerDeath;

    public CameraScipt cameraSettings;

    public ParticleSystem[] reviveBuff;
    public bool reviveBuffTrigger;

    public AudioSource reviveAudio;
    public AudioClip reviveAudioClip;

    public AudioSource hurtAudio;
    public AudioClip hurtAudioClip;

    public CanvasGroup fadeImage;
    public bool fadeTrigger;


    // Start is called before the first frame update
    void Start()
    {
        
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
        currentplayerHP = maxplayerHP;
        animator = GetComponent<Animator>();
        //playerboxCollider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        sliderHP.value = currentplayerHP/maxplayerHP;
        worldData.getPlayerCurrentHP = maxplayerHP;
        UponDeath();
        ReFreshHP();

        if(fadeTrigger)
        {
            fadeImage.alpha += Time.deltaTime;
        }
        else
        {
            fadeImage.alpha -= Time.deltaTime;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            currentplayerHP-=worldData.enemyDamageBonus;
            hurtAudio.clip = hurtAudioClip;
            hurtAudio.Play();
            Debug.Log("Player HP: " + currentplayerHP);
        }
        else if (other.gameObject.tag == "WeaponGateKeeper")
        {
            currentplayerHP-=15;
            hurtAudio.clip = hurtAudioClip;
            hurtAudio.Play();
            Debug.Log("Player HP: " + currentplayerHP);
        }
        else if (other.gameObject.tag == "WeaponMutant")
        {
            currentplayerHP-=worldData.enemyDamageBonus*2;
            hurtAudio.clip = hurtAudioClip;
            hurtAudio.Play();
            Debug.Log("Player HP: " + currentplayerHP);
        }
        else if (other.gameObject.tag == "BossSpell")
        {
            currentplayerHP -= 5;
            hurtAudio.clip = hurtAudioClip;
            hurtAudio.Play();
            Debug.Log("Player HP: " + currentplayerHP);
        }


    }

    private void UponDeath()
    {
        if (currentplayerHP <= 0 && !playerDeath)
        {
            playerDeath = true;
            //currentplayerHP = maxplayerHP;
            playerboxCollider.enabled = false;
            
            StartCoroutine(DeathDelay());
            
        }
    }

    public void Respawn()
    {

        reviveBuffTrigger = true;
        reviveAudio.Play();

        fadeTrigger = true;
        //StartCoroutine(FadeinBlack());
        if (reviveBuffTrigger)
        {

            StartCoroutine(ReviveBuff());
        }
        StartCoroutine(RespwanDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        respawnButton.SetActive(true);
        Cursor.visible = true;
        animator.enabled = false;
        Time.timeScale = 0;
    }

    IEnumerator RespwanDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);

        this.transform.position = respawnPoint.position;
 

        playerboxCollider.enabled = true;
        respawnButton.SetActive(false);
        Cursor.visible = false;


        yield return new WaitForSeconds(1);
        currentplayerHP = maxplayerHP;
        playerDeath = false;

        animator.enabled = true;

        animator.Play("Idle");
        fadeTrigger = false;



    }

    private void ReFreshHP()
    {
        if (worldData.playerRefreshHP)
        {
            maxplayerHP += worldData.playerHealthBonus;
            currentplayerHP = maxplayerHP;
            worldData.playerRefreshHP = false;
        }
    }

    IEnumerator ReviveBuff()
    {
        foreach (var x in reviveBuff)
        {
            x.Emit(1);
        }
        yield return new WaitForSeconds(2f);
        foreach (var x in reviveBuff)
        {
            x.Stop();
        }
        reviveBuffTrigger = false;
    }

    IEnumerator FadeinBlack()
    {
        fadeImage.alpha += Time.deltaTime;

        yield return new WaitForSeconds(3f);

        //fadeImage.alpha -= Time.deltaTime;
    }

}
