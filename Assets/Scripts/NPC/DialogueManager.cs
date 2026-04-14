using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    public Button helloButton;
    public Button leaveButton;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Gọi khi bắt đầu nói chuyện
    public void StartTalk()
    {
        dialoguePanel.SetActive(true);

        npcNameText.text = "Ông Lão";
        dialogueText.text = "…";

        helloButton.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        helloButton.onClick.RemoveAllListeners();
        leaveButton.onClick.RemoveAllListeners();

        helloButton.onClick.AddListener(SayHello);
        leaveButton.onClick.AddListener(Leave);
    }

    void SayHello()
    {
        dialogueText.text = "Chào cậu, chúc cậu một ngày tốt lành.";
        helloButton.gameObject.SetActive(false);
    }

    void Leave()
    {
        dialoguePanel.SetActive(false);
    }
}
