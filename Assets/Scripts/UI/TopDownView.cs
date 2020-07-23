
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class TopDownView
{
    private string name = null;
    private Button newGame = null;
    private Button saveGame = null;
    private Button option = null;
    private static TopDownView view = null;
    private float startTime;
    public TopDownView () { }
    public TopDownView(string name)
    {
        this.name = name;
    }
    public TopDownView(Button button, Button button1, Button button2)
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
        set => name = value ??
             throw new System.ArgumentNullException(nameof(value), "Name cannot be null");
    }

    public float StartTime
    {
        get => startTime; 
        set => startTime = value; 
    }

    public static IEnumerator Call(string name, float time )
    {
        view = new TopDownView();
        yield return new WaitForSeconds(view.StartTime = time);
        SceneManager.LoadScene(view.Name = name);
    }
}
