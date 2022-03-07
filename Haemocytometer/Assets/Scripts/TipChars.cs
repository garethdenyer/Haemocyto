using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipChars : MonoBehaviour
{
    //attached to Tip GameObject

    public float volintip;
    public List<float> proteinConcs = new List<float>();

    public InputField volume;

    public float GetVolume()
    {
        //get the volume from the input field
        float vol;
        
        vol = float.Parse(volume.text);

        return vol;
    }
}
