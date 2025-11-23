using UnityEngine;
using TMPro;

/// <summary>
/// HUD самолЄта: отображает параметры полЄта.
/// </summary>
/// 
public class AircraftHUDController : MonoBehaviour
{
    [SerializeField] private AircraftController _aircraft;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _altitudeText;
    [SerializeField] private TextMeshProUGUI _throttleText;
    [SerializeField] private RectTransform _horizonTransform;

    private void Update()
    {
        if (_aircraft == null) return;

        // „исловые значени€
        _speedText.text = $"SPD: {_aircraft.GetSpeed():000}";
        _altitudeText.text = $"ALT: {_aircraft.GetAltitude():0000}";
        _throttleText.text = $"THR: {_aircraft.GetThrottle():P0}";

        // »скусственный горизонт (по z Ч roll)
        _horizonTransform.rotation = Quaternion.Euler(0, 0, -_aircraft.transform.eulerAngles.z);
    }
}