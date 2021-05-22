using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitObject : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Clear!");

            LoadScene();
        }
    }
}
