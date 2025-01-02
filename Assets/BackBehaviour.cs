using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBehaviour : MonoBehaviour
{
    public void GoBack()
    {

        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu()
    {
        var ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        ServiceLocator.MusicController.Play_TitleScreen_Music();
        ServiceLocator.GameListController.is_highlighting_game = false;
    }

    public void BackToGameSelection()
    {
        var ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        ServiceLocator.MusicController.Play_GameSelection_Music();

        ServiceLocator.GameListController.is_highlighting_game = false;
    }

}
