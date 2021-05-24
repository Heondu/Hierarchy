using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    [SerializeField]
    private float rotateTime = 0.5f;
    [SerializeField]
    private int rotateDir = 1;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateDir * rotateTime * Time.deltaTime));
    }
}
