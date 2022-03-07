using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaqueCount : MonoBehaviour
{
    
    //on top level of the well that holds the plaques
    
    public int count;
    public TMP_Text display;
    public TMP_Text realcounts;



    public void updatedials()
    {
        display.text = count.ToString();
    }



}
