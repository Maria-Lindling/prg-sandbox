using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    public void ButtonPressContinue()
    {
        DebugLogButonPress("Continue");
    }

    public void ButtonPressNewGame()
    {
        DebugLogButonPress("Start");
    }

    public void ButtonPressLoadGame()
    {
        DebugLogButonPress("Load");
    }
    public void ButtonPressSettings()
    {
        DebugLogButonPress("Settings");
    }

    public void ButtonPressQuit()
    {
        DebugLogButonPress("Quit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    private void DebugLogButonPress(string message) => Debug.Log(new StringBuilder(message).Append(" Button Pressed").ToString());

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
