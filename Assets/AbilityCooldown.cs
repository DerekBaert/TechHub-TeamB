using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField, Tooltip("Cooldown length in seconds")]
    private float cooldownTime = 15f;

    [SerializeField, Tooltip("Assign your UI Slider in the Inspector")]
    private Slider cooldownSlider;

    private float lastUsedTime = -Mathf.Infinity;

    void Start()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.minValue = 0f;
            cooldownSlider.maxValue = cooldownTime;
            cooldownSlider.value = 0f; // 0 means ready
        }
    }

    void Update()
    {
        HandleStaminaRegeneration();
        UpdateCooldownUI();
    }

    private void HandleStaminaRegeneration()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanUse)
        {
            lastUsedTime = Time.time; // set before UseAbility to avoid reentrancy issues
            UseAbility();
        }
    }

    void UseAbility()
    {
        Debug.Log("Ability used!");
        // Your ability logic here
    }

    private void UpdateCooldownUI()
    {
        if (cooldownSlider == null) return;

        float remaining = GetRemainingCooldown();

        // Show remaining seconds on the slider (full = cooldownTime, empty = ready)
        cooldownSlider.value = remaining;
    }

    public float GetRemainingCooldown()
    {
        float remaining = (lastUsedTime + cooldownTime) - Time.time;
        return Mathf.Max(0f, remaining);
    }

    public bool CanUse => Time.time >= lastUsedTime + cooldownTime;
}