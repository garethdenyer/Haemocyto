using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCells : MonoBehaviour
{
    //attached to sphere part of cell (not the top level)

    public GameObject toplevelcell;

    private void OnMouseDown()
    {


       if(toplevelcell.GetComponent<CellChars>().selection == "selected")
        {
            toplevelcell.GetComponent<CellChars>().selection = "";
            this.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            toplevelcell.GetComponent<CellChars>().selection = "selected";
            this.GetComponent<Renderer>().material.color = Color.yellow;
        }

    }
}
