using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        if(instance != null) //sahnede sadece bitane var olabilen global olarak eriþilebilen bir sýnýftýr :singleton buyüzden
            Destroy(instance.gameObject);  //birden fazla olmasý durumunda sonradan gelenniyok ediyoruz.
        else
            instance = this; // we assigned the instance we created. // eðer instance yoska ilk geleni asign ediyoruz.

    }

    


}
