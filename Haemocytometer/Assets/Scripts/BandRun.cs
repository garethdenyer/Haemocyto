using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BandRun : MonoBehaviour
{
    // attached to Empty HOlder


    public GameObject bandPrefab;
    public GameObject gelPrefab;
    public GameObject TMPprefab;

    public List<float> standards = new List<float>();

    ProteinList listofpros;
    MakeProteinMix sampleMaker;

    private void Start()
    {
        standards.AddRange(new List<float>() { 250f, 150f, 100f,75f, 50f, 37f, 25f, 20f, 15f, 10f });
        
        listofpros = this.GetComponent<ProteinList>();
        sampleMaker = this.GetComponent<MakeProteinMix>();
       
        listofpros.MakeDatabase();
    }

    public float CalcMigration(float molwt)
    {
        float distance = 336f * Mathf.Pow(molwt, -0.519f); //returns distance in mm from well
        return distance;
    }
    

    public void CreateAndPosition()

    {
        float gelheight = gelPrefab.transform.localScale.y;

        GameObject gel = Instantiate(gelPrefab, new Vector3(0f, -gelheight/2f, -0.1f), gelPrefab.transform.rotation);
        gel.transform.name = "Gel";

        float dyefront = gel.transform.localScale.y;
        float gelwidth = gel.transform.localScale.x;
        
        for(int s=0; s < standards.Count; s++)  //loops through each standard, making a band in the leftmost track
        {
            float migration = CalcMigration(standards[s]);
            GameObject standardband = Instantiate(bandPrefab, new Vector3(0f, 0f, 0f), bandPrefab.transform.rotation);
            standardband.transform.position = new Vector3(-4f, -migration/dyefront, -0.6f);
            standardband.transform.name = standards[s].ToString() + " kDa";

            
            GameObject standardlabel = Instantiate(TMPprefab, new Vector3(0f, 0f, 0f), TMPprefab.transform.rotation);
            standardlabel.transform.position = new Vector3(-5.5f, -migration/dyefront, -0.6f);
            standardlabel.GetComponent<TMP_Text>().text = standards[s].ToString();
            standardlabel.GetComponent<TMP_Text>().fontSize = 4;
            standardlabel.transform.name = "label";
        }

        for(int k =0; k<sampleMaker.samples.Count; k++)  //creates a track for each sample
        {
            GameObject lanelabel = Instantiate(TMPprefab, new Vector3(0f, 0f, 0f), TMPprefab.transform.rotation);
            lanelabel.transform.position = new Vector3((k-2f)/1.5f, 0.6f, -0.6f);
            lanelabel.GetComponent<TMP_Text>().text = (k+1).ToString();
            lanelabel.GetComponent<TMP_Text>().fontSize = 4;
            lanelabel.transform.name = "Lane" + (k+1).ToString();

            for (int t = 0; t < listofpros.MolWts.Count; t++)  //within a track, make a band for each of the proteins
            {
                float waytogo = CalcMigration(listofpros.MolWts[t]);
                GameObject mainproteinband = Instantiate(bandPrefab, new Vector3(0f, 0f, 0f), bandPrefab.transform.rotation);

                mainproteinband.GetComponent<BandChars>().affinityAb01 = listofpros.Affinities01[t];
                mainproteinband.GetComponent<BandChars>().Mw = listofpros.MolWts[t];
                mainproteinband.GetComponent<BandChars>().amount = sampleMaker.samples[k].GetComponent<SampleChars>().proteinConcs[t];


                mainproteinband.transform.position = new Vector3((k - 2f) / 1.5f, -waytogo / dyefront, -0.6f);

                float height = sampleMaker.samples[k].GetComponent<SampleChars>().proteinConcs[t]; //vary the height of the bans according to concn
                mainproteinband.transform.localScale = new Vector3(0.9f, height, 1f);

                float transparency = sampleMaker.samples[k].GetComponent<SampleChars>().proteinConcs[t]; //vary the transparency of the bans according to concn
                mainproteinband.GetComponentInChildren<Renderer>().material.color = new Color (1f, 1f, 1f, transparency);


                mainproteinband.transform.name = listofpros.Names[t];

            }

        }
    }


    public void Blotty()
    {
        GameObject[] allbands = GameObject.FindGameObjectsWithTag("Protein");
        foreach (GameObject band in allbands)
        {
            float intenstity = band.GetComponent<BandChars>().amount * band.GetComponent<BandChars>().affinityAb01;

            if (intenstity > 0.1)
            {
            band.GetComponentInChildren<Renderer>().material.color = Color.red;
            }

        }
    }
}
