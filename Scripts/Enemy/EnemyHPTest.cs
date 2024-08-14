using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPXXX : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    public Transform targetCamera;
    public GameObject canvasFloatingHP;
    public Slider hpSlider;
    public SideQuestScript sidequestScript;
    public CapsuleCollider capsuleCollider;
    public BoxCollider skillBoxCollider;

    public MeleeGirlSkill meleeGirlSkill;
    public float meleeGirlUltDelay;
    public float meleeGirlUltMax;
    public bool meleeGirlUltBool;

    //public enum EnemyType
    //{
    //    Zombie,
    //    Mutant
    //}

    //public EnemyType enemyType;

    void Start()
    {
        Debug.Log("LALALAALAL");
        //meleeGirlSkill = GameObject.Find("UltSkill").GetComponent<MeleeGirlSkill>();
        sidequestScript = GameObject.Find("QuestLog").GetComponent<SideQuestScript>();
        targetCamera = Camera.main.transform;
        currentHP = maxHP;
        capsuleCollider = GetComponent<CapsuleCollider>();

        meleeGirlUltDelay = 0.5f;
    }

    private void Update()
    {
        //canvasFloatingHP.transform.LookAt(targetCamera);
        //hpSlider.value = currentHP / maxHP;
        ////EnemyHPUpdate();

    }

    //private void EnemyHPUpdate()
    //{
    //    if (currentHP == 0)
    //    {
    //        //if (enemyType.Equals(EnemyType.Zombie))
    //        //{
    //        //    sidequestScript.currentEnemyNum--;
    //        //}
    //        canvasFloatingHP.SetActive(false);
    //        capsuleCollider.enabled = false;

    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Weapon")
    //    {
    //        currentHP--;
    //        //if (currentHP == 0)
    //        //{
    //        //    if (enemyType.Equals(EnemyType.Zombie))
    //        //    {
    //        //        sidequestScript.currentEnemyNum--;
    //        //    }
    //        //    canvasFloatingHP.SetActive(false);
    //        //    capsuleCollider.enabled = false;

    //        //}
    //        Debug.Log("ENEMY HP: " + currentHP);
    //    }

    //    //if (other.gameObject.tag == "UltSkill")
    //    //{
    //    //    HitByMeleeUlt();
    //    //}
    //}

    //private void HitByMeleeUlt()
    //{
    //    //if (meleeGirlSkill.enemyInUltRange)
    //    //{
    //    //    meleeGirlUltDelay -= Time.deltaTime;
    //    //    if (meleeGirlUltDelay <= 0f)
    //    //    {
    //    //        meleeGirlUltDelay = meleeGirlUltMax;
    //    //        meleeGirlUltBool = true;
    //    //    }
    //    //    if (meleeGirlUltBool)
    //    //    {
    //    //        currentHP--;
    //    //        Debug.Log(currentHP);
    //    //        meleeGirlUltBool = false;
    //    //    }
    //    //}



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
}
