using UnityEngine;
using UnityEngine.UI;

public class Recovery : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 10f;
    public float currentStamina;
    public float staminaDrainRate = 2f; // Stamina drained per second when running
    public float staminaRegenRate = 2f; // Stamina regenerated per second when not running
    public float regenDelay = 2f; // Seconds before regeneration starts after using stamina

    [Header("Healing Settings")]
    // These fields are kept for backwards compatibility when no external Health exists.
    public float maxHealth = 10f;
    public float currentHealth;
    public float healAmount = 2f;       // float-friendly heal amount used when no external Health
    public float healCooldown = 15f;
    public int maxHeals = 2;
    public KeyCode healKey = KeyCode.E;

    [Header("UI References")]
    public Slider staminaBar;
    public Slider healthBar;           // optional: only used when there's no external HealthBarUI controlling it
    public Text staminaText;
    public Text healCountText;
    public Button healButton;
    public Text healKeyText; // optional, displays the key

    [Header("External Integration (optional)")]
    public Health healthComponent;       // existing project Health (int-based)
    public HealthBarUI healthBarUI;      // existing HealthBarUI that already updates the slider

    // Internal state
    private int currentHeals;
    private bool isRunning;
    private float timeSinceLastStaminaUse = Mathf.Infinity;
    private float timeSinceLastHeal = Mathf.Infinity;

    // Helper to know whether to use external Health
    private bool UseExternalHealth => healthComponent != null;

    void Start()
    {
        // Initialize stamina and heals
        currentStamina = maxStamina;
        currentHeals = maxHeals;

        // If an external Health exists, sync local float fields for display but treat external as authoritative
        if (UseExternalHealth)
        {
            // sync local fields for UI fallbacks (if needed)
            currentHealth = healthComponent.currentHealth;
            maxHealth = healthComponent.maxHealth;

            // If there's an external HealthBarUI, prefer it; otherwise adjust the plain slider to external max
            if (healthBarUI == null && healthBar != null)
            {
                healthBar.maxValue = healthComponent.maxHealth;
                healthBar.minValue = 0f;
            }
        }
        else
        {
            // No external Health: ensure sliders use absolute values
            currentHealth = maxHealth;
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;
                healthBar.minValue = 0f;
            }
        }

        // Setup UI and button
        UpdateStaminaUI();
        UpdateHealthUI();
        UpdateHealUI();

            if (healButton != null)
            healButton.onClick.AddListener(UseHeal);

        if (healKeyText != null)
            healKeyText.text = $"Heal ({healKey})";
    }

    void Update()
    {
        HandleInput();
        UpdateStamina();
        UpdateHealCooldown();

        if (Input.GetKeyDown(healKey))
            UseHeal();
    }

    void HandleInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;
    }

    void UpdateStamina()
    {
        if (isRunning && currentStamina > 0f)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(0f, currentStamina);
            timeSinceLastStaminaUse = 0f;
        }
        else
        {
            timeSinceLastStaminaUse += Time.deltaTime;
            if (timeSinceLastStaminaUse >= regenDelay && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Min(maxStamina, currentStamina);
            }
        }

        UpdateStaminaUI();
    }

    void UpdateStaminaUI()
    {
        if (staminaBar != null)
            staminaBar.value = Mathf.Clamp(currentStamina, 0f, maxStamina);

        if (staminaText != null)
            staminaText.text = $"Stamina: {currentStamina:F0}/{maxStamina}";
    }

    void UpdateHealCooldown()
    {
        if (timeSinceLastHeal < healCooldown)
            timeSinceLastHeal += Time.deltaTime;

        UpdateHealUI();
    }

    public void UseHeal()
    {
        // If an external Health exists, use it (Health.TakeDamage accepts negative to heal)
        if (UseExternalHealth)
        {
            bool canHeal = currentHeals > 0 && timeSinceLastHeal >= healCooldown && healthComponent.currentHealth < healthComponent.maxHealth;
            if (!canHeal)
            {
                LogHealFail();
                return;
            }

            // Convert healAmount (float) to int for external Health (round up to ensure healing occurs)
            int healInt = Mathf.CeilToInt(healAmount);
            healthComponent.TakeDamage(-healInt); // pass negative to heal
            currentHeals--;
            timeSinceLastHeal = 0f;

            // Sync local float for any fallback UI
            currentHealth = healthComponent.currentHealth;
            UpdateHealUI();
            // Do not manually update HealthBarUI - it updates itself. If you're using plain healthBar, UpdateHealthUI will handle it.
            UpdateHealthUI();
            return;
        }

        // No external Health: use local float health
        if (currentHeals > 0 && timeSinceLastHeal >= healCooldown && currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
            currentHeals--;
            timeSinceLastHeal = 0f;

            UpdateHealthUI();
            UpdateHealUI();
        }
        else
        {
            LogHealFail();
        }
    }

    void LogHealFail()
    {
        if (currentHeals <= 0)
            Debug.Log("No heals remaining!");
        else if (timeSinceLastHeal < healCooldown)
            Debug.Log($"Heal on cooldown! {healCooldown - timeSinceLastHeal:F1}s remaining");
        else
            Debug.Log("Already at full health!");
    }

    void UpdateHealthUI()
    {
        if (UseExternalHealth)
        {
            // external Health is authoritative; if there's no HealthBarUI but a plain slider, update it
            if (healthBarUI == null && healthBar != null)
            {
                healthBar.value = Mathf.Clamp(healthComponent.currentHealth, 0, healthComponent.maxHealth);
            }
            // If healthBarUI exists it will update the slider itself every frame (no action needed here)
        }
        else
        {
            if (healthBar != null)
                healthBar.value = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
    }

    void UpdateHealUI()
    {
        if (healCountText != null)
        {
            healCountText.text = $"Heals: {currentHeals}/{maxHeals}";
            if (timeSinceLastHeal < healCooldown && currentHeals > 0)
                healCountText.text += $"\nCooldown: {healCooldown - timeSinceLastHeal:F1}s";
        }

        if (healButton != null)
        {
            bool canHeal = currentHeals > 0 && timeSinceLastHeal >= healCooldown;
            if (UseExternalHealth)
                canHeal &= (healthComponent.currentHealth < healthComponent.maxHealth);
            else
                canHeal &= (currentHealth < maxHealth);

            healButton.interactable = canHeal;
        }
    }

    // Public methods to modify values from other scripts
    public void ModifyStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        UpdateStaminaUI();
    }

    public void AddHeal()
    {
        currentHeals = Mathf.Min(maxHeals, currentHeals + 1);
        UpdateHealUI();
    }

    // Properties for other scripts to access
    public bool IsRunning => isRunning;
    public bool CanRun => currentStamina > 0f;
    public float StaminaPercentage => currentStamina / maxStamina;
}
