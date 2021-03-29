using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    //Used to turn SFX Manager into a singleton
    public static SFX_Manager Instance;

    //Stores SFX
    public List<AudioSource> SFX;


    void Awake()
    {
        //Turns SFX Manager into a singleton
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void PlaySFX(int soundEffect)
    {
        if(SFX[soundEffect] != null)
        {
            SFX[soundEffect].Play();
        }
    }
}
