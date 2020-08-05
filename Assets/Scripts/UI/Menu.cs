using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool m_SceneLoaded;
    public Button button;
    public Button button1;
    public Button button2;

    public float startTime, time;
    public new string name;
    TopDownView view;
    private void Start()
    {
        view = new TopDownView(button, button1, button2);
    }
    public void New_Game()
    {
        StartCoroutine(TopDownView.Call(name, time));
        Debug.Log("Start game");
    }
    public void Save_Game()
    {
        Debug.Log(view.SaveGame.name);
    }
    public void Option()
    {
        Debug.Log(view.Option.name);
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
