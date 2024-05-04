using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        if(instance != null) //sahnede sadece bitane var olabilen global olarak eri�ilebilen bir s�n�ft�r :singleton buy�zden
            Destroy(instance.gameObject);  //birden fazla olmas� durumunda sonradan gelenniyok ediyoruz.
        else
            instance = this; // we assigned the instance we created. // e�er instance yoska ilk geleni asign ediyoruz.

    }

    


}
