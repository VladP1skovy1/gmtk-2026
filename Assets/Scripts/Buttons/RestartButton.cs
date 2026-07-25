using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaunchBad.Buttons
{
    public class RestartButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}