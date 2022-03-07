using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackChars : MonoBehaviour
{
    //attached to Track prefab

    public float volloaded;
    public List<float> proteinConcs = new List<float>();

    public TMP_Text label;



    public void UpdateLabel()
    {
        label.text = volloaded.ToString();
    }
}
