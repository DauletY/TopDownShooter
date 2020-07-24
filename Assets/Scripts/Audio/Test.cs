using UnityEngine;

public class Test : MonoBehaviour {
    public new AudioSource audio;
    private void Start() {
        Creator[] creators =  {new ConcreateCreator(audio) };

        foreach(var item in creators) {
            AudioProduct audioProduct  = item.FactoryMethod();
            Debug.Log("Audio creator: " + audioProduct.GetType());

        }
    }
}