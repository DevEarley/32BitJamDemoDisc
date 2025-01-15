using UnityEngine;
public static class GameService
{
    public static void OnClickButton(GameManifest gameManifest)
    {
        if(gameManifest.OnWebGL == true)
        {
            Application.OpenURL(gameManifest.DirectLink);
        }
        else
        {
            Application.OpenURL(gameManifest.ItchIOLink);
        }


    }
    public static void OnClick_SocialButton(GameManifest gameManifest)
    {
        Application.OpenURL(gameManifest.SocialMediaLink);
    }
}
