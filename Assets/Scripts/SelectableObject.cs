using System.Collections;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private GameObject outline;

    protected ISelectableObject parentObject;
    private Transform parentTransform;

    protected Vector3 dir = Vector3.zero;

    private void Awake()
    {
        outline = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            parentObject = transform.parent.GetComponent<ISelectableObject>();
            parentTransform = transform.parent;

            if (CheckWall(parentObject.GetDir()))
            {
                GetComponent<ISelectableObject>().SetParent(null);
            }
        }
        else if (parentObject != null)
        {
            if (CheckWall(parentObject.GetDir()) == false)
            {
                GetComponent<ISelectableObject>().SetParent(parentTransform);
            }
        }
    }

    public bool CheckWall(Vector3 dir)
    {
        Vector3 rotation = transform.InverseTransformVector(transform.rotation.eulerAngles);
        
        if (rotation != Vector3.zero && parentObject != null)
        {
            float angle = (rotation.z + 90 * parentObject.GetRotDir().x) * Mathf.Deg2Rad;
            dir = (dir + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)).normalized;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, 1 << LayerMask.NameToLayer("Wall"));

        Debug.DrawLine(transform.position, transform.position + dir);

        if (hit != false)
        {
            return true;
        }

        return false;
    }

    public IEnumerator FixPosition()
    {
        Vector3 start = transform.localPosition;
        Vector3 end = Vector3Int.RoundToInt(transform.localPosition);

        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 10;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    public IEnumerator FixRotation()
    {
        Vector3 start = transform.localRotation.eulerAngles;
        Vector3 end = new Vector3(0, 0, Mathf.RoundToInt(transform.localRotation.eulerAngles.z / 90) * 90);

        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 10;

            transform.localRotation = Quaternion.Euler(Vector3.Lerp(start, end, percent));

            yield return null;
        }
    }

    public void DrawOutline(bool value)
    {
        outline.SetActive(value);
    }
}
