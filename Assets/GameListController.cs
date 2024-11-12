
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
        SortedGames.ToList().ForEach(x => {
            Debug.Log(x);
        });
        ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        Animator = gameObject.GetComponent<Animator>();
    }

    private void SetGamesToTarget()
    {
        Debug.Log("SetGamesToTarget");
        for (var i = 0; i < Targets.Count; i++)
        {
            // Debug.Log(i + "|" + SortedGames.Length);
            var enoughGames = SortedGames.Length > i;
            if (enoughGames == false)
            {
                i = Targets.Count;
                return;
            }
            var offset_i = i + index;
            if (offset_i  >= SortedGames.Length) {
                offset_i = offset_i - (SortedGames.Length );
            }

            var game = SortedGames[offset_i];

            var target = Targets[i];

            if(game == null || target == null)
            {
                return;
            }

             game.transform.SetParent(target.transform, false);
        }
      
    }
    public void Idle_Animation()
    {
        Debug.Log("Idle_Animation");

        Animator.Play("game-list-intro-idle");
    }
    public void ShiftGames_left()
    {
        Debug.Log("ShiftGames_left");

        index--;
        if (index < 0)
        {
            index = SortedGames.Length-1;
        }

        SetGamesToTarget();
    }

    public void ShiftGames_right()
    {
        index++;
        if (index > SortedGames.Length - 1)
        {
            index = 0;
        }

        SetGamesToTarget();
    }


}

