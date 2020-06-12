using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AssaultShip : MonoBehaviour
{
    //public EnemiesSpawner enemiesSpawner;

    public float speed = 10.0f;

    public Transform target;

    public bool isFire = true;
    // Список пушек для стрельбы
    public Transform[] firePoints;
    // Индекс в firePoints, указывающий на следующую
    // пушку
    private int firePointIndex;

    public float distance;
    public float fireRate = 0.2f;
    public GameObject shotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //target = enemiesSpawner.target;
        GetComponent<Rigidbody>().velocity
        = transform.forward * speed;
        // Создать красный индикатор для данного астероида
        var indicator = IndicatorManager.instance
        .AddIndicator(gameObject, Color.red);

        indicator.showDistanceTo = GameManager.instance.currentSpaceStation.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        StartFire();
    }

    private void StartFire()
    {

        if (distance <= 40 & isFire==true)
        {
            isFire = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(FireWeapons());
        }
    }

    IEnumerator FireWeapons()
    {

        Debug.Log("StartCorute");

        while (true)
        {

          
            Fire();
            yield return new WaitForSeconds(fireRate);

        }

    }


    // Вызывается диспетчером ввода InputManager.
    public void Fire()
    {
        // Если пушки отсутствуют, выйти
        if (firePoints.Length == 0)
            return;
        // Определить следующую пушку для выстрела
        var firePointToUse = firePoints[firePointIndex];
        // Создать новый снаряд с ориентацией,
        // соответствующей пушке
        Instantiate(shotPrefab,
        firePointToUse.position,
        firePointToUse.rotation);

        var audio = firePointToUse.GetComponent<AudioSource>();
        if (audio)
        {
            audio.Play();
        }



        // Перейти к следующей пушке
        firePointIndex++;
        // Если произошел выход за границы массива,

        // вернуться к его началу
        if (firePointIndex >= firePoints.Length)
            firePointIndex = 0;
    }

    private void OnDestroy()
    {
        GameManager.instance.UpDieEnemiesCount();
    }
}
