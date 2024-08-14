using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    private Animator animator;
    private Vector2 userInput;
    [SerializeField]
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");
        //print($"User Input: {userInput}");

        this.transform.Translate(Vector3.forward * userInput.y * moveSpeed);
        this.transform.Translate(Vector3.right * userInput.x * moveSpeed);
        

        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);
    }
}
