using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stageText;
    [SerializeField]
    private string prevSceneName;
    [SerializeField]
    private string sceneName;
    private bool isLock = true;
    private Button button;
    [SerializeField]
    private GameObject lockImage;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (prevSceneName == "")
        {
            isLock = false;
        }
        else if (StageDataManager.instance.stageDatas.ContainsKey(prevSceneName))
        {
            if (StageDataManager.instance.stageDatas[prevSceneName] && sceneName != "")
            {
                isLock = false;
            }
        }
        else
        {
            StageDataManager.instance.stageDatas.Add(prevSceneName, false);
        }

        if (isLock)
        {
            lockImage.SetActive(true);
            button.interactable = false;
        }
        else
        {
            lockImage.SetActive(false);
            button.interactable = true;
        }

        button.onClick.AddListener(LoadScene);

        stageText.text = (transform.GetSiblingIndex() + 1).ToString();
    }

    private void LoadScene()
    {
        if (isLock == false)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
