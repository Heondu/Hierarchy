using UnityEngine;

public class ExitObject : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.LoadScene(nextSceneName);
        }
    }
}
