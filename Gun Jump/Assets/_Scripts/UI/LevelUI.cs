using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class LevelUI : MonoBehaviour
{
    [Space(10)]
    [Header("References To UI Objects")]
    [Space(5)]

    [SerializeField] private GameObject _levelUI;
    [SerializeField] private GameObject _progressBarUIGameObject;
    [SerializeField] private GameObject _gameOverUIGameObject;
    [SerializeField] private GameObject _coinIconPrefab;
    [SerializeField] private GameObject _moneyMultipliedGameObject;
    [SerializeField] private Transform _coinIconGroupTransform;
    [SerializeField] private Image _progressBarForeground;
    [SerializeField] private Button _continueButton;

    [Space(10)]
    [Header("References To UI Text Objects")]
    [Space(5)]

    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    [SerializeField] private TextMeshProUGUI _moneyAmountReceivedFromCurrentLevelText;
    [SerializeField] private TextMeshProUGUI _moneyMultiplierText;

    [Space(10)]
    [Header("References To External Objects")]
    [Space(5)]

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _levelStartPosition;
    [SerializeField] private Transform _levelEndPosition;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void Start()
        => UpdateMoneyAmountText(Economy.TotalMoneyAmount);

    private void Update()
    {
        AdjustProgressBarBasedOnPlayerPosition();
    }

    private void OnEnable()
    {
        GameEndedStaticEvent.OnGameEnded += GameEndedStaticEvent_OnGameEnded;
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
        InstantiateCoinPrefabsAtRandomPositions(7);
    }

    private void OnDisable()
    {
        GameEndedStaticEvent.OnGameEnded -= GameEndedStaticEvent_OnGameEnded;
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void GameEndedStaticEvent_OnGameEnded()
    {
        _progressBarUIGameObject.SetActive(false);
        _gameOverUIGameObject.SetActive(true);
        UpdateMoneyAmountReceivedFromCurrentLevelText();
    }

    private void UpdateMoneyAmountText(int amount)
        => _moneyAmountText.text = $"{amount}";

    private void UpdateMoneyMultipliedText(int multiplierAmount)
        => _moneyMultiplierText.text = $"X{multiplierAmount}";

    private void UpdateMoneyAmountReceivedFromCurrentLevelText()
    {
        if (Economy.CurrentLevelMoneyAmount == 0)
        {
            _moneyAmountReceivedFromCurrentLevelText.text = $"{Economy.DefaultMoneyAmountToAdd}";
            return;
        }

        _moneyAmountReceivedFromCurrentLevelText.text = $"{Economy.CurrentLevelMoneyAmount}";
    }

    private void AdjustProgressBarBasedOnPlayerPosition()
    {
        _progressBarForeground.fillAmount =
            Mathf.InverseLerp(_levelStartPosition.position.z, _levelEndPosition.position.z, _weapon.transform.position.z);
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs projectileHitScoreCubeEventArgs)
    {
        UpdateMoneyMultipliedText(projectileHitScoreCubeEventArgs.MoneyMultiplierAmount);
    }

    private void InstantiateCoinPrefabsAtRandomPositions(int amountCoinIconsToSpawn)
    {
        if (amountCoinIconsToSpawn < 0)
            throw new ArgumentException("AmountCoinIconsToSpawn value can not be less than zero");

        for (int i = 0; i < amountCoinIconsToSpawn; i++)
        {
            GameObject coinIcon = Instantiate(_coinIconPrefab, _coinIconGroupTransform);
            Vector2 randomPosition = _coinIconGroupTransform.position * Random.insideUnitCircle * .1f;

            coinIcon.transform.localPosition = randomPosition;
        }
    }
}