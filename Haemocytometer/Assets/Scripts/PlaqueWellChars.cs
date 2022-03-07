using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaqueWellChars : MonoBehaviour
{
    //attached to welldish


    public TMP_Text welllabel;

    public void SetLabel(string label)
    {
        welllabel.text = label;
    }


}
