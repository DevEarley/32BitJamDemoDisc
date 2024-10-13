using UnityEngine;

[RequireComponent(typeof(MusicController))]
[RequireComponent(typeof(SFXController))]
public class ServiceLocator : MonoBehaviour
{
    [HideInInspector]
    public MusicController MusicController;
    
    [HideInInspector]
    public SFXController SFXController;
    
    private void Start()
    {
        MusicController = gameObject.GetComponent<MusicController>();
        SFXController = gameObject.GetComponent<SFXController>();
    }
}
