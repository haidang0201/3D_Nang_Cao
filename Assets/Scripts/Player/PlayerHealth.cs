using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] AudioClip deathClip = null;

    [Header("Script References")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAttack playerAttack;

    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;

    [Header("UI")]
    [SerializeField] FlashFade damageImage;
    [SerializeField] Slider healthSlider;

    [Header("Health UI Effect")]
    [SerializeField] float smoothSpeed = 5f; // 👉 tốc độ mượt

    [Header("Debugging Properties")]
    [SerializeField] bool isInvulnerable = false;

    int currentHealth;
    float targetHealthPercent; // 👉 dùng cho smooth

    void Reset()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Awake()
    {
        currentHealth = maxHealth;
        targetHealthPercent = 1f;

        UpdateHealthUI(true);
    }

    void Update()
    {
        // 👉 Làm thanh máu mượt
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Lerp(
                healthSlider.value,
                targetHealthPercent,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive())
            return;

        if (!isInvulnerable)
            currentHealth -= amount;

        // 👉 Clamp máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (damageImage != null)
            damageImage.Flash();

        UpdateHealthUI(false);

        Debug.Log("Player Health: " + currentHealth + "/" + maxHealth);

        if (!IsAlive())
        {
            if (playerMovement != null)
                playerMovement.Defeated();

            if (playerAttack != null)
                playerAttack.Defeated();

            if (animator != null)
                animator.SetTrigger("Die");

            if (audioSource != null && deathClip != null)
                audioSource.clip = deathClip;

            GameManager.Instance.PlayerDied();

            Invoke("LoadGameOverScene", 2f);
        }

        if (audioSource != null)
            audioSource.Play();
    }

    // 👉 HÀM UPDATE UI CHUYÊN NGHIỆP
    void UpdateHealthUI(bool instant)
    {
        float percent = currentHealth / (float)maxHealth;
        targetHealthPercent = percent;

        if (healthSlider != null)
        {
            if (instant)
                healthSlider.value = percent;

            // 👉 Đổi màu theo máu
            Image fill = healthSlider.fillRect.GetComponent<Image>();
            if (fill != null)
            {
                fill.color = Color.Lerp(Color.red, Color.green, percent);
            }
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    void DeathComplete()
    {
        if (GameManager.Instance.Player == this)
            GameManager.Instance.PlayerDeathComplete();
    }

    void LoadGameOverScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }
}