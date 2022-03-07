using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpreadCells : MonoBehaviour
{
    //attached to EmptyHolder
    
    public GameObject cellPrefab;
    public GameObject dirtPrefab;

    public GameObject quadReportPrefab;
    public GameObject extraButtonPanel;
    public GameObject resultsPanel;
    public Material DeadCellMaterial;
    public Material[] dirtMaterials;

    //all the lists for recording quadrant contents
    public List<GameObject> cellList = new List<GameObject>();
    public List<GameObject> quadAlist = new List<GameObject>();
    public List<GameObject> quadBlist = new List<GameObject>();
    public List<GameObject> quadClist = new List<GameObject>();
    public List<GameObject> quadDlist = new List<GameObject>();
    public List<GameObject> quadOlist = new List<GameObject>();
    public List<GameObject> quadAdeads = new List<GameObject>();
    public List<GameObject> quadBdeads = new List<GameObject>();
    public List<GameObject> quadCdeads = new List<GameObject>();
    public List<GameObject> quadDdeads = new List<GameObject>();

    //constants for defining boundaries
    public float ACleft = -23.8f;
    public float ACright = -8.3f;
    public float BDleft = 8.7f;
    public float BDright = 24.3f;
    public float ABtop = 24.6f;
    public float ABbottom = 8.7f;
    public float CDtop = -8.39f;
    public float CDbottom = -24.2f;

    //the parameters of the cell suspension
    public float cellConc;
    public float celldiln;
    public float deathRate;
    public float cellSuspVol;
    public int numberOfCells;

    public string SessionID;
    string currtime;

    public InputField QuadAinput;
    public InputField QuadBinput;
    public InputField QuadCinput;
    public InputField QuadDinput;
    public InputField QuadADeadinput;
    public InputField QuadBDeadinput;
    public InputField QuadCDeadinput;
    public InputField QuadDDeadinput;
    public InputField CellCountinput;
    public Text summary;

    private void Start()
    {
        summary.text = "";
    }

    public void SetNewBasicParameters()
    {
       cellConc = Random.Range(1000f, 10000000f);
       deathRate = Random.Range(0, 20f);
       celldiln = 2;
       cellSuspVol = 1;
       
       extraButtonPanel.SetActive(true);
        resultsPanel.SetActive(true);
    }


    public void FirstForm()
    {
        currtime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");

        WWWForm form = new WWWForm();
        form.AddField("sessionPost", FirstLogin.SessionID);
        form.AddField("cellconcPost", cellConc.ToString());
        form.AddField("deadratePost", deathRate.ToString());
        form.AddField("timedatePost", currtime);
        form.AddField("usernamePost", FirstLogin.Username);
        form.AddField("dilfac_Post", celldiln.ToString());

        WWW pagetogoto = new WWW("https://labtroubleshooter.000webhostapp.com/AddHaemSession.php", form);
    }
  
    public void CreateAndPosition(string type)
    {
        DestroyCells();
        DestroyReports();
        ClearDirt();

        if(type == "new")
        {
        SetNewBasicParameters();
        }

        else if (type == "dilute")
        {
            celldiln *= 2;
        }

        else if (type == "concentrate")
        {
            cellConc *= 2;
            cellSuspVol /= 2f;
        }

        numberOfCells = (int)((cellConc / celldiln) / 10000f);

        for (int i = 0; i < numberOfCells; i++)
        {

            Vector3 position = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(0f, 0.5f));
            GameObject cell = Instantiate(cellPrefab, position, cellPrefab.transform.rotation);

            float cellscale = Random.Range(0.7f, 1.1f);
            cell.transform.localScale = new Vector3(cellscale, cellscale, cellscale);
            
            float lifefactor = Random.Range(0f, 100f);
            if(lifefactor < deathRate)
            {
                cell.GetComponent<CellChars>().health = "Dead";
                cell.GetComponentInChildren<Renderer>().material = DeadCellMaterial;
            }   
            
            if((cell.transform.position.x >ACleft && cell.transform.position.x <ACright) && (cell.transform.position.y > ABbottom && cell.transform.position.y < ABtop))
            {
                cell.GetComponent<CellChars>().quadrant = "A";
                if (cell.GetComponent<CellChars>().health == "Dead")
                {
                    quadAdeads.Add(cell);
                }
                else
                {
                    quadAlist.Add(cell);
                }                   
            }

            else if ((cell.transform.position.x >BDleft && cell.transform.position.x <BDright) && (cell.transform.position.y >ABbottom && cell.transform.position.y <ABtop))
            {
                cell.GetComponent<CellChars>().quadrant = "B";
                if (cell.GetComponent<CellChars>().health == "Dead")
                {
                    quadBdeads.Add(cell);
                }
                else
                {
                    quadBlist.Add(cell);
                }
            }

            else if ((cell.transform.position.x > ACleft && cell.transform.position.x < ACright) && (cell.transform.position.y > CDbottom && cell.transform.position.y < CDtop))
            {
                cell.GetComponent<CellChars>().quadrant = "C";
                if (cell.GetComponent<CellChars>().health == "Dead")
                {
                    quadCdeads.Add(cell);
                }
                else
                {
                    quadClist.Add(cell);
                }
            }

            else if ((cell.transform.position.x > BDleft && cell.transform.position.x < BDright) && (cell.transform.position.y > CDbottom && cell.transform.position.y < CDtop))
            {
                cell.GetComponent<CellChars>().quadrant = "D";
                if (cell.GetComponent<CellChars>().health == "Dead")
                {
                    quadDdeads.Add(cell);
                }
                else
                {
                    quadDlist.Add(cell);
                }
            }

            else
            {
                cell.GetComponent<CellChars>().quadrant = "Out";
                quadOlist.Add(cell);
            }

            cellList.Add(cell);
        }

        MakeDirt();

        FirstForm();

        summary.text = "Cells resupsended in " + cellSuspVol.ToString() + " mL. " + '\n' +  "Cell suspension diluted " + celldiln.ToString() + "x with trypan blue for assay.";
    }

    public void DestroyCells()
    {
        foreach (GameObject celltokill in cellList)
        {
            Destroy(celltokill);
        } 
        
        cellList = new List<GameObject>();  //clears the list
        quadAlist = new List<GameObject>();
        quadBlist = new List<GameObject>();
        quadClist = new List<GameObject>();
        quadDlist = new List<GameObject>();
        quadOlist = new List<GameObject>();
        quadAdeads = new List<GameObject>();
        quadBdeads = new List<GameObject>();
        quadCdeads = new List<GameObject>();
        quadDdeads = new List<GameObject>();

    }

    public void DestroyReports()
    {
        GameObject[] reports = GameObject.FindGameObjectsWithTag("quadreport");
        foreach (GameObject rep in reports)
            GameObject.Destroy(rep);

        QuadAinput.text = "";
        QuadBinput.text = "";
        QuadCinput.text = "";
        QuadDinput.text = "";
        QuadADeadinput.text = "";
        QuadBDeadinput.text = "";
        QuadCDeadinput.text = "";
        QuadDDeadinput.text = "";
        CellCountinput.text = "";


    }


    public void ShowTotals()
    {
        if (QuadAinput.text == "") { QuadAinput.text = "NA"; }
        if (QuadBinput.text == "") { QuadBinput.text = "NA"; }
        if (QuadCinput.text == "") { QuadCinput.text = "NA"; }
        if (QuadDinput.text == "") { QuadDinput.text = "NA"; }
        if (QuadADeadinput.text == "") { QuadADeadinput.text = "NA"; }
        if (QuadBDeadinput.text == "") { QuadBDeadinput.text = "NA"; }
        if (QuadCDeadinput.text == "") { QuadCDeadinput.text = "NA"; }
        if (QuadDDeadinput.text == "") { QuadDDeadinput.text = "NA"; }
        if (CellCountinput.text == "") { CellCountinput.text = "NA"; }



        int totalCount = cellList.Count;
        int quadAcount = quadAlist.Count;
        int quadBcount = quadBlist.Count;
        int quadCcount = quadClist.Count;
        int quadDcount = quadDlist.Count;
        int quadAdeadcount = quadAdeads.Count;
        int quadBdeadcount = quadBdeads.Count;
        int quadCdeadcount = quadCdeads.Count;
        int quadDdeadcount = quadDdeads.Count;

        float livePerMl = (((quadAcount + quadBcount + quadCcount + quadDcount) / 4f) * 10f) * celldiln * cellSuspVol * 1000f;
        float deadPerMl = (((quadAdeadcount + quadBdeadcount + quadCdeadcount + quadDdeadcount) / 4f) * 10f) * celldiln * cellSuspVol * 1000f;

        currtime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");

        WWWForm form = new WWWForm();
        form.AddField("sessionPost", FirstLogin.SessionID);
        form.AddField("cellconcPost", cellConc.ToString());
        form.AddField("deadratePost", deathRate.ToString()); 
        form.AddField("timedatePost", currtime);
        form.AddField("usernamePost", FirstLogin.Username);
        form.AddField("quadA_Post", QuadAinput.text);
        form.AddField("quadB_Post", QuadBinput.text);
        form.AddField("quadC_Post", QuadCinput.text);
        form.AddField("quadD_Post", QuadDinput.text);
        form.AddField("quadAreal_Post", quadAcount.ToString() + "-" + quadAdeadcount.ToString());
        form.AddField("quadBreal_Post", quadBcount.ToString() + "-" + quadBdeadcount.ToString());
        form.AddField("quadCreal_Post", quadCcount.ToString() + "-" + quadCdeadcount.ToString());
        form.AddField("quadDreal_Post", quadDcount.ToString() + "-" + quadDdeadcount.ToString());
        form.AddField("dilfac_Post", celldiln.ToString());

        WWW pagetogoto = new WWW("https://labtroubleshooter.000webhostapp.com/AddHaemGuess.php", form);



        for (int j = 0; j < totalCount; j++)
        {
            cellList[j].GetComponent<CellChars>().SetQuadrantColour();
        }

        GameObject QuadArepot = Instantiate(quadReportPrefab, new Vector3(-15.8f, 15f, -2f), quadReportPrefab.transform.rotation);
        QuadArepot.GetComponent<QuadReportChars>().UpdateQuadReport("A", quadAcount.ToString() + "-" + quadAdeadcount.ToString());
        GameObject QuadBrepot = Instantiate(quadReportPrefab, new Vector3(14.8f, 15f, -2f), quadReportPrefab.transform.rotation);
        QuadBrepot.GetComponent<QuadReportChars>().UpdateQuadReport("B", quadBcount.ToString() + "-" + quadBdeadcount.ToString());
        GameObject QuadCrepot = Instantiate(quadReportPrefab, new Vector3(14.8f, -15.8f, -2f), quadReportPrefab.transform.rotation);
        QuadCrepot.GetComponent<QuadReportChars>().UpdateQuadReport("D", quadDcount.ToString() + "-" + quadDdeadcount.ToString());
        GameObject QuadDrepot = Instantiate(quadReportPrefab, new Vector3(-15.8f, -15.8f, -2f), quadReportPrefab.transform.rotation);
        QuadDrepot.GetComponent<QuadReportChars>().UpdateQuadReport("C", quadCcount.ToString() + "-" + quadCdeadcount.ToString());

        GameObject LiveReport = Instantiate(quadReportPrefab, new Vector3(-1f, 1.7f, -2f), quadReportPrefab.transform.rotation);
        LiveReport.GetComponent<QuadReportChars>().UpdateFinalReport("Live " + livePerMl.ToString("#,#") + '\n' + "Dead " + deadPerMl.ToString("#,#") + '\n' + " in " + cellSuspVol.ToString() + " mL");
    }

    public void MakeDirt()
    {
        float bitstomake = 100;


        for(int i = 0; i < bitstomake; i++)
        {
            Vector3 possy = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(0f, 0.5f));
            Vector3 rotty = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            int RandyChoice = Random.Range(1, 2);
            GameObject bitofdirt = Instantiate(dirtPrefab, possy, Quaternion.Euler(rotty));
            bitofdirt.GetComponentInChildren<Renderer>().material = dirtMaterials[RandyChoice];
        }


    }

    public void ClearDirt()
    {
        GameObject[] bits = GameObject.FindGameObjectsWithTag("Dirt");
        foreach (GameObject bit in bits)
            GameObject.Destroy(bit);
    }
}
