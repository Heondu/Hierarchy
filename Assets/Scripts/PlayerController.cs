using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private float time = 1;
    private new Rigidbody2D rigidbody;

    private Vector3 originPos;
    private Vector3 parentPos;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        originPos = transform.position;

        StartCoroutine("Move");
    }

    private void Update()
    {
        if (transform.parent != null)
            parentPos = transform.parent.GetComponent<Rigidbody2D>().velocity;
    }

    private IEnumerator Move()
    {
        while (true)
        {
            float percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime;

                Vector3 newPos = Vector3.Lerp(Vector3.zero, destination, percent);

                rigidbody.MovePosition(originPos + newPos + parentPos);

                yield return null;
            }

            yield return new WaitForSeconds(1f);

            while (percent > 0)
            {
                percent -= Time.deltaTime;

                Vector3 newPos = Vector3.Lerp(Vector3.zero, destination, percent);

                rigidbody.MovePosition(originPos + newPos + parentPos);

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
