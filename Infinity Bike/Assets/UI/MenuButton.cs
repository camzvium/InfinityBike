﻿using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public enum Menu
    {
        STARTSCREEN = 0, OPTIONSCEEN = 1 , GAMESCREEN = 2
    }
    public Transform[] onScreenButtonGroup;
    public int activeGroup = -1;

    public void ActivateMenuButtons(int id)
    {
        foreach (Transform item in onScreenButtonGroup)
        { item.gameObject.SetActive(false); }

        onScreenButtonGroup[id].gameObject.SetActive(true);
        activeGroup = id;
    }   

    public void DeactivateMenuButtons(int id)
    {
        foreach (Transform item in onScreenButtonGroup)
        { item.gameObject.SetActive(false); }
        activeGroup = -1;
    }

    public void ExitGame()
    {   
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }   

    public void LoadScene(int scene)
    {

        SceneManager.LoadScene(scene);
    }

}
