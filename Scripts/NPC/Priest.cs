using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public WORLDDATA worldData;
    [SerializeField] GameObject popUpBox;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Animator animator;
    private Transform cameraPopup;

    public ParticleSystem[] lvlupBuff;
    public bool lvlupBuffTrigger;



    public AudioSource audioSrc;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraPopup = Camera.main.transform;
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
    }

    // Update is called once per frame
    void Update()
    {
        popUpBox.transform.LookAt(cameraPopup.transform);
        LevelUP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popUpBox.SetActive(true);
            worldData.isInteractingPriest = true;
            if(worldData.lvlUPBonus == 0 )
            {
                animator.Play("Wave");
                textMeshProUGUI.text =
                    //"Thank you for your help <br> <br>" +
                    "Lvl: " + worldData.playerLVL + "<br>" +
                    "Damage: " + worldData.playerDamageBonus + "<br>" +
                    "HP: " + worldData.getPlayerCurrentHP;
            }
            else if (worldData.lvlUPBonus != 0)
            {
                animator.Play("Talking");
                textMeshProUGUI.text =
                    "Press F to Level UP <br> <br>" +
                    "Lvl: " + worldData.playerLVL + " -> " + (worldData.playerLVL + worldData.lvlUPBonus) + "<br>" +
                    "Damage: " + worldData.playerDamageBonus+ " -> " + (worldData.playerDamageBonus + worldData.lvlUPBonus) + "<br>" +
                    "HP: " + worldData.getPlayerCurrentHP+ " -> " + (worldData.getPlayerCurrentHP + worldData.lvlUPBonus * 10);
            }

        }
        Debug.Log("Works");
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    animator.Play("Talking");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            worldData.isInteractingPriest = false;
            popUpBox.SetActive(false);
        }
    }

    private void LevelUP()
    {
        if (worldData.isInteractingPriest && worldData.lvlUPBonus != 0 && Input.GetKeyDown(KeyCode.F))
        {
            worldData.enemyDamageBonus++;
            worldData.playerLVL += worldData.lvlUPBonus;
            worldData.playerDamageBonus += worldData.lvlUPBonus;
            worldData.playerHealthBonus = worldData.lvlUPBonus * 10 ;
            worldData.playerRefreshHP = true;
            worldData.enemyHPBonus += worldData.lvlUPBonus * 10;
            worldData.lvlUPBonus = 0;

            audioSrc.clip = audioClip;
            audioSrc.Play();

            lvlupBuffTrigger = true;
            if(lvlupBuffTrigger)
            {
                StartCoroutine(LvlUpBuff());
            }




        }
    }

    IEnumerator LvlUpBuff()
    {
        foreach (var x in lvlupBuff)
        {
            x.Emit(4);
        }
        yield return new WaitForSeconds(2f);
        foreach (var x in lvlupBuff)
        {
            x.Stop();
        }
        lvlupBuffTrigger =false;
    }


}
