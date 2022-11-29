using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    
    public void LoadInstagram()
    {
        Application.OpenURL("https://www.instagram.com/s_z_e_r_g_i_u_s_z/?hl=en");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
