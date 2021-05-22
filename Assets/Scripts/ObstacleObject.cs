using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : SelectableObject, ISelectableObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ILivingObject obj = collision.GetComponent<ILivingObject>();
        if (obj != null) obj.TakeDamage();
    }

    public void SetParent(Transform parent, bool isKeepParentTransform = false)
    {
        StopCoroutine("FixPosition");

        transform.SetParent(parent);
        if (isKeepParentTransform == false)
            parentObject = null;

        StartCoroutine("FixPosition");
        StartCoroutine("FixRotation");
    }

    public Vector3 GetDir()
    {
        if (transform.parent != null)
        {
            return transform.parent.GetComponent<ISelectableObject>().GetDir().normalized;
        }

        return Vector3.zero;
    }

    public Vector3 GetRotDir()
    {
        if (transform.parent != null)
        {
            return transform.parent.GetComponent<ISelectableObject>().GetRotDir().normalized;
        }

        return Vector3.zero;
    }
}
