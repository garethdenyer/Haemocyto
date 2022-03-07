using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTracks : MonoBehaviour
{

    //attached to Empty Holder

    public GameObject gelwellprefab;

    public void CreateWells()
    {
        for(int i=1; i<16; i++)
        {
            GameObject well = Instantiate(gelwellprefab, new Vector3(-10f + i, 0f, -1f), gelwellprefab.transform.rotation);
            well.transform.name = i.ToString();

            well.GetComponent<TrackChars>().volloaded = 0f;
            well.GetComponent<TrackChars>().UpdateLabel();
        }
    }
}
