using System.Collections;
using UnityEngine;

public class MovingObjecct : SelectableObject, ISelectableObject
{
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private float speed;

    private Vector3 currentPos;
    private Vector3 currentMovePos;
    private float distance;

    private void Start()
    {
        currentPos = transform.position;
        distance = Vector3.Distance(currentPos, currentPos + destination);

        StartCoroutine("Move");
    }

    public void SetParent(Transform parent)
    {
        StopCoroutine("FixPosition");

        transform.SetParent(parent);

        currentPos = Vector3Int.RoundToInt(transform.localPosition);

        StartCoroutine("FixPosition");
        StartCoroutine("FixRotation");
    }

    public Vector3 GetDir()
    {
        if (transform.parent != null)
        {
            return (dir + transform.parent.GetComponent<ISelectableObject>().GetDir()).normalized;
        }

        return dir;
    }

    public Vector3 GetRotDir()
    {
        if (transform.parent != null)
        {
            return (transform.parent.GetComponent<ISelectableObject>().GetRotDir()).normalized;
        }

        return Vector3.zero;
    }

    private IEnumerator Move()
    {
        while (true)
        {
            int index = 0;

            dir = destination.normalized;
            for ( ; index < distance; index++)
            {
                if (CheckWall(dir)) break;

                yield return StartCoroutine(MoveTo(Vector3.zero, dir));
            }

            dir = Vector3.zero;
            yield return new WaitForSeconds(1f);

            dir = -destination.normalized;
            for ( ; index > 0; index--)
            {
                if (CheckWall(dir)) break;

                yield return StartCoroutine(MoveTo(Vector3.zero, dir));
            }

            dir = Vector3.zero;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator MoveTo(Vector3 start, Vector3 end)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;

            currentMovePos = Vector3.Lerp(start, end, percent);
            transform.localPosition = currentPos + currentMovePos;

            yield return null;
        }
        currentPos += currentMovePos;
    }
}
