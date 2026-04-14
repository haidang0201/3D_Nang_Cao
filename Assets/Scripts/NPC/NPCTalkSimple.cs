using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCTalkSimple : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
    public Button helloButton;
    public Button leaveButton;

    private bool playerInRange = false;

    void Start()
    {
        dialoguePanel.SetActive(false);

        helloButton.onClick.AddListener(SayHello);
        leaveButton.onClick.AddListener(CloseDialogue);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }
    }

    void OpenDialogue()
    {
        dialoguePanel.SetActive(true);
        npcNameText.text = "NPC";
        dialogueText.text = "...";
    }

    void SayHello()
    {
        dialogueText.text = "Chào bạn, chúc bạn một ngày tốt lành!";
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CloseDialogue();
        }
    }
}
