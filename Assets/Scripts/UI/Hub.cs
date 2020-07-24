using UnityEngine.UI;
using UnityEngine;

public class Hub : MonoBehaviour
{
    public Slider slider;
    public Text bullet;
    private int lastHelath = 0;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = Band._health;
        // vitality =50
        if(lastHelath != Band._health) {
            slider.value = Band._health;
            lastHelath = Band._health;
            animator.SetBool("Update", true);
        }else {
            animator.SetBool("Update", false);
        }
        bullet.text= (Band.clipSize - BandWeapon.shotsFired).ToString();
    }
}
