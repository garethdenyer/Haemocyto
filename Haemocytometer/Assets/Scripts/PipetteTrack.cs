using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteTrack : MonoBehaviour
{
    //attached to Track prefab

    private void OnMouseDown()
    {
        GameObject tip = GameObject.FindGameObjectWithTag("Tip");

        //if the tip is not empty do something with it
        if (tip.GetComponent<TipChars>().volintip > 0f)
        {
            //move the tip to the track well
            tip.transform.position = transform.position;

            //transfer the volume and protein concs from the tip to the track
            this.GetComponent<TrackChars>().volloaded = tip.GetComponent<TipChars>().volintip;                       
            for (int p = 0; p < tip.GetComponent<TipChars>().proteinConcs.Count; p++)
            {
                this.GetComponent<TrackChars>().proteinConcs.Add(tip.GetComponent<TipChars>().proteinConcs[p]);
            }
            this.GetComponent<TrackChars>().UpdateLabel();

            //reset the tip
            tip.GetComponent<TipChars>().volintip = 0f;
            tip.GetComponent<Renderer>().material.color = Color.white;
        }


    }
}
