using UnityEngine;

public class PlayerObject : SelectableObject, ISelectableObject
{
    public void SetParent(Transform parent)
    {
        StopCoroutine("FixPosition");

        transform.SetParent(parent);
        parentObject = null;

        StartCoroutine("FixPosition");
        StartCoroutine("FixRotation");
    }

    //private void Update()
    //{
    //    Vector3 rotation = transform.InverseTransformVector(transform.rotation.eulerAngles);
    //
    //    if (rotation != Vector3.zero)
    //    {
    //        float angle = (rotation.z + 90 * GetDir().x) * Mathf.Deg2Rad;
    //        Vector3 newDir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    //
    //        Debug.Log(newDir);
    //        Debug.DrawLine(transform.position, transform.position + newDir);
    //    }
    //}

    public Vector3 GetDir()
    {
        if (transform.parent != null)
        {
            return (transform.parent.GetComponent<ISelectableObject>().GetDir()).normalized;
        }

        return Vector3.zero;
    }

    public Vector3 GetRotDir()
    {
        if (transform.parent != null)
        {
            return (transform.parent.GetComponent<ISelectableObject>().GetRotDir()).normalized;
        }

        return Vector3.zero;
    }
}
