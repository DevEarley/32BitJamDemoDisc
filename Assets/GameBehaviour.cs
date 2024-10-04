using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    public GameManifest GameManifest;
    public Image Image;
    public TextMeshProUGUI Text;

    private void Start()
    {
        Image.sprite = GameManifest.ThumbnailImage;
        var text = GameManifest.Name + "\n";
        text += GameManifest.AuthorOrTeamName + "\n";

        Text.text = text;
    }
    public void OnClick()
    {
       
        GameService.OnClickButton(GameManifest);
    }


}
