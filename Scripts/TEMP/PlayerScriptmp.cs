using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptmp : MonoBehaviour
{
    public Animator animator;
    public Slider slider;
    public float MaxHP;
    public float currentHP;
    public GameObject weapTemp;
    public bool canAttack;


    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = MaxHP;
    }


    private void Update()
    {
        slider.value = currentHP * MaxHP / 100;
        StartCoroutine(Kick());
    }


    //private void Kick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        animator.Play("Kick");
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        currentHP--;
        yield return new WaitForSeconds(3f);

    }

    IEnumerator Kick()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.Play("Kick");
            weapTemp.SetActive(true);
            canAttack = false;
            yield return new WaitForSeconds(2f);
            canAttack = true;
            weapTemp.SetActive(false);
        }

    }

}
