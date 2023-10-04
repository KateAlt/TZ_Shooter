using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    //-- UI
    public UIController uiController;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - firePoint.position;

            bullet.transform.rotation = Quaternion.LookRotation(direction);

            float pitch = Mathf.Clamp(direction.y, -180, 180);
            float yaw = Mathf.Clamp(direction.x, -180, 180);

            transform.rotation = Quaternion.LookRotation(direction);

            rb.AddForce(direction.normalized * bulletForce, ForceMode.Impulse);

            uiController.UpdateEnemyHealthData();
        }
    }
}






// void Shoot()
//     {
//         // Створюємо об'єкт пулі з префабу
//         GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

//         // Отримуємо компонент Rigidbody об'єкта пулі
//         Rigidbody rb = bullet.GetComponent<Rigidbody>();

//         // Застосовуємо силу до пулі, щоб вона летіла
//         rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
//     }