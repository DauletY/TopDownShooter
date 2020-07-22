
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class TopDowmView
{
    private string name = null;
    private Button newGame = null;
    private Button saveGame = null;
    private Button option = null;
    private static TopDowmView view = null;
    private float startTime;
    public TopDowmView () { }
    public TopDowmView(string name)
    {
        this.name = name;
    }
    public TopDowmView(Button button, Button button1, Button button2)
    {
        newGame = button;
        saveGame = button1;
        option = button2;
     
    }
    public Button NewGame
    {
        get => newGame;
        set => newGame = value;
    }
    public Button SaveGame
    {
        get => saveGame;
        set => saveGame = value;
    }
    public Button Option
    {
        get => option;
        set => option = value;
    }
    public string Name
    {
        get => name;
        set => name = value;
    }

    public float StartTime
    {
        get => startTime; 
        set => startTime = value; 
    }

    public static IEnumerator Call(string name, float time )
    {
        view = new TopDowmView();
        yield return new WaitForSeconds(view.StartTime = time);
        SceneManager.LoadScene(view.Name = name);
    }
}
