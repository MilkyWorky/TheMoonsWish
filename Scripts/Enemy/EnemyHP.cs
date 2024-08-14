using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public WORLDDATA worldData;

    public float currentHP;
    public float maxHP;
    public float maxHPCurrent;

    public Transform targetCamera;
    public GameObject canvasFloatingHP;
    public Slider hpSlider;
    public SideQuestScript sidequestScript;
    public CapsuleCollider capsuleCollider;
    public BoxCollider boxCollider;
    public BoxCollider skillBoxCollider;

    public MeleeGirlSkill meleeGirlSkill;
    public float meleeGirlUltDelay;
    public float meleeGirlUltMax;
    public bool meleeGirlUltBool;

    [SerializeField] bool enemyDeathtrigger;

    public BossData bossData;

    [Header("AUDIO")]
    public AudioSource audioHit;
    public AudioSource audioAttack;
    public AudioClip audioisHit;
    public AudioClip audioIdle;
    public AudioClip audioDeath;

    public enum EnemyType
    {
        Zombie,
        Mutant,
        GateKeeper,
        Boss
    }

    public EnemyType enemyType;

    private void Start()
    {
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
        //Debug.Log("LALALAALAL");
        meleeGirlSkill = GameObject.Find("UltSkill").GetComponent<MeleeGirlSkill>();
        sidequestScript = GameObject.Find("QuestLog").GetComponent<SideQuestScript>();
        canvasFloatingHP = transform.GetChild(1).gameObject;
        hpSlider = transform.GetComponentInChildren<Slider>();
        targetCamera = Camera.main.transform;
        maxHP += worldData.enemyHPBonus;
        maxHPCurrent = maxHP;
        currentHP = maxHPCurrent;
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponent<BoxCollider>();
        enemyDeathtrigger = true;

        bossData = GetComponent<BossData>();
        meleeGirlUltDelay = 0.5f;


    }

    private void Update()
    {
        canvasFloatingHP.transform.LookAt(targetCamera);
        //hpSlider.value = currentHP/ (maxHP + worldData.enemyHPBonus);
        hpSlider.value = currentHP / maxHPCurrent;
        EnemyHPUpdate();

    }

    private void AudioIdle()
    {
        audioAttack.clip = audioIdle;
        audioAttack.Play();
    }

    private void EnemyHPUpdate()
    {
        if (currentHP <= 0 && enemyDeathtrigger)
        {
            enemyDeathtrigger = false ;
            audioAttack.clip = audioDeath; audioAttack.Play();

            if (enemyType.Equals(EnemyType.GateKeeper))
            {
                worldData.gateKeeperNum--;
            }

            if (enemyType.Equals(EnemyType.Zombie))
            {
                if (worldData.isZombie)
                {
                    sidequestScript.currentEnemyNum--;
                    Debug.Log("Quest-");
                }

            }
            else if (enemyType.Equals(EnemyType.Mutant))
            {
                if (worldData.isMutant)
                {
                    sidequestScript.currentEnemyNum--;
                    Debug.Log("Quest-");
                }

            }
            canvasFloatingHP.SetActive(false);
            boxCollider.enabled = false;
            capsuleCollider.enabled = false;
            if(bossData!=null)
            {
                bossData.bossIsDead = true;
            }

            Destroy(gameObject, 10);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            currentHP = currentHP - worldData.playerDamageBonus;
            //if (currentHP == 0)
            //{
            //    if (enemyType.Equals(EnemyType.Zombie))
            //    {
            //        sidequestScript.currentEnemyNum--;
            //    }
            //    canvasFloatingHP.SetActive(false);
            //    capsuleCollider.enabled = false;

            //}
            Debug.Log("ENEMY HP: " + currentHP);

            audioHit.clip = audioisHit;
            audioHit.Play();
        }


    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("olalal");
        if (other.gameObject.tag == "aaa" && currentHP >= 0)
        {
            HitByMeleeUlt();

        }
    }



    private void HitByMeleeUlt()
    {
        //if (meleeGirlSkill.enemyInUltRange)
        //{
        //    meleeGirlUltDelay -= Time.deltaTime;
        //    if (meleeGirlUltDelay <= 0f)
        //    {
        //        meleeGirlUltDelay = meleeGirlUltMax;
        //        meleeGirlUltBool = true;
        //    }
        //    if (meleeGirlUltBool)
        //    {
        //        currentHP--;
        //        Debug.Log(currentHP);
        //        meleeGirlUltBool = false;
        //    }
        //}



        meleeGirlUltDelay -= Time.deltaTime;
        if (meleeGirlUltDelay <= 0f)
        {
            meleeGirlUltDelay = meleeGirlUltMax;
            meleeGirlUltBool = true;
        }
        if (meleeGirlUltBool)
        {
            //currentHP--;
            currentHP = currentHP - worldData.playerDamageBonus;

            meleeGirlUltBool = false;
        }
    }
}
