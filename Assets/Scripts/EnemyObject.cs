using UnityEngine;

public class EnemyObject : SelectableObject, ISelectableObject, ILivingObject
{
    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.Restart();
        }
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
