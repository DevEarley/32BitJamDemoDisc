using UnityEngine;
public static class GameService
{
   public static void OnClickButton(GameManifest gameManifest)
    {
        if (gameManifest.OnWindows) {
            Application.OpenURL("itchio://");
            Application.OpenURL(gameManifest.ItchIOLink);

        }
        else if (gameManifest.OnWebGL)
        {
            Application.OpenURL(gameManifest.ItchIOLink);

        }
        else
        {
            Application.OpenURL(gameManifest.ItchIOLink);

        }
    }
}
