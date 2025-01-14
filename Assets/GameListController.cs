
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class GameListController : MonoBehaviour
{
    [HideInInspector]
    public ServiceLocator ServiceLocator;
    [HideInInspector]
    public Animator Animator;

    public List<GameObject> Targets;
    public List<GameObject> Games;

    public TextMeshProUGUI GameDescription;
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI GameTeam;
    public TextMeshProUGUI GameSocial;
    public TextMeshProUGUI GameLink;
    public Image GameCover;
    public Image GameBadge_Windows;
    public Image GameBadge_MacOS;
    public Image GameBadge_Linux;
    public Image GameBadge_WebGL;
    public Image GameBadge_Rom;


    private GameObject[] SortedGames;
    private int index = 1;
    private void Start()
    {
        SortedGames = Games.ToArray();
        ServiceLocator = FindAnyObjectByType<ServiceLocator>();
        Animator = gameObject.GetComponent<Animator>();
        SetGamesToTarget_AfterAnimation();
        ServiceLocator.MenuAnimator.Play("choose-game");


    }


    private void SetGamesToTarget_AfterAnimation() { 
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
    private Vector4 transparent_color = new Vector4(1.0f, 1.0f, 1.0f, 0.1f);
    public GameManifest CurrentGame;
    public void ChooseGame(GameManifest GameManifest)
    {
        is_highlighting_game = true;
        ServiceLocator.MenuAnimator.Play("choose-game-highlight-game");
        CurrentGame = GameManifest;
        ServiceLocator.SFXController.Play_SelectGame_SFX();
        GameDescription.text = CurrentGame.Description;
        GameTitle.text = CurrentGame.Name;
        GameTeam.text = CurrentGame.AuthorOrTeamName;
        GameSocial.text = CurrentGame.SocialMediaLink;
        GameLink.text = CurrentGame.ItchIOLink;
        GameCover.sprite = CurrentGame.ThumbnailImage;
        if (CurrentGame.OnLinux == true)
        {
            GameBadge_Linux.color = Color.white;
        }
        else
        {
            GameBadge_Linux.color = transparent_color;
        }
        if (CurrentGame.OnWindows == true)
        {
            GameBadge_Windows.color = Color.white;
        }
        else
        {
            GameBadge_Windows.color = transparent_color;
        }
        if (CurrentGame.OnWebGL == true)
        {
            GameBadge_WebGL.color = Color.white;

        }
        else
        {
            GameBadge_WebGL.color = transparent_color;
        }
        if (CurrentGame.OnMacOS == true)
        {
            GameBadge_MacOS.color = Color.white;
        }
        else
        {
            GameBadge_MacOS.color = transparent_color;
        }
        if (CurrentGame.Rom == true)
        {
            GameBadge_Rom.color = Color.white;
        }
        else
        {
            GameBadge_Rom.color = transparent_color;
        }
    }
    public void OnClick_GameLink()
    {
        GameService.OnClickButton(CurrentGame);
    }
    public void OnClick_SocialLink()
    {
        GameService.OnClick_SocialButton(CurrentGame);

    }
    public void Idle_Animation()
    {
        Debug.Log("Idle_Animation");

        Animator.Play("game-list-intro-idle");
    }

    public void ShiftGames_left()
    {
        if (is_highlighting_game == true)
        {
            is_highlighting_game = false;
            ServiceLocator.MenuAnimator.Play("choose-game-un-highlight");
        }
        index--;
        if (index < 0)
        {
            index = SortedGames.Length-1;
        }

        SetGamesToTarget_AfterAnimation();
    }
    public bool is_highlighting_game = false;

    public void ShiftGames_right()
    {
        if(is_highlighting_game == true)
        {
            is_highlighting_game = false;
            ServiceLocator.MenuAnimator.Play("choose-game-un-highlight");
        }
        index++;
        if (index > SortedGames.Length - 1)
        {
            index = 0;
        }

        SetGamesToTarget_AfterAnimation();
    }



}

