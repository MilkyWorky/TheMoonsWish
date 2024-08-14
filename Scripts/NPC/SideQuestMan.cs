using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SideQuestMan : MonoBehaviour
{
    public WORLDDATA worldData;
    public BoxCollider boxCollider;
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject questPopUp;
    private Transform cameraPopup;
    [SerializeField] Animator animator;
    [SerializeField] GameObject zombieEnemy;
    [SerializeField] GameObject mutantEnemy;
    public Transform[] spawnpoint;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
        cameraPopup = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        questPopUp.transform.LookAt(cameraPopup.transform);
        SetEnemyQuest();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && worldData.isSideQuestComplete)
        {
            questPopUp.SetActive(true);
            worldData.isInteracting = true;
            textMeshProUGUI.text = "Press F For New Quest";
            Debug.Log("Player Enter");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            questPopUp.SetActive(false);
            worldData.isInteracting = false;
            Debug.Log("Player Exit");
        }
    }

    private void SetEnemyQuest()
    {
        if (worldData.getRandom)
        {
            worldData.enemyType = Random.Range(0, 2);
            worldData.getRandom = false;
        }

        if (worldData.enemyType == 0)
        {
            worldData.isZombie = true;
            worldData.isMutant = false;
        }
        else if (worldData.enemyType == 1)
        {
            worldData.isMutant = true;
            worldData.isZombie = false;

        }
        GetQuest();
    }
    private void GetQuest()
    {
        if(worldData.isInteracting && worldData.isSideQuestComplete && Input.GetKeyDown(KeyCode.F))
        {
            animator.Play("Talking");
            worldData.isGetQuest = true;
            Debug.Log("GetQuest");
            GameObject go;
            
            if (worldData.isZombie)
            {
                textMeshProUGUI.text = "Kill 3 Zombies";
                foreach (var spawn in spawnpoint)
                {
                    Vector3 location = spawn.transform.position;
                    go = Instantiate(zombieEnemy, location, transform.rotation);
                    go.GetComponent<NavMeshAgent>().Warp(location);
                }

            }
            else if (worldData.isMutant)
            {
                textMeshProUGUI.text = "Kill 3 Mutants";
                foreach (var spawn in spawnpoint)
                {
                    Vector3 location = spawn.transform.position;
                    go = Instantiate(mutantEnemy, location, transform.rotation);
                    go.GetComponent<NavMeshAgent>().Warp(location);
                }

            }
        }
    }
}
