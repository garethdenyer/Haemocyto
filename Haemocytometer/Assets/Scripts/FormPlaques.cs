using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormPlaques : MonoBehaviour
{
    //On Empty holder

    public GameObject plaquePrefab;
    public GameObject welldishPrefab;
    public GameObject TMPprefab;

    public InputField DF;
    public InputField volToUse;

    public GameObject setupPanel;
    public GameObject NewPrepButton;
    public GameObject ResultsPanel;

    public int voluL;
    public int dilFac;

    public int plaqueCount;

    string charlist = "ABCD";

    public List<int> dillist = new List<int>();
    public List<int> PFUs = new List<int>();
    public List<int> plaqueNos = new List<int>();
    public List<GameObject> plaqueHits = new List<GameObject>();
    public List<GameObject> dishes = new List<GameObject>();

    GameObject[] plaquestokill;
    GameObject[] labelstokill;

    private void Start()
    {
        voluL = 100;
        dilFac = 100;
    }

    public void DoEverything()
    {
        DestroyEverything();
        CollectParameters();
        CreateTheWellData();
        CreateTheWells();
        MakeThePlaques();
    }

    public void DestroyEverything()
    {
        foreach (GameObject dishtokill in dishes)
        {
            Destroy(dishtokill);
        }

        plaquestokill = GameObject.FindGameObjectsWithTag("Plaque");
        foreach (GameObject plaque in plaquestokill)
        {
            Destroy(plaque);
        }

        labelstokill = GameObject.FindGameObjectsWithTag("Dillabel");
        foreach (GameObject lab in labelstokill)
        {
            Destroy(lab);
        }


        dishes = new List<GameObject>();
        dillist = new List<int>();

        plaqueNos = new List<int>();
        plaqueHits = new List<GameObject>();

        this.GetComponent<PlaqueReport>().ClearEntries();
}

    public void DoItAllAgain()
    {
        setupPanel.SetActive(false);
        NewPrepButton.SetActive(true);
        ResultsPanel.SetActive(false);
        PFUs = new List<int>();
        DestroyEverything();
    }

    public void ShowResultsPanel()
    {
        ResultsPanel.SetActive(true);
    }

    public void NewPFUs()
    {
        int anchorPFU = Random.Range(20000, 2000000);
        PFUs.Add((int)(((float)(anchorPFU) * Random.Range(0.95f, 1.05f))));
        PFUs.Add((int)(((float)(anchorPFU) * Random.Range(0.95f, 1.05f))));

        NewPrepButton.SetActive(false);
        setupPanel.SetActive(true);
    }


    public void CollectParameters()
    {
        
        if(volToUse.text != "")
        {
        voluL = int.Parse(volToUse.text);
        }
        
        if(DF.text != "")
        {
        dilFac = int.Parse(DF.text);
        }

    }

    public void CreateTheWellData()
    {
        int n = 0;

        for (int k = 0; k < PFUs.Count; k++)
        {
            int thisPFU = PFUs[k];

            for (int j = 0; j < 3; j++)
            {

                int thisDF = dilFac * (int)(Mathf.Pow(10, j));  //every j, the difFac increases by 10
                dillist.Add(thisDF);

                int NoPlaques = (int)(thisPFU * (voluL/1000f)) / thisDF;
                plaqueNos.Add(NoPlaques);
            }
        }

        n += 1;
    }


    public void CreateTheWells()
    {
        //make DF labels across top
        for(int p = 0; p < 3; p++)
        {
            GameObject dilfaclabel = Instantiate(TMPprefab, new Vector3(-60f + (70f * p), 75f, 0f), TMPprefab.transform.rotation);
            dilfaclabel.transform.tag = "Dillabel";
            dilfaclabel.GetComponent<TMP_Text>().text = dillist[p].ToString(); 
        }



        //nested loop to make wells
        for (int r = 0; r < 2; r++)
        {
            char letter;
            letter = charlist[r];

            for (int z = 0; z < 3; z++)
            {
                GameObject dish = Instantiate(welldishPrefab, new Vector3(-60f + (70f * z), 30f - (70 * r), 0f), welldishPrefab.transform.rotation);
                dish.transform.tag = "Dish";
                dish.GetComponentInChildren<PlaqueCount>().count = 0;
                dish.GetComponentInChildren<PlaqueCount>().updatedials();
                dish.transform.name = letter.ToString() + "-" + (z + 1).ToString();

                dish.GetComponent<PlaqueWellChars>().SetLabel(letter.ToString() + (z+1).ToString());

                dishes.Add(dish);
            }
        }
    }

    public void MakeThePlaques()
    {
        for (int s = 0; s < dishes.Count; s++)
        {

            plaqueHits = new List<GameObject>();

            Vector3 dishcentre = dishes[s].transform.position;

            if (plaqueNos[s] > 600)
            {
                plaqueCount = 600;
            }
            else
            {
                plaqueCount = plaqueNos[s];
            }

            for (int i = 0; i < plaqueCount; i++)
            {
                Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), -1.5f);

                float radius = Mathf.Sqrt(((position.x * position.x) + (position.y * position.y)));

                if (radius < 30f)
                {
                    GameObject plaque = Instantiate(plaquePrefab, new Vector3(0f, 0f, 0f), plaquePrefab.transform.rotation);
                    plaque.transform.tag = "Plaque";
                    plaque.transform.name = "Plaque" + i.ToString();
                    plaque.transform.parent = dishes[s].transform;                    
                    plaque.transform.localPosition = position;

                    float size = Random.Range(0.3f, 1.2f);
                    plaque.transform.localScale = new Vector3(size, size, 1f);

                    plaqueHits.Add(plaque);
                }
            }

            dishes[s].GetComponent<PlaqueCount>().realcounts.text = plaqueHits.Count.ToString();

        }
    }
}
