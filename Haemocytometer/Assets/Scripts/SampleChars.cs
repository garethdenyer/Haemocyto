using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SampleChars : MonoBehaviour
{

    // attached to sample prefab

    public float incubTime;
    public float drugConc;
    public float voluL;

    public List<float> proteinConcs = new List<float>() ;

    public TMP_Text label;

    private void Awake()
    {
        proteinConcs.AddRange(new List<float>() { 0.5f, 1f, 0.8f, 0.32f, 0.7f, 0.91f, 0.19f, 0.1f });
    }

    public void IncubationResults()
    {
        proteinConcs[0] -= (incubTime * drugConc) / 1000f;
        proteinConcs[7] += (incubTime * drugConc) / 1000f;
    }

    public void UpdateLabel()
    {
        label.text = voluL.ToString();
    }


}




