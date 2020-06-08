using UnityEngine;
using System.Collections;


public class ShipWeapons : MonoBehaviour
{
    // Шаблон для создания снарядов
    public GameObject shotPrefab;

    public void Awake()
    {
        InputManager.instance.SetWeapons(this);
    }

    public void OnDestroy()
    {
        if(Application.isPlaying == true)
        {
            InputManager.instance.RemoveWeapons(this);
        }
    }


    // Список пушек для стрельбы
    public Transform[] firePoints;
    // Индекс в firePoints, указывающий на следующую
    // пушку
    private int firePointIndex;
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
}