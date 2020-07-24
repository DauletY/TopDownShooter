using UnityEngine;
public class ConcreateCreator : Creator{
    public AudioSource audio;


    public ConcreateCreator(AudioSource audio) {
            this.audio = audio;
        }

    public override AudioProduct FactoryMethod() {
            return new ConcreateProduct(audio);
    }
}