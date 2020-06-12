using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{

    public float distance;
    // Объект, служащий целью для астероидов
    public Transform target; 
    
    //ШТУРМОВИК
    public GameObject assaultShipPrefab;  
    // Радиус сферы, на поверхности которой создаются астероиды
    public float assaultShipRadius = 300.0f;
    //Время ожидани следующего спавна
    public float assaultSpawnRate = 5.0f;
    public float variance = 1.0f;  
    //количество необходимых и текущих кораблей
    public int allAssaultCount = 0;
    public int currentAllAssaultCount;
    // Значение false запрещает создавать астероиды
    public bool spawnAssaultShip = false;

    //ТАРАН
    public GameObject ramShipPrefab;
    // Радиус сферы, на поверхности которой создаются астероиды
    public float ramShipRadius = 250.0f;
    //Время ожидани следующего спавна
    public float ramSpawnRate = 5.0f;  
    //количество необходимых и текущих кораблей
    public int allRamCount = 0;
    public int currentAllRamCount;
    // Значение false запрещает создавать астероиды
    public bool spawnRamShip = false;







    void Start()
    {
        StartCoroutine(CreateRamShips());
        StartCoroutine(CreateAssaultShips());
        
    }
    IEnumerator CreateAssaultShips()
    {
        // Бесконечный цикл
        while (true)
        {
            if (currentAllAssaultCount >= allAssaultCount)
            {
                spawnAssaultShip = false;
            }       
                // Определить место появления следующего астероида
                float nextSpawnTime
                = assaultSpawnRate + Random.Range(-variance, variance);
                // Ждать в течение заданного интервала времени
                yield return new WaitForSeconds(nextSpawnTime);
                // Также дождаться, пока обновится физическая подсистема
                yield return new WaitForFixedUpdate();
                // Создать астероид
                CreateNewAssaultShip(spawnAssaultShip, ref currentAllAssaultCount, assaultShipRadius, assaultShipPrefab);

        }
    }

    IEnumerator CreateRamShips()
    {
        // Бесконечный цикл
        while (true)
        {
            if (currentAllRamCount >= allRamCount)
            {
                spawnRamShip = false;
            }
            // Определить место появления следующего астероида
            float nextSpawnTime
            = ramSpawnRate + Random.Range(-variance, variance);
            // Ждать в течение заданного интервала времени
            yield return new WaitForSeconds(nextSpawnTime);
            // Также дождаться, пока обновится физическая подсистема
            yield return new WaitForFixedUpdate();
            // Создать астероид
            CreateNewAssaultShip(spawnRamShip, ref currentAllRamCount, ramShipRadius, ramShipPrefab);

        }
    }




    void CreateNewAssaultShip(bool spawnShip, ref int currentShipsCount, float shipRadius, GameObject shipPrefab)
    {
        if (spawnShip == false)
        {
            return;
        }
        else
        {
            currentShipsCount += 1;
        }
        Vector3 assaultPosition;

        do
        {
            assaultPosition = Random.onUnitSphere * shipRadius;
            assaultPosition.y = 0;
            Debug.Log(distance);
            Debug.Log(assaultPosition);
        }
        while ((Mathf.Abs(assaultPosition.x) < 200) & (Mathf.Abs(assaultPosition.z) < 200));

        Debug.Log("secces distance");
        // Выбрать случайную точку на поверхности сферы

        // Масштабировать в соответствии с объектом
        assaultPosition.Scale(transform.lossyScale);
        // И добавить смещение объекта, порождающего астероиды
        //assaultPosition += transform.position;
        // Создать новый астероид
        var newAssaultShip = Instantiate(shipPrefab);
        // Поместить его в только что вычисленную точку
        // newAsteroid.transform.position = asteroidPosition;
        newAssaultShip.GetComponent<Rigidbody>().transform.position = assaultPosition;
        // Направить на цель
        // newAsteroid.transform.LookAt(target);
        newAssaultShip.GetComponent<Rigidbody>().transform.LookAt(target);

        if (newAssaultShip.CompareTag("AssaultShip"))
        {
            newAssaultShip.GetComponent<AssaultShip>().target = target;
        }
        
        //newAssaultShip.target = target;
    }
    
    void OnDrawGizmosSelected()
    {
        // Установить желтый цвет
        Gizmos.color = Color.yellow;
        // Сообщить визуализатору Gizmos, что тот должен использовать
        // текущие позицию и масштаб
        Gizmos.matrix = transform.localToWorldMatrix;
        // Нарисовать сферу, представляющую собой область создания астероидов
        Gizmos.DrawWireSphere(Vector2.zero, assaultShipRadius);
    }
    public void DestroyAllEnemies()
    {
        // Удалить все имеющиеся в игре астероиды
        foreach (var assaultShip in
        FindObjectsOfType<AssaultShip>())
        {
            Destroy(assaultShip.gameObject);
        }
        foreach (var assaultShip in
        FindObjectsOfType<Asteroid>())
        {
            Destroy(assaultShip.gameObject);
        }

        distance = 0;
        //currentAllAsteroids = 0;
    }

    public static Vector2 RandomPointOnUnitCircle(float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        return new Vector2(x, y);

    }

    public void SwitchAllSpawnerProcesses(bool inditator)
    {
        spawnAssaultShip = inditator;
        spawnRamShip = inditator;
    }

}
