using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private bool selectFlag = false;
    private bool startFlag = false;

    private void Start()
    {
        text.text = "����� ������Ʈ�� �ʷϻ� ������Ʈ ��ġ�� �̵����Ѻ�����.\n" +
                    "������Ʈ���� Ŭ���Ͽ� �θ�-�ڽ� ���踦 ������ �� �ֽ��ϴ�.";
    }

    private void Update()
    {
        if (selectFlag == false && GameController.instance.selectObject != null)
        {
            selectFlag = true;
            text.text = "���� ����� ������Ʈ�� Ŭ���ϰ� �Ķ��� ������Ʈ�� Ŭ���غ�����.\n" +
                        "�ڽ� ������Ʈ�� ��Ŭ�������ν� �θ�-�ڽ� ���踦 ������ �� �ְ� �߸� �������� ��� ��Ŭ������ ������ �� �ֽ��ϴ�.";
        }
        else if (startFlag == false && FindObjectOfType<PlayerObject>().transform.parent != null)
        {
            startFlag = true;
            text.text = "�����̽� �� Ȥ�� ����� ��� ��ư�� ���� ������ �����ų �� �ֽ��ϴ�.\n" +
                        "Ȥ���� ���� ���� �� ������ ��찡 �߻����� ���� RŰ�� ���� ������� �� �ֽ��ϴ�.";
        }
    }
}
