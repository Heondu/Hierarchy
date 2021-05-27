using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject settingUI;
    private bool isPause = false;
    public bool IsPause => isPause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.instance != null && GameController.instance.IsClear) return;

            if (settingUI.activeSelf)
            {
                settingUI.SetActive(false);
                pauseUI.SetActive(true);
                return;
            }

            isPause = !isPause;
            pauseUI.SetActive(isPause);
            if (isPause) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        pauseUI.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
