
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ServiceLocator))]

public class DemoDiscController : MonoBehaviour
{
    [HideInInspector]
    public ServiceLocator ServiceLocator;
    public Sprite Windows;
    public Sprite MacOs;
    public Sprite Linux;
    public Sprite WebGL;
    public Sprite ROM;
    public Sprite InstaPlay;
    public Sprite DownloadPlay;
    public LayerMask GameListLayerMask;
    private void Start()
    {
        ServiceLocator = gameObject.GetComponent<ServiceLocator>();
        
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

}
