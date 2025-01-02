using UnityEngine;

[RequireComponent(typeof(MusicController))]
[RequireComponent(typeof(SFXController))]
public class ServiceLocator : MonoBehaviour
{
    [HideInInspector]
    public MusicController MusicController;

    public Animator MenuAnimator ;
    public Animator GameListAnimator;
    [HideInInspector]
    public DemoDiscController DemoDiscController;
    public GameListController GameListController;
    [HideInInspector]
    public SFXController SFXController;
    
    private void Start()
    {
        GameListController = FindAnyObjectByType<GameListController>();
        DemoDiscController = gameObject.GetComponent<DemoDiscController>();
        MusicController = gameObject.GetComponent<MusicController>();
        SFXController = gameObject.GetComponent<SFXController>();
    }
}
