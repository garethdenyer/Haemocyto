using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffects : MonoBehaviour
{
    public GameObject PostProcessThingy;

    public float brightness;

    private void Update()
    {
        PostProcessThingy.GetComponent<Vignette>().intensity.value = brightness;    
    }

}
