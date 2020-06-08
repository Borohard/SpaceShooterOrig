using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : MonoBehaviour
{
    // Спрайт для использования в качестве прицельной сетки.
    public Sprite targetImage;
    void Start()
    {
        // Зарегистрировать новый индикатор, соответствующий
        // данному объекту, использовать желтый цвет и
        // нестандартный спрайт.
        IndicatorManager.instance.AddIndicator(gameObject,
        Color.yellow, targetImage);
    }
}
