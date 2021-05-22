using UnityEngine;
using UnityEngine.SceneManagement;

public class SetParent : MonoBehaviour
{
    private SelectableObject firstSelectTransfom = null;
    private SelectableObject overlapObject = null;


    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, 1 << LayerMask.NameToLayer("ParentObject"));

        if (hit != false)
        {
            if (overlapObject == null)
            {
                overlapObject = hit.collider.transform.GetComponent<SelectableObject>();
                overlapObject.DrawOutline(true);
            }
        }
        else
        {
            if (overlapObject != null)
            {
                if (overlapObject != firstSelectTransfom)
                {
                    overlapObject.DrawOutline(false);
                }
                overlapObject = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, 1 << LayerMask.NameToLayer("ParentObject"));

            if (hit != false)
            {
                //if (firstSelectTransfom == null)
                //{
                //    firstSelectTransfom = hit.collider.transform.GetComponent<SelectableObject>();
                //    firstSelectTransfom.DrawOutline(true);
                //    return;
                //}
                //
                //if (firstSelectTransfom.transform.parent == null || hit.collider.transform != firstSelectTransfom.transform.parent)
                //{
                //    firstSelectTransfom.GetComponent<ISelectableObject>().SetParent(hit.collider.transform);
                //    firstSelectTransfom.DrawOutline(false);
                //    hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
                //    firstSelectTransfom = null;
                //}

                //if (hit.collider.transform != transform.root)
                //{
                //    transform.root.GetComponent<ISelectableObject>().SetParent(hit.collider.transform);
                //    hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
                //}

                transform.GetComponent<ISelectableObject>().SetParent(hit.collider.transform);
                hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, 1 << LayerMask.NameToLayer("ParentObject"));

            //if (hit != false && hit.collider.transform.parent != null)
            //{
            //    hit.collider.transform.GetComponent<ISelectableObject>().SetParent(null);
            //    hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
            //}
            //else if (firstSelectTransfom != null)
            //{
            //    firstSelectTransfom.DrawOutline(false);
            //    firstSelectTransfom = null;
            //}

            if (hit != false && hit.collider.transform.childCount > 1)
            {
                hit.collider.transform.GetChild(1).GetComponent<ISelectableObject>().SetParent(null);
                hit.collider.transform.GetComponent<SelectableObject>().DrawOutline(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
