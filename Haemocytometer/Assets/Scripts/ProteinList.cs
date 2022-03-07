using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinList : MonoBehaviour
{
    //Attached to Empty HOlder

    public  List<float> MolWts = new List<float>();
    public  List<string> Names = new List<string>();
    public  List<float> Affinities01 = new List<float>();

    public void MakeDatabase()
    {
        MolWts.AddRange(new List<float>() { 220f, 140f, 45f, 32f, 30f, 17f, 13f, 55f });
        Names.AddRange(new List<string>() { "albom", "betazelf", "catenocin", "drigva", "elfinov", "feunducin", "grinpolfin", "X3502" });
        Affinities01.AddRange(new List<float>() { 0.5f, 0.1f, 0.05f, 0f, 0f, 0f, 0f, 1f });
    }

}
