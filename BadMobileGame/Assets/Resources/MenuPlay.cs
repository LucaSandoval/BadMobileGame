using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlay : MonoBehaviour
{
    public string MainScene = "BackGroundTest";
    // Public method to load a scene by name
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(MainScene);
    }



    public List<GameObject> settingObjects;
    public List<GameObject> MainMenuObjects;

    public void StartSettings() {
        ActivateObjects(settingObjects);
        DeactivateObjects(MainMenuObjects);
    }

    public void EndSettings() {
        DeactivateObjects(settingObjects);
        ActivateObjects(MainMenuObjects);
    }

    // Method to activate the specified objects
    public void ActivateObjects(List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(true);
        }
    }

    // Method to deactivate the specified objects
    public void DeactivateObjects(List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(false);
        }
    }
}
