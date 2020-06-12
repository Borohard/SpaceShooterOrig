// Перемещает объект вперед с постоянной скоростью и уничтожает
// его спустя заданный промежуток времени.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shot : MonoBehaviour
{
    // Скорость движения снаряда
    public float speed = 100.0f;
    // Время в секундах, через которое следует уничтожить снаряд
    public float life = 5.0f;
    void Start()
    {
        Invoke("RadiusUpScale", 0.2f);
        // Уничтожить через 'life' секунд
        Destroy(gameObject, life);

    }
    void Update()
    {
        
        // Перемещать вперед с постоянной скоростью
        transform.Translate(
        Vector3.forward * speed * Time.deltaTime);
    }

    void RadiusUpScale()
    {
        GetComponent<SphereCollider>().radius = 0.5f;
    }
}