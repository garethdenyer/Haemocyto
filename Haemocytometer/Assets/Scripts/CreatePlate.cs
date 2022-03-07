using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreatePlate : MonoBehaviour
{
    //attached to Empty Holder

    public int Rows = 8;
    public int Columns = 12;

    public GameObject wellprefab;
    public GameObject emptyGO;
    public GameObject TMPprefab;

    int wellno;

    string rowletters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=!@#$%^&*()_+";

    public GameObject[] wellstodestroy;

    public List<GameObject> welllist = new List<GameObject>();
    public List<GameObject> columnheadings = new List<GameObject>();
    public List<GameObject> rowheadings = new List<GameObject>();
    public List<float>dflist = new List<float>();


    public void MakeItSo()
    {
            //DestroyGrid();
            CreateGrid();
    }



    void CreateGrid()
    {

        float size = wellprefab.transform.localScale.x * 2f; //not sure why suddently we need the x2 but overlapping if not
        float dilfac = 1f;

        Debug.Log(size.ToString());

        wellno = 0;

        //make base 
        GameObject EmptyTop = Instantiate(emptyGO, new Vector3(0f, 0f, 0f), Quaternion.identity);
        EmptyTop.GetComponent<BoxCollider>().size = new Vector3(Columns * size, size, size);
        EmptyTop.GetComponent<BoxCollider>().center = new Vector3(((Columns * size / 2) - size / 2), size / 2, Rows * -size);
        EmptyTop.transform.name = "Plate";

        EmptyTop.GetComponent<PlateChars>().masterStrength = Random.Range(0f, 500f);

        //The column headings and dilutions
        for (int k = 0; k < Columns; k++)
        {

            GameObject colheading = Instantiate(TMPprefab, new Vector3(k * size, 0, size), TMPprefab.transform.rotation);
            colheading.transform.tag = "Label";
            colheading.transform.name = "CH" + (k + 1).ToString();
            colheading.transform.rotation= Quaternion.Euler(90f,0f,0f);
            colheading.transform.SetParent(EmptyTop.transform,false);
            colheading.GetComponent<TMP_Text>().text = (k + 1).ToString();
            colheading.GetComponent<TMP_Text>().fontSize = 9;
            columnheadings.Add(colheading);

            //make the master dilution factors

            dilfac *= 2f;
            dflist.Add(dilfac);
            GameObject dilfacheading = Instantiate(TMPprefab, new Vector3(k * size, 0f, size *2f), TMPprefab.transform.rotation);
            dilfacheading.GetComponent<TMP_Text>().text = "1/"  + dilfac.ToString();
            dilfacheading.transform.name = "DF" + k.ToString();
            dilfacheading.transform.rotation = Quaternion.Euler(90f, 0f, 45f);
            dilfacheading.transform.SetParent(EmptyTop.transform, false);
            dilfacheading.GetComponent<TMP_Text>().fontSize = 9;

        }

        //the row headings
        for (int l = 0; l < Rows; l++)
        {

            char rl = rowletters[l];

            GameObject rowheading = Instantiate(TMPprefab, new Vector3(-size, 0, -l * size), TMPprefab.transform.rotation);
            rowheading.transform.tag = "Label";
            rowheading.GetComponent<TMP_Text>().text = rl.ToString();
            rowheading.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            rowheading.transform.name = "RH" + rl.ToString();
            rowheading.GetComponent<TMP_Text>().fontSize = 9;
            rowheading.transform.SetParent(EmptyTop.transform, false);
            rowheadings.Add(rowheading);
        }

        //the wells
        for (int i = 0; i < Rows; i++)
        {

            char ch = rowletters[i];

            for (int j = 0; j < Columns; j++)
            {
                wellno += 1;

                GameObject well = Instantiate(wellprefab, new Vector3(j * size, 0, -i * size), Quaternion.identity);
                well.transform.parent = EmptyTop.transform;
                well.transform.tag = "Well";
                well.transform.name = ch + (j + 1).ToString();

                float randomerror = Random.Range(-30f, 30f);

                well.GetComponent<WellChars>().dilfac = dflist[j] + (dflist[j]*(randomerror/100));

                welllist.Add(well);
            }
        }
    }


    public void DestroyGrid()
    {
        welllist = new List<GameObject>();  //clears the list
        columnheadings = new List<GameObject>();
        rowheadings = new List<GameObject>();

        wellstodestroy = GameObject.FindGameObjectsWithTag("Well");
        foreach (GameObject welltokill in wellstodestroy)
        {
            Destroy(welltokill);
        }
    }


    public void UpdateWellChars()
    {
        for(int i=0; i< welllist.Count; i++)
        {
            welllist[i].GetComponent<WellChars>().SetAppearances();
        }
    }


}
