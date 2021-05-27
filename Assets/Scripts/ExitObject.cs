using UnityEngine;
using UnityEngine.Events;

public class ExitObject : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    public UnityEvent onClear = new UnityEvent();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.IsClear = true;
            onClear.Invoke();
        }
    }

    public string GetNextSceneName()
    {
        return nextSceneName;
    }
}
