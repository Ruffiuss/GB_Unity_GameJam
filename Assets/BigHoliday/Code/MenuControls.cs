using UnityEngine;
using UnityEngine.SceneManagement;

namespace Island
{
    public class MenuControls : MonoBehaviour
    {
        public void PlayPressed()
        {
            SceneManager.LoadScene("Game");
        }

        //public void EscPressed()
        //{
        //    Input.GetKeyDown(KeyCode.Escape);
        //    SceneManager.LoadScene("Game");
        //}

        public void ExitPressed()
        {
            Application.Quit();
        }
    }
}