using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class DebugPanelView : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField spawnRateInput;
    [SerializeField] private TMP_InputField maxEnemiesInput;
    [SerializeField] private TMP_InputField bulletFireRateInput;
    [SerializeField] private TMP_InputField grenadeFireRateInput;
    [SerializeField] private TMP_InputField bulletsPerShotInput;
    [SerializeField] private TMP_InputField grenadesPerShotInput;

    [Header("Buttons")]
    [SerializeField] private Button applyButton;

    private RuntimeCombatSettings _runtimeSettings;

    [Inject]
    public void Construct(RuntimeCombatSettings runtimeSettings)
    {
        _runtimeSettings = runtimeSettings;
    }

    private void Start()
    {
        if (_runtimeSettings == null)
            return;

        RefreshView();

        if (applyButton != null)
            applyButton.onClick.AddListener(ApplyValues);
    }

    private void OnDestroy()
    {
        if (applyButton != null)
            applyButton.onClick.RemoveListener(ApplyValues);
    }

    private void RefreshView()
    {
        spawnRateInput.SetTextWithoutNotify(_runtimeSettings.EnemySpawnInterval.ToString("0.###"));
        maxEnemiesInput.SetTextWithoutNotify(_runtimeSettings.MaxEnemies.ToString());
        bulletFireRateInput.SetTextWithoutNotify(_runtimeSettings.BulletFireInterval.ToString("0.###"));
        grenadeFireRateInput.SetTextWithoutNotify(_runtimeSettings.GrenadeFireInterval.ToString("0.###"));
        bulletsPerShotInput.SetTextWithoutNotify(_runtimeSettings.BulletsPerShot.ToString());
        grenadesPerShotInput.SetTextWithoutNotify(_runtimeSettings.GrenadesPerShot.ToString());
    }

    private void ApplyValues()
    {
        if (_runtimeSettings == null)
            return;

        _runtimeSettings.EnemySpawnInterval = ParseFloat(spawnRateInput, _runtimeSettings.EnemySpawnInterval, 0.01f);
        _runtimeSettings.MaxEnemies = ParseInt(maxEnemiesInput, _runtimeSettings.MaxEnemies, 1);
        _runtimeSettings.BulletFireInterval = ParseFloat(bulletFireRateInput, _runtimeSettings.BulletFireInterval, 0.01f);
        _runtimeSettings.GrenadeFireInterval = ParseFloat(grenadeFireRateInput, _runtimeSettings.GrenadeFireInterval, 0.01f);
        _runtimeSettings.BulletsPerShot = ParseInt(bulletsPerShotInput, _runtimeSettings.BulletsPerShot, 1);
        _runtimeSettings.GrenadesPerShot = ParseInt(grenadesPerShotInput, _runtimeSettings.GrenadesPerShot, 1);

        RefreshView();
    }
    
    private static float ParseFloat(TMP_InputField input, float fallback, float minValue)
    {
        if (input == null)
            return fallback;
        string raw = input.text;
        if (string.IsNullOrWhiteSpace(raw))
            return fallback;
        raw = raw.Trim().Replace(',', '.');
        if (!float.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
            return fallback;
        return Mathf.Max(minValue, value);
    }

    private static int ParseInt(TMP_InputField input, int fallback, int minValue)
    {
        if (input == null)
            return fallback;

        if (!int.TryParse(input.text, out int value))
            return fallback;

        return Mathf.Max(minValue, value);
    }
}