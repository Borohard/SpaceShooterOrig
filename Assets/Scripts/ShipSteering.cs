using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSteering : MonoBehaviour
{

    public Button inversionButton;
    // Скорость поворота корабля
    public float turnRate = 6.0f;
    // Сила выравнивания корабля
    public float levelDamping = 1.0f;

    public int inversionIndicator = -1;
    void Update()
    {
        // Создать новый поворот, умножив вектор направления джойстика
        // на turnRate, и ограничить величиной 90 % от половины круга.
        // Сначала получить ввод пользователя.
        var steeringInput
        = InputManager.instance.steering.delta;
        // Теперь создать вектор для вычисления поворота.
        var rotation = new Vector2();
        rotation.y = steeringInput.x;
        rotation.x = steeringInput.y*inversionIndicator;
        // Умножить на turnRate, чтобы получить величину поворота.
        rotation *= turnRate;
        // Преобразовать в радианы, умножив на 90 %
        // половины круга
        rotation.x = Mathf.Clamp(
        rotation.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);
        // И преобразовать радианы в кватернион поворота!
        var newOrientation = Quaternion.Euler(rotation);
        // Объединить поворот с текущей ориентацией
        transform.rotation *= newOrientation;
        // Далее попытаться минимизировать поворот!

        // Сначала определить, какой была бы ориентация
        // в отсутствие вращения относительно оси Z
        var levelAngles = transform.eulerAngles;
        levelAngles.z = 0.0f;
        var levelOrientation = Quaternion.Euler(levelAngles);
        // Объединить текущую ориентацию с небольшой величиной
        // этой ориентации "без вращения"; когда это происходит
        // на протяжении нескольких кадров, объект медленно
        // выравнивается над поверхностью
        transform.rotation = Quaternion.Slerp(
        transform.rotation, levelOrientation,
        levelDamping * Time.deltaTime);
    }

    public void SwitchInversion()
    {
        if(inversionIndicator == -1)
        {
            //inversionButton.interactable = false;
            inversionButton.image.color = Color.green;
            inversionIndicator = 1;
        }
        else
        {
            inversionButton.image.color = Color.white;
            //inversionButton.interactable = true;
            inversionIndicator = -1;
        }

    }

    /*private void OnDestroy()
    {
        inversionButton.image.color = Color.white;
    }*/
}
