using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public GameObject navPanel;


    public void ToggleNavPanel()
    {
        if (navPanel.activeSelf)
        {
            navPanel.SetActive(false); 
        }
        else
        {
            navPanel.SetActive(true);
        }
    }


    public void ChangeToScene(string sceneName)
    {

        string currtime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", FirstLogin.Username);
        form.AddField("timedatePost", currtime);
        form.AddField("sessionPost", FirstLogin.SessionID);
        form.AddField("scenePost", sceneName);

        WWW pagetogoto = new WWW("http://localhost/viro/AddNewSession.php", form);

        SceneManager.LoadScene(sceneName);
    }

  
}
