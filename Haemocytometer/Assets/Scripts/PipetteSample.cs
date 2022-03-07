using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipetteSample : MonoBehaviour
{
    //attached to Sample prefab




    private void OnMouseDown()
    {  
            GameObject tip = GameObject.FindGameObjectWithTag("Tip");

            if(tip.GetComponent<TipChars>().volintip ==0f)
            {     
                //move the tip 
                tip.transform.position = transform.position + new Vector3(0f, 1f, 0f);

            //set vol to 
                tip.GetComponent<TipChars>().volintip = tip.GetComponent<TipChars>().GetVolume();
                tip.GetComponent<Renderer>().material.color = Color.red;

                for (int p=0; p< this.GetComponent<SampleChars>().proteinConcs.Count; p++)
                {
                    tip.GetComponent<TipChars>().proteinConcs.Add(this.GetComponent<SampleChars>().proteinConcs[p]);
                }
            }
        
    }
}
