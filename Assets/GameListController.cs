
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class GameListController : MonoBehaviour
{
    [HideInInspector]
    public ServiceLocator ServiceLocator;
    [HideInInspector]
    public Animator Animator;

    public List<GameObject> Targets;
    public List<GameObject> Games;

    private GameObject[] SortedGames;
    private int index = 0;
    private void Start()
    {
        SortedGames = Games.ToArray();
        ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        Animator = gameObject.GetComponent<Animator>();
    }

    private void SetGamesToTarget_AfterAnimation()
    {
        Debug.Log("SetGamesToTarget");
        for (var i = 0; i < Games.Count; i++)
        {
            var offset_i = i + index;
            if (offset_i >= SortedGames.Length)
            {
                offset_i = offset_i - (SortedGames.Length);
            }
            var game = SortedGames[offset_i];

            if (i <= Targets.Count - 1)
            {
                var target = Targets[i];
                game.SetActive(true);

                game.transform.SetParent(target.transform, false);
             }
            else
            {
                game.SetActive(false);
            }
        }


    }
    public void Idle_Animation()
    {
        Debug.Log("Idle_Animation");

        Animator.Play("game-list-intro-idle");
    }

    public void ShiftGames_left()
    {
        index--;
        if (index < 0)
        {
            index = SortedGames.Length-1;
        }

        SetGamesToTarget_AfterAnimation();
    }

    public void ShiftGames_right()
    {
        index++;
        if (index > SortedGames.Length - 1)
        {
            index = 0;
        }

        SetGamesToTarget_AfterAnimation();
    }



}

