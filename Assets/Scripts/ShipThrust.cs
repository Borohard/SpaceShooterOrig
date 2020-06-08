using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour
{
   
    public  float speed = 15.0f;

    private float basicSpeed;

    

    // Update is called once per frame
    void Update()
    {
        var offset = Vector3.forward * Time.deltaTime * speed;
        this.transform.Translate(offset);
    }


    public void Awake()
    {
        InputManager.instance.SetSpeed(this);
    }

    public void OnDestroy()
    {
        if (Application.isPlaying == true)
        {
            InputManager.instance.RemoveSpeed(this);
        }
    }

    public void Speeding()
    {
        basicSpeed = speed;
        while(speed <= 20.0f)
        {
            speed +=0.01f;
        }            
    }

    public void Unspeeding()
    {
        
        while (speed >= basicSpeed)
        {
            speed -= 0.01f;
        }
    }

}
