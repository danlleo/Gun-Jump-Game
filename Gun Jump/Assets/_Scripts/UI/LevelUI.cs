using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelUI;
    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    [SerializeField] private Image _progressBarForeground;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private Transform _levelStartPosition;
    [SerializeField] private Transform _levelEndPosition;

    private void Awake()
        => Show();

    private void Update()
    {
        AdjustProgressBarBasedOnPlayerPosition();
    }

    private void OnEnable()
    {
        Economy.OnReceivedMoney += EarningManager_OnReceivedMoney;
    }

    private void OnDisable()
    {
        Economy.OnReceivedMoney -= EarningManager_OnReceivedMoney;
    }

    private void EarningManager_OnReceivedMoney(object sender, EarningManagerEventArgs e)
    {
        UpdateMoneyAmountText(e.CurrentMoneyAmount);
    }

    private void Show() => _levelUI.SetActive(true);

    private void Hide() => _levelUI.SetActive(false);

    private void UpdateMoneyAmountText(int amount)
        => _moneyAmountText.text = $"{amount}";

    private void AdjustProgressBarBasedOnPlayerPosition()
    {
        _progressBarForeground.fillAmount =
            Mathf.InverseLerp(_levelStartPosition.position.z, _levelEndPosition.position.z, _weapon.transform.position.z);
    }
}