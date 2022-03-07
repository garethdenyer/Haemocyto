using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlaqueReport : MonoBehaviour
{

    // on EmptyHOlder

    public InputField A1userInput;
    public InputField A2userInput;
    public InputField A3userInput;
    public InputField B1userInput;
    public InputField B2userInput;
    public InputField B3userInput;


    public void CollectUserCounts()
    {
        GameObject[] countdisplays = GameObject.FindGameObjectsWithTag("plaquereport");
        foreach (GameObject display in countdisplays) 
        
            display.GetComponent<Renderer>().enabled=true;
        
    }

    public void ClearEntries()
    {
        A1userInput.text = "";
        A2userInput.text = "";
        A3userInput.text = "";
        B1userInput.text = "";
        B2userInput.text = "";
        B3userInput.text = "";
    }

}
