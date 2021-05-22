using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private SelectableObject selectObject = null;
    private SelectableObject overlapObject = null;

    private LayerMask layerMask;

    private bool isStop = true;
    public bool IsStop => isStop;

    private void Awake()
    {
        if (instance == false) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("SelectableObject");
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
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = !isStop;
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
