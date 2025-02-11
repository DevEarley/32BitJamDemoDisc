﻿using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// Attach this script below a GameObject with an AudioSource and manually assign a clip and enable Play on Awake.
/// Since this script does not care what song is playing you can implement an Audio manager to change songs as you wish.
/// Make sure to manually assign two prefabs of your choice.
/// </summary>
public class SpectrumAnalyzer : MonoBehaviour
{
    public AnalyzerSettings settings; //All of our settings
    public Animator Camera_Rig_animator;
    public GameObject Logo;
    public MeshRenderer Grid;
    //private
    private float[] spectrum; //Audio Source data
    private List<GameObject> pillars; //ref pillars to scale/move with music
    private GameObject folder;
    private bool isBuilding; //Prevents multi-calls and update while building.


    void Start()
    {
        isBuilding = true;
        CreatePillarsByShapes();
    }

    private void CreatePillarsByShapes()
    {
        //get current pillar types
        GameObject currentPrefabType = settings.pillar.type == PillarTypes.Cylinder ? settings.Prefabs.CylPrefab : settings.Prefabs.BoxPrefab;
       
        
        pillars = MathB.ShapesOfGameObjects(currentPrefabType, settings.pillar.radius, (int) settings.pillar.amount,settings.pillar.shape);

        //Organize pillars nicely in this folder under this transform
        folder = new GameObject("Pillars-" + pillars.Count);
        folder.transform.SetParent(transform);

        foreach (var piller in pillars)
        {
            piller.transform.SetParent(folder.transform);
        }

        isBuilding = false;
    }

    private float momentum = 0;
    private float momentum2 = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) Rebuild();
        if (isBuilding) return;

        spectrum = AudioListener.GetSpectrumData((int) settings.spectrum.sampleRate, 0, settings.spectrum.FffWindowType);

        var sum = 0.0f;

        for (int i = 0; i < pillars.Count; i++) //needs to be <= sample rate or error
        {
            float level = spectrum[i]*settings.pillar.sensitivity*Time.deltaTime*1000; //0,1 = l,r for two channels
            sum += level;
            //Scale pillars 
            Vector3 previousScale = pillars[i].transform.localScale;
            previousScale.y = Mathf.Lerp(previousScale.y, level, settings.pillar.speed*Time.deltaTime);
                //Add delta time please
            pillars[i].transform.localScale = previousScale;

            //Move pillars up by scale/2
            Vector3 pos = pillars[i].transform.position;
            pos.y = previousScale.y*.5f;
            pillars[i].transform.position = pos;
        }
        var average = sum / pillars.Count;
        momentum += (average * average);
        momentum2 += (average * 0.01f);


        momentum2 = Mathf.Min(10.0f, momentum2);
        momentum2 = Mathf.Lerp(momentum2, 0.0f, Time.deltaTime  * 8.0f);
        momentum2 = Mathf.Max(0.05f, momentum2);

        Camera_Rig_animator.speed = momentum2;
        Grid.material.SetFloat("_Amplitude2", (20.0f* momentum2));
       //Grid.material.SetFloat("_Frequency1", (2.0f* average));
        Grid.material.SetFloat("_Emission", 0.2f+(momentum2*3.0f));

        momentum = Mathf.Min(0.4f, momentum);

        momentum = Mathf.Lerp(momentum, 0.0f, Time.deltaTime  * 4.0f);
        momentum = Mathf.Max(0.05f, momentum);
        if (average > 0.9)
        {
            BigMode = Mathf.Lerp(BigMode, 15.0f, Time.deltaTime * (average/4.0f));
        }
        else
        {
            BigMode = Mathf.Lerp(BigMode, 0, Time.deltaTime * 4.0f);

        }
            Logo.transform.localScale = momentum*Vector3.one*(15.0f + BigMode);
    }
    public float BigMode = 0;
    /// <summary>
    /// Called by UI slider onValue changed
    /// </summary>
    public void Rebuild()
    {
        if (isBuilding) return;

        //Destroy the pillars we had, clear the pillar list and start over
        isBuilding = true;
        pillars.Clear();
        DestroyImmediate(folder);
        CreatePillarsByShapes();
    }

    /// <summary>
    /// Resets to all settings to default in inspector drop down.
    /// </summary>
    private void Reset()
    {
        settings.pillar.Reset();
        settings.spectrum.Reset();
    }

    #region Dynamic floats and for UI sliders

    /// <summary>
    /// Convert Shapes enum to an int from a float so we can control by UI Slider
    /// </summary>
    public float PillarShape
    {
        get { return (int) settings.pillar.shape; }
        set
        {
            //set with UI Slider
            int num = (int) Mathf.Clamp(value, 0, 3);
            settings.pillar.shape = (Shapes) num;
        }
    }

    public float PillarType
    {
        get { return (int) settings.pillar.type; }
        set
        {
            //set with UI Slider
            int num = (int)Mathf.Clamp(value, 0, 2); 
            settings.pillar.type = (PillarTypes) num;
        }
    }

    public float Amount
    {
        get { return settings.pillar.amount; }
        set
        {
            settings.pillar.amount = Mathf.Clamp(value, 4, 128);
            
        }
    }

    public float Radius
    {
        get { return settings.pillar.radius; }
        set { settings.pillar.radius = Mathf.Clamp(value, 2, 256); }
    }


    public float Sensitivity
    {
        get { return settings.pillar.sensitivity; }
        set { settings.pillar.sensitivity = Mathf.Clamp(value, 1, 50); }
    }

    public float PillarSpeed
    {
        get { return settings.pillar.speed; }
        set { settings.pillar.speed = Mathf.Clamp(value, 1, 30); }
    }


    public float SampleMethod
    {
        get { return (int) settings.spectrum.FffWindowType; }
        set
        {
            //set with UI Slider
            int num = (int)Mathf.Clamp(value, 0, 6); 
            settings.spectrum.FffWindowType = (FFTWindow) num;
        }
    }

    public float SampleRate
    {
        get { return (int) settings.spectrum.sampleRate; }
        set
        {
            //set with UI Slider
            int num = (int) Mathf.Pow(2, 7 + value);//128,256,512,1024,2056
            settings.spectrum.sampleRate = (SampleRates) num;
        }
    }

    #endregion
}