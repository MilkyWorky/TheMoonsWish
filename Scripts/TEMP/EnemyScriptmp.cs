using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptmp : MonoBehaviour
{

    public float maxHP;
    public float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("aaa"))
        {
            Destroy(gameObject);
        }
    }
}
