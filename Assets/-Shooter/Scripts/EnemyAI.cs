using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public BaseData baseData;
    private Animator animator;
    public Transform target;
    public float speed;
    private float moveSpeed;
    public float rotationSpeed = 100f;
    public float distanceToPlayer;

    //-- UI
    public UIController uiController;

    void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = speed;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < distanceToPlayer)
        {
            Debug.Log("Attack!");
            moveSpeed = 0f;
            animator.SetBool("IsAttacking", true);
            baseData.healthPlayer = baseData.healthPlayer - 0.1f;
            uiController.UpdateHealthData();
        }
        else
        {
            moveSpeed = speed;
            animator.SetBool("IsAttacking", false);
        }
    }
}
