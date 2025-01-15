using System.Collections;
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
    private static float glyph_space = 23f;
    private void Start()
    {
        ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        StartCoroutine(Delayed_start());

    }

    private IEnumerator Delayed_start()
    {
        yield return new WaitForSeconds(1.0f);
        Image.sprite = GameManifest.ThumbnailImage;
        var text = GameManifest.Name + "\n";
        text += GameManifest.AuthorOrTeamName + "\n";
        var number_of_icons_ = 0;
        if (GameManifest.OnWindows == true)
        {
            MakeSprite(ServiceLocator.DemoDiscController.Windows, Vector3.zero);
            number_of_icons_ += 1;
        }
        if (GameManifest.OnLinux == true)
        {
            MakeSprite(ServiceLocator.DemoDiscController.Linux, new Vector3(-number_of_icons_ * glyph_space, 0, 0));
            number_of_icons_ += 1;

        }
        if (GameManifest.OnMacOS == true)
        {
            MakeSprite(ServiceLocator.DemoDiscController.MacOs, new Vector3(-number_of_icons_ * glyph_space, 0, 0));
            number_of_icons_ += 1;
        }
        if (GameManifest.Rom == true)
        {
            MakeSprite(ServiceLocator.DemoDiscController.ROM, new Vector3(-number_of_icons_ * glyph_space, 0, 0));
            number_of_icons_ += 1;
        }
        if (GameManifest.OnWebGL == true)
        {
            MakeSprite(ServiceLocator.DemoDiscController.WebGL, new Vector3(-number_of_icons_ * glyph_space, 0, 0));
            number_of_icons_ += 1;
        }



        Text.text = text;
    }

    private void MakeSprite(Sprite sprite, Vector3 offset)
    {
        var glyph__ = new GameObject();
        glyph__.transform.parent = transform;
        var glyph___image = glyph__.AddComponent<Image>();
        glyph___image.sprite = sprite;
        glyph___image.SetNativeSize();
     
        var rect_transform__ = glyph___image.rectTransform;
        rect_transform__.anchorMin = Vector2.one;
        rect_transform__.anchorMax = Vector2.one;
        rect_transform__.anchoredPosition = offset;
        glyph__.layer = glyph__.transform.parent.gameObject.layer;
      
    }

    private  void make_glyph(GameObject glyph_windows)
    {
   
    }

    public void OnClick()
    {

        ServiceLocator.MusicController.Pause();
        ServiceLocator.MusicController.Play_GameInfo_Music();

        ServiceLocator.GameListController.ChooseGame(GameManifest);


    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus && ServiceLocator && ServiceLocator.MusicController)
        ServiceLocator.MusicController.Play_GameSelection_Music();

    }

}
