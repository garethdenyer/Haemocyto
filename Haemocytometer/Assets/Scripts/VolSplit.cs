using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolSplit : MonoBehaviour
{

   public InputField VolEnterField;

    string volumeAndUnits;

    public void SplitTheField()
    {
        volumeAndUnits = VolEnterField.GetComponent<InputField>().text;
        
        string[] splitBits = volumeAndUnits.Split(' ');
        int vol = int.Parse(splitBits[0]);
        string units = splitBits[1];

        Debug.Log("orignal " + volumeAndUnits + '\n' + "Split to " + vol.ToString() + " and " + units);
    }



}
