using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject weapon;
    public EnemyHP enemyHP;
    public PlayerLocomotion playerData;


    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
        enemyHP = GetComponent<EnemyHP>();
        playerData = GameObject.Find("PlaYer").GetComponent<PlayerLocomotion>();
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP.currentHP <= 0)
        {
            weapon.SetActive (false);
        }
    }

    public void AttackStart()
    {
        weapon.SetActive(true);
        playerData.isMount = false;
    }

    public void AttackEnd()
    {
        weapon.SetActive(false);
    }
}
