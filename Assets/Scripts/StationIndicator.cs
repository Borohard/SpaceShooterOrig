using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationIndicator : MonoBehaviour
{
    public DamageTaking spaceStation;

    //public Image healthbar;

    public float fillbar = 1.0f;

    public float basicHealth;

    public float currentHelth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHelth = spaceStation.hitPoints;
        fillbar = currentHelth / basicHealth;

        GetComponent<Image>().fillAmount = fillbar;
    }
}
