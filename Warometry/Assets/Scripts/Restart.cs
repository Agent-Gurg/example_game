using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
