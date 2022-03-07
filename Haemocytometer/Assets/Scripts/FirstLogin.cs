using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstLogin : MonoBehaviour
{
    //attached to EmptyHolder on Login Scene

    public InputField nameInput;

    public static string SessionID;
    public static string Username;

    string charlist = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456790";

    private void Awake()  //on first starting, set the SessionID
    {
        for(int i = 0; i<8; i++)
        {
        char c =  charlist[Random.Range(0,charlist.Length)];
        SessionID = SessionID + c.ToString();
        }

    }

    public void CheckRecordAndGo(string sceneName)
    {
        if (nameInput.text.Length > 0)
        {
            Username = nameInput.text;
            MakeRecordinDB(sceneName);
            SceneManager.LoadScene(sceneName);
        }

        else
        {
            nameInput.placeholder.GetComponent<Text>().text = "Must provide some info";
        }
    }

    public void MakeRecordinDB(string scene)
    {
        string currtime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("timedatePost", currtime);
        form.AddField("sessionPost", SessionID);
        form.AddField("scenePost", scene);

        WWW pagetogoto = new WWW("https://labtroubleshooter.000webhostapp.com/AddNewSession.php", form);
    }








}
