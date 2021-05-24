using UnityEngine;

public class ExitObject : MonoBehaviour
{
    [SerializeField]
    private GameObject clearUI;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.IsClear = true;
            clearUI.SetActive(true);
        }
    }
}
