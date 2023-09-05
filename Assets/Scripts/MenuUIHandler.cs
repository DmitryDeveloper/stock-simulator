using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        // pass color to MainManager
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        //we will fetch color from storage
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    // we assign event onClick on Start button
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        //save clor in storage befor exiting
        MainManager.Instance.SaveColor(); 

        //it is actually instructions for the compiler. (lines starts with # , such lines will not builded)
        // inside unity it will compile EditorApplication.ExitPlaymode() but during buid Application.Quit()
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }

    //assign on SaveColor button
    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }
    //assign on LoadColor button
    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
