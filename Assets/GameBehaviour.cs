using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    [HideInInspector]
    public ServiceLocator ServiceLocator;
    public GameManifest GameManifest;
    public Image Image;
    public TextMeshProUGUI Text;

    private void Start()
    {
        ServiceLocator = FindAnyObjectByType<ServiceLocator>();

        Image.sprite = GameManifest.ThumbnailImage;
        var text = GameManifest.Name + "\n";
        text += GameManifest.AuthorOrTeamName + "\n";

        Text.text = text;
    }
    public void OnClick()
    {

        ServiceLocator.MusicController.Pause();
    
        ServiceLocator.GameListController.ChooseGame(GameManifest);


    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus && ServiceLocator && ServiceLocator.MusicController)
        ServiceLocator.MusicController.Play_GameSelection_Music();

    }

}
