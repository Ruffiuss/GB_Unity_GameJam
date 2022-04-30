using UnityEngine;
using UnityEngine.SceneManagement;

namespace Island
{
    public class MenuControls : MonoBehaviour
    {
        public void PlayPressed()
        {
            SceneManager.LoadScene("NewGame");
        }

        public void ExitPressed()
        {
            Application.Quit();
        }
    }
}