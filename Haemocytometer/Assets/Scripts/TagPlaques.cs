using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPlaques : MonoBehaviour
{
   
    //attached to cylinder part of plaque (not the top level)

    public GameObject toplevelplaque;

    private void OnMouseDown()
    {
        if (toplevelplaque.GetComponent<PlaqueChars>().selection == "selected")
        {
            toplevelplaque.GetComponent<PlaqueChars>().selection = "";
            this.GetComponent<Renderer>().material.color = Color.white;
            this.transform.parent.parent.GetComponentInChildren<PlaqueCount>().count -= 1;
        }
        else
        {
            toplevelplaque.GetComponent<PlaqueChars>().selection = "selected";
            this.GetComponent<Renderer>().material.color = Color.black;
            this.transform.parent.parent.GetComponentInChildren<PlaqueCount>().count += 1;
        }

            this.transform.parent.parent.GetComponentInChildren<PlaqueCount>().updatedials();
    }
}

