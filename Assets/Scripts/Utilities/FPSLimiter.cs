using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    private void Start()
    {
        // Establece el límite de FPS a 120 hz
        Application.targetFrameRate = 120;
    }
    
}
