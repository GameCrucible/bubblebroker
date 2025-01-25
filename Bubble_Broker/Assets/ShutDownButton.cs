using UnityEngine;
using UnityEngine.SceneManagement;

public class ShutDownButton : MonoBehaviour
{
    //Initialize the scene to load
    public string sceneToLoad;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
