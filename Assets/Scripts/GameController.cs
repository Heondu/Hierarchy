using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public SelectableObject selectObject { get; private set; } = null;
    private SelectableObject overlapObject = null;

    [SerializeField]
    private GameObject playImage;
    [SerializeField]
    private GameObject pauseImage;
    [SerializeField]
    private GameObject clearUI;

    private ExitObject exit;

    private LayerMask layerMask;

    private bool isStop = true;
    public bool IsStop => isStop;
    private bool isClear = false;
    public bool IsClear
    {
        get => isClear;

        set
        {
            isClear = value;
            if (value == true)
            {
                Stop(true);
                StageDataManager.instance.Clear();
            }
        }
    }

    private void Awake()
    {
        if (instance == false) instance = this;
        else Destroy(gameObject);

        Stop(true);
    }

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("SelectableObject");

        exit = FindObjectOfType<ExitObject>();
        exit.onClear.AddListener(OnClear);
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, layerMask);

        if (hit != false)
        {
            if (overlapObject == null)
            {
                if (selectObject != null)
                {
                    ParentObject parent = hit.collider.GetComponent<ParentObject>();
                    if (parent == null) return;
                }

                overlapObject = hit.collider.transform.GetComponent<SelectableObject>();
                overlapObject.DrawOutline(true);
            }
        }
        else
        {
            if (overlapObject != null)
            {
                if (overlapObject != selectObject)
                {
                    overlapObject.DrawOutline(false);
                }
                overlapObject = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hit != false)
            {
                if (selectObject == null)
                {
                    ChildObject child = hit.collider.GetComponent<ChildObject>();
                    if (child == null) return;

                    selectObject = hit.collider.GetComponent<SelectableObject>();
                    selectObject.DrawOutline(true);
                    return;
                }
                
                if (selectObject != null)
                {
                    ParentObject parent = hit.collider.GetComponent<ParentObject>();
                    if (parent == null) return;

                    if (hit.collider.transform.parent != null && hit.collider.transform.parent == selectObject.transform)
                    {
                        hit.collider.GetComponent<ISelectableObject>().SetParent(null);
                    }
                    selectObject.GetComponent<ISelectableObject>().SetParent(hit.collider.transform);
                    selectObject.DrawOutline(false);
                    hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
                    selectObject = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (hit != false && hit.collider.transform.parent != null)
            {
                hit.collider.transform.GetComponent<ISelectableObject>().SetParent(null);
                hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
            }
            else if (selectObject != null)
            {
                selectObject.DrawOutline(false);
                selectObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (isClear == false && Input.GetKeyDown(KeyCode.Space))
        {
            Stop(!isStop);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Stop(bool value)
    {
        isStop = value;
        if (value)
        {
            //Time.timeScale = 0;
            pauseImage.SetActive(false);
            playImage.SetActive(true);
        }
        else
        {
            //Time.timeScale = 1;
            playImage.SetActive(false);
            pauseImage.SetActive(true);
        }
    }

    private void OnClear()
    {
        clearUI.SetActive(true);
    }

    public void LoadNextStage()
    {
        SceneManager.LoadScene(exit.GetNextSceneName());
    }
}
