using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class cameraScript : MonoBehaviour
{
    private static cameraScript instance;

    void Awake()
    {
       if (instance != null && instance != this)
       {
           Destroy(gameObject);   //destroy any duplicate cameras in the start screen
       }
     
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);   //to not delete camera with each scene change(toggling on and off ther UI is done elsewhere)
        }

        
    }
}