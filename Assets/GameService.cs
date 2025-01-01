using UnityEngine;
public static class GameService
{
   public static void OnClickButton(GameManifest gameManifest)
    {
         Application.OpenURL(gameManifest.ItchIOLink);
    
        
    }

}
