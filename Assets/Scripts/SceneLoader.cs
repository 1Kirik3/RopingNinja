using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneNumber) 
        => SceneManager.LoadScene(sceneNumber);

    public void ExitGame()
        => Application.Quit();
}
