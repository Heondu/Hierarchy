using System.Collections;
using UnityEngine;

public class RotatingObject : SelectableObject, ISelectableObject
{
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private float endAngle;
    private int sign;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool repeat = false;

    private SpriteRenderer icon;

    private void Start()
    {
        icon = transform.Find("Icon").GetComponent<SpriteRenderer>();

        sign = (int)Mathf.Sign(endAngle - startAngle);
        dir = new Vector3(-sign, 0, 0).normalized;

        StartCoroutine("Rotate");
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
            return (transform.parent.GetComponent<ISelectableObject>().GetDir()).normalized;
        }

        return Vector3.zero;
    }

    public Vector3 GetRotDir()
    {
        if (transform.parent != null)
        {
            return (dir + transform.parent.GetComponent<ISelectableObject>().GetRotDir()).normalized;
        }

        return dir;
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            if (repeat)
            {
                transform.Rotate(new Vector3(0, 0, sign) * speed * Time.deltaTime, Space.Self);

                yield return null;
            }
            else
            {
                if (GameController.instance.IsStop == false) icon.flipX = false;
                dir = new Vector3(-sign, 0, 0).normalized;
                float current = 0;
                while (current < Mathf.Abs(endAngle - startAngle))
                {
                    if (GameController.instance.IsStop == false)
                    {
                        current += 1 * speed * Time.deltaTime;

                        transform.Rotate(new Vector3(0, 0, sign) * speed * Time.deltaTime, Space.Self);
                    }

                    yield return null;
                }

                dir = Vector3.zero;
                StartCoroutine("FixRotation");
                yield return new WaitForSeconds(1f);

                if (GameController.instance.IsStop == false) icon.flipX = true;
                dir = new Vector3(sign, 0, 0).normalized;
                while (current > 0)
                {
                    if (GameController.instance.IsStop == false)
                    {
                        current -= 1 * speed * Time.deltaTime;

                        transform.Rotate(new Vector3(0, 0, -sign) * speed * Time.deltaTime, Space.Self);
                    }

                    yield return null;
                }

                dir = Vector3.zero;
                StartCoroutine("FixRotation");
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
