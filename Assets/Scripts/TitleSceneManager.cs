using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private Pause pause;

    private void Update()
    {
        if (pause.IsPause) return;

        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
