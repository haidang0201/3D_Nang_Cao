using UnityEngine;
using TMPro;

public class NPCDialogue3D : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text pressEText;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogueLines;
    private int currentLine = 0;

    private bool playerInRange = false;
    private bool isTalking = false;

    void Start()
    {
        Debug.Log("NPCDialogue3D START");

        if (dialoguePanel == null)
            Debug.LogError("❌ dialoguePanel CHƯA GÁN");

        if (dialogueText == null)
            Debug.LogError("❌ dialogueText CHƯA GÁN");

        if (pressEText == null)
            Debug.LogError("❌ pressEText CHƯA GÁN");

        if (dialogueLines == null || dialogueLines.Length == 0)
            Debug.LogError("❌ dialogueLines TRỐNG");

        dialoguePanel.SetActive(false);
        pressEText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;

        Debug.Log("🟢 Player đang trong vùng NPC");

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🟡 ĐÃ NHẤN PHÍM E");

            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        Debug.Log("▶ BẮT ĐẦU NÓI CHUYỆN");

        isTalking = true;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        pressEText.gameObject.SetActive(false);

        dialogueText.text = dialogueLines[currentLine];
    }

    void NextLine()
    {
        currentLine++;

        Debug.Log("➡ NEXT LINE: " + currentLine);

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    void EndDialogue()
    {
        Debug.Log("⛔ KẾT THÚC HỘI THOẠI");

        isTalking = false;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("⚪ OnTriggerEnter với: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ PLAYER ĐÃ VÀO VÙNG NPC");

            playerInRange = true;
            pressEText.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("❌ KHÔNG PHẢI PLAYER");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("🔴 OnTriggerExit: " + other.name);

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            isTalking = false;

            dialoguePanel.SetActive(false);
            pressEText.gameObject.SetActive(false);
        }
    }
}
