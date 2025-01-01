using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour
{
    public void ClickedCredits()
    {
        SceneManager.LoadScene("credits");
    }
}
