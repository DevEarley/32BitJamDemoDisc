using UnityEngine;

[RequireComponent(typeof(MusicController))]
[RequireComponent(typeof(SFXController))]
public class ServiceLocator : MonoBehaviour
{
    public MusicController MusicController;
    public Animator MenuAnimator ;
    public Animator GameListAnimator;
    public DemoDiscController DemoDiscController;
    public GameListController GameListController;
    public SFXController SFXController;
    
    private void Start()
    {
     
    }
}
