using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image Bar;
    private float fill;
    public float maxhp;
    private float hp;
    public GameObject player;
    public GameObject EndScreen;

    private void Start()
    {
        hp = maxhp;
        fill = 1f;
        EndScreen.SetActive(false);
    }

    private void Update()
    {
        Bar.fillAmount = fill;

        if (hp <= 0f)
        {
            EndScreen.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy") 
        {
            fill = hp / maxhp - 20 / maxhp;
            hp = hp - 20;
        }

        if (coll.gameObject.tag == "Bullet")
        {
            fill = hp/maxhp - 25/maxhp;
            hp = hp - 25;
        }
    }

}
