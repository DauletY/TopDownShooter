using UnityEngine;
public class ConcreateProduct : AudioProduct {
     public AudioSource source;
     public ConcreateProduct(AudioSource audio) {
         source = audio;
     }
}