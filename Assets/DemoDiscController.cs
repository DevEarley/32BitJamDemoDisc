
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ServiceLocator))]

public class DemoDiscController : MonoBehaviour
{
    [HideInInspector]
    public ServiceLocator ServiceLocator;

    private void Start()
    {
        ServiceLocator = gameObject.GetComponent<ServiceLocator>();
    }



}
