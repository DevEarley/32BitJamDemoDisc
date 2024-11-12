using UnityEngine;


[CreateAssetMenu(fileName = "GameManifest", menuName = "Scriptable Objects/GameManifest")]
public class GameManifest : ScriptableObject
{
   public string AuthorOrTeamName;
    public string ItchIOLink;
    public string SocialMediaLink;
    public string Description;
    public string Name;
    public Sprite ThumbnailImage;
    public bool OnWindows;
    public bool OnMacOS;
    public bool OnLinux;
    public bool OnWebGL;
    public GameManifest_GameJam GameJam_1 = GameManifest_GameJam.Unselected;
    public GameManifest_GameJam GameJam_2 = GameManifest_GameJam.Unselected;
    public GameManifest_GameJam GameJam_3 = GameManifest_GameJam.Unselected;
    public GameManifest_GameJam GameJam_4 = GameManifest_GameJam.Unselected;



    [System.Serializable]
    public enum GameManifest_GameJam {
        Unselected  = 0,
        NewTo32BitJam  = 1,
        Spring_2019,
        Summer_2019,
        Fall_2019,
        Winter_2019,
        Spring_2020,
        Summer_2020,
        Fall_2020,
        Winter_2020,
        Spring_2021,
        Summer_2021,
        Fall_2021,
        Winter_2021,
        Spring_2022,
        Summer_2022,
        Fall_2022,
        Winter_2022,
        Spring_2023,
        Summer_2023,
        Fall_2023,
        Winter_2023,
        Spring_2024,
        Summer_2024,
        Fall_2024,
        Winter_2024,
    }


}
