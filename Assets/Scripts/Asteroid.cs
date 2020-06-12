using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Скорость перемещения астероида.
    public float speed = 10.0f;
    void Start()
    {
        // установить скорость перемещения твердого тела
        GetComponent<Rigidbody>().velocity
        = transform.forward * speed;
        // Создать красный индикатор для данного астероида
        var indicator = IndicatorManager.instance
        .AddIndicator(gameObject, Color.Lerp(Color.red,Color.blue, 0.3f));

        indicator.showDistanceTo = GameManager.instance.currentSpaceStation.transform;
    }

    private void OnDestroy()
    {
        GameManager.instance.UpDieEnemiesCount();
    }
}