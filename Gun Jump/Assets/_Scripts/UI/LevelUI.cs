using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelUI;
    [SerializeField] private GameObject _progressBarUIGameObject;
    [SerializeField] private GameObject _gameOverUIGameObject;
    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    [SerializeField] private Image _progressBarForeground;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private Transform _levelStartPosition;
    [SerializeField] private Transform _levelEndPosition;

    [SerializeField] private Button _continueButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void Update()
    {
        AdjustProgressBarBasedOnPlayerPosition();
    }

    private void OnEnable()
    {
        Economy.OnReceivedMoney += EarningManager_OnReceivedMoney;
        GameEndedStaticEvent.OnGameEnded += GameEndedStaticEvent_OnGameEnded;
    }

    private void OnDisable()
    {
        Economy.OnReceivedMoney -= EarningManager_OnReceivedMoney;
        GameEndedStaticEvent.OnGameEnded -= GameEndedStaticEvent_OnGameEnded;
    }

    private void GameEndedStaticEvent_OnGameEnded()
    {
        _progressBarUIGameObject.SetActive(false);
        _gameOverUIGameObject.SetActive(true);
    }

    private void EarningManager_OnReceivedMoney(object sender, EarningManagerEventArgs e)
    {
        UpdateMoneyAmountText(e.CurrentMoneyAmount);
    }

    private void UpdateMoneyAmountText(int amount)
        => _moneyAmountText.text = $"{amount}";

    private void AdjustProgressBarBasedOnPlayerPosition()
    {
        _progressBarForeground.fillAmount =
            Mathf.InverseLerp(_levelStartPosition.position.z, _levelEndPosition.position.z, _weapon.transform.position.z);
    }
}