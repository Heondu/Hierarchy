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
        text.text = "노란색 오브젝트를 초록색 오브젝트 위치로 이동시켜보세요.\n" +
                    "오브젝트들을 클릭하여 부모-자식 관계를 형성할 수 있습니다.";
    }

    private void Update()
    {
        if (selectFlag == false && GameController.instance.selectObject != null)
        {
            selectFlag = true;
            text.text = "먼저 노란색 오브젝트를 클릭하고 파란색 오브젝트를 클릭해보세요.\n" +
                        "자식 오브젝트를 우클림함으로써 부모-자식 관계를 해제할 수 있고 잘못 선택했을 경우 우클릭으로 해제할 수 있습니다.";
        }
        else if (startFlag == false && FindObjectOfType<PlayerObject>().transform.parent != null)
        {
            startFlag = true;
            text.text = "스페이스 바 혹은 상단의 재생 버튼을 눌러 게임을 진행시킬 수 있습니다.\n" +
                        "혹여나 게임 진행 중 막히는 경우가 발생했을 때는 R키를 눌러 재시작할 수 있습니다.";
        }
    }
}
