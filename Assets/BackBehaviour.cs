using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBehaviour : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
