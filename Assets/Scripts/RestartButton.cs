using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaunchBad
{
    public class RestartButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}