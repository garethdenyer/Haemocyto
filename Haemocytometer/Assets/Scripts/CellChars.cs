using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChars : MonoBehaviour
{
    // Attach to Cell Prefab at top level

    public float cellDiam;
    public string health;
    public string quadrant;
    public string selection;

    public GameObject tickSprite;
    public GameObject crossSprite;

    public void SetQuadrantColour()
    {
        if (quadrant == "A" || quadrant == "B" || quadrant == "C" || quadrant == "D")
        {
            tickSprite.SetActive(true);
        }

        else
        {
            crossSprite.SetActive(true);
        }

    }


}
