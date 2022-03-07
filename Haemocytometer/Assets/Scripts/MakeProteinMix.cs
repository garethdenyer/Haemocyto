using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeProteinMix : MonoBehaviour
{
    //attached to Empty Holder

    public GameObject samplePrefab;

    public List<GameObject> samples = new List<GameObject>();

    public void CreateSamples()  //makes a bunch of samples and sets their incubation conditions   standard set of proteins made 
    {
        for (int i=0; i<6; i++) 
        { 
            GameObject sample = Instantiate(samplePrefab, new Vector3(-7f+i, -6f, -0.5f), samplePrefab.transform.rotation);
            sample.transform.name = "s" + i.ToString();
            samples.Add(sample);

            sample.GetComponent<SampleChars>().incubTime = (i % 3f) * 60f;
            sample.GetComponent<SampleChars>().voluL = 30f;
            sample.GetComponent<SampleChars>().UpdateLabel();

            if (i < 3)
            {
                sample.GetComponent<SampleChars>().drugConc = 5f;
            }
        }

    }

    public void IncubateSamples()  //for each sample, run the incubation
    {
        for (int i = 0; i < samples.Count; i++)
        {
            samples[i].GetComponent<SampleChars>().IncubationResults();
        }
    }


}
