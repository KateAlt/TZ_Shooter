using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5.0f;
    private float startTime;
    public BaseData baseData;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider otherCollider = collision.collider;

        if (otherCollider.CompareTag("Enemy"))
        {
            baseData.healthEnemy = baseData.healthEnemy - 5f;
        }
    }
}
