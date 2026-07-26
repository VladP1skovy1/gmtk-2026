using UnityEngine;

namespace LaunchBad.Core
{
    public class SceneManager : MonoBehaviour
    {
        public static void LoadScene(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
    }
}
