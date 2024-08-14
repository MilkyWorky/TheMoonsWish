using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossData : MonoBehaviour
{
    [Header("Reference")]
    public Transform playerTransform;
    public Animator animator;
    public Slider bossHPSlider;
    public EnemyHP enemyHP;
    public GameObject enemyHPCanvas;
    public TextMeshProUGUI textMeshProUGUI;

    public GameObject temp;

    [Header("Boss Quest")]
    public bool isBoss;
    public bool bossIsDead;

    [Header("Spell")]
    public bool isUsingSpell;
    public float spellCooldown;
    public float maxCooldown;
    public Transform spellTransform;
    public AudioSource spellAudioSource;


    [Header("Spell Mechanic")]
    public float spellCount;
    public float spellMaxCount;
    public bool spellCountBool;
    public bool spellCountStart;
    public bool spellReset;
    public Transform spellCountLocation;
    public ParticleSystem[] spellParticle;
    public CapsuleCollider spellCollider;
    public bool playerInCombatRange;
    public LayerMask playerLayer;
    public float attackrange;



    // Start is called before the first frame update
    void Start()
    {
        isBoss = true;
        spellCount = spellMaxCount;
        animator = GetComponent<Animator>();
        enemyHP = GetComponent<EnemyHP>();

    }

    // Update is called once per frame
    void Update()
    {
        BossIsDead();
        bossHPSlider.value = enemyHP.hpSlider.value;
        playerInCombatRange = Physics.CheckSphere(transform.position, attackrange, playerLayer);
        if (playerInCombatRange && !bossIsDead)
        {
            enemyHPCanvas.SetActive(true);
            if (spellCountBool )
            {
                spellCount -= Time.deltaTime;
                if (spellCount <= 0)
                {
                    isUsingSpell = true;
                    StartCoroutine(BossSpell());
                    if (spellReset)
                    {
                        StartCoroutine(BossSpellReset());
                    }
                }

            }
        }
        else
        {
            enemyHPCanvas.SetActive(false);
        }
        
    }

     IEnumerator BossSpell()
    {
        animator.SetBool("isSpell", true);
        animator.Play("SpellCasting");
        spellCountBool = false;
        spellTransform.position = playerTransform.position;
        foreach (var particle in spellParticle)
        {
            particle.Emit(4);
        }
        yield return new WaitForSeconds(1f);
        spellCollider.enabled = true;
        
        yield return new WaitForSeconds(1f);
        spellAudioSource.Play();
        spellCollider.enabled = false;
        spellCountBool = true;
    }

    IEnumerator BossSpellReset()
    {
        spellReset = false;
        yield return new WaitForSeconds(10f);
        spellCount = spellMaxCount;
        animator.SetBool("isSpell", false);
        isUsingSpell = false;
        spellReset = true;
        
    }

    private void BossIsDead()
    {
        if (bossIsDead)
        {
            enemyHPCanvas.SetActive(false);
            textMeshProUGUI.text = "Boss is Dead";
            temp.SetActive(true);
            Cursor.visible = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }


    public void BackToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
