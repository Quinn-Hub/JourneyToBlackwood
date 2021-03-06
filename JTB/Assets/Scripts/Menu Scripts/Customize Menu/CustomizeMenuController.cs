﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Anima2D;
public class CustomizeMenuController : MonoBehaviour {

    [SerializeField]
    private Slider skinColorSlider;
    public int startLevelID;
    //Anima2D script
    [HideInInspector]
    private SpriteMeshInstance eyeScript;
    private SpriteMeshInstance hairScript;
    // Use this for initialization
    void Start () {
        // Reference the mesh instance script that is on the eyes on the character sprite
        eyeScript = GameObject.Find("MC Sprite").transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<SpriteMeshInstance>();
        hairScript = GameObject.Find("MC Sprite").transform.GetChild(1).GetChild(4).GetChild(2).GetComponent<SpriteMeshInstance>();

    }


    public void StartGame()
    {

        loadScene.LoadScene(startLevelID);
    }


    public void ChangeSkinColor()
    {
        CustomizationManager.instance.AdjustSkinToneValues(skinColorSlider.value);
    }


    public void ChangeCostume(int dir)
    {
        CustomizationManager.instance.AdvanceCurrentCostume(dir);
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SetYellowEyeColor()
    {
        Color yellowHue = new Color(1, .8f, 0, .75f);
        eyeScript.color = yellowHue;
        GlobalColor.Instance.eyeColor = yellowHue;

    }
    public void SetPurpleEyeColor()
    {
        Color purpleHue = new Color(.5f, 0, 1, .75f);
        eyeScript.color = purpleHue;
        GlobalColor.Instance.eyeColor = purpleHue;
    }

    public void SetBlueEyeColor()
    {
        Color blueHue = new Color(0, .7f, 1, .75f);
        eyeScript.color = blueHue;
        GlobalColor.Instance.eyeColor = blueHue;

    }

    public void SetYellowHairColor()
    {
        Color yellowHue = new Color(1, .8f, 0, 1);
        hairScript.color = yellowHue;
        GlobalColor.Instance.hairColor = yellowHue;

    }
    public void SetPurpleHairColor()
    {
        Color purpleHue = new Color(.5f, 0, 1, 1);
        hairScript.color = purpleHue;
        GlobalColor.Instance.hairColor = purpleHue;

    }
    public void SetPinkHairColor()
    {
        Color pinkHue = new Color(1, .45f, .98f, 1f);
        hairScript.color = pinkHue;
        GlobalColor.Instance.hairColor = pinkHue;

    }
    public void SetBlueHairColor()
    {
        Color blueHue = new Color(0, .7f, 1, 1);
        hairScript.color = blueHue;
        GlobalColor.Instance.hairColor = blueHue;

    }
    public void ChangeHairStyle(int dir)
    {
        CustomizationManager.instance.AdvanceCurrentHairStyle(dir);
    }


    public void ChangeFace(int dir)
    {
        CustomizationManager.instance.AdvanceCurrentFace(dir);
    }

}
