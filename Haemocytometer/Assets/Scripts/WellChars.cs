using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellChars : MonoBehaviour
{
    // Attachedwell unit

    public GameObject mainSolnCylinder;
    public GameObject pelletCylinder;

    public Material clearSoln;
    public Material redSoln;

    public float dilfac;
    public float strength;

    public void SetAppearances()
    {
        strength = this.transform.parent.gameObject.GetComponent<PlateChars>().masterStrength / dilfac;

        if(strength > 1f)
        {
            pelletCylinder.GetComponent<Renderer>().enabled = false;
            mainSolnCylinder.GetComponent<Renderer>().material = redSoln;
        }

        else
        {
            pelletCylinder.GetComponent<Renderer>().enabled = true;
            pelletCylinder.transform.localPosition = new Vector3(0f, 0.37f, 0f);
            mainSolnCylinder.GetComponent<Renderer>().material = clearSoln;
        }
    }
    
}
