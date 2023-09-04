using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform _targetCoinMoveTransform;
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
		StartCoroutine(EndGameUIAnimationRoutine(Economy.CurrentLevelMoneyAmount, 100));
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

        float verticalOffset = 80f;

        for (int i = 0; i < amountCoinIconsToSpawn; i++)
        {
            GameObject coinIcon = Instantiate(_coinIconPrefab, _coinIconGroupTransform);
            Vector2 randomPosition = Random.insideUnitCircle * verticalOffset + Vector2.up  * _coinIconGroupTransform.localPosition;

            coinIcon.transform.localPosition = randomPosition;
        }
    }

    private IEnumerator EndGameUIAnimationRoutine(int moneyAmountReceivedFromCurrentLevel, int multiplier)
    {
        yield return new WaitForSeconds(.215f);

        int multipliedResult = moneyAmountReceivedFromCurrentLevel * multiplier;

        float interpolateMultipliedResultDuration = .25f;
        float interpolateMultipliedResultTimer = 0f;

        while (interpolateMultipliedResultTimer < interpolateMultipliedResultDuration)
        {
            interpolateMultipliedResultTimer += Time.deltaTime;
            float t = interpolateMultipliedResultTimer / interpolateMultipliedResultDuration;

            _moneyAmountReceivedFromCurrentLevelText.text = $"{Mathf.RoundToInt(Mathf.Lerp(moneyAmountReceivedFromCurrentLevel, multipliedResult, t))}";

            yield return null;
        }

        InstantiateCoinPrefabsAtRandomPositions(7);

        yield return new WaitForSeconds(.15f);

        // Move Coins To Target Position
        float delayToMoveNextCoinIcon = .15f;

        List<Transform> coinIconList = new();

        foreach (Transform child in _coinIconGroupTransform)
            coinIconList.Add(child);
        
		int iterationIndex = 0;
		
        foreach (Transform coinIcon in coinIconList)
        {
            StartCoroutine(MoveCoinIconRoutine(coinIcon, _targetCoinMoveTransform.position));
            yield return new WaitForSeconds(delayToMoveNextCoinIcon);
			
			if (iterationIndex == 0)
				StartCoroutine(InterpolateTotalMoneyAmountRoutine(moneyAmountReceivedFromCurrentLevel, multiplier));
			
			iterationIndex++;
        }
		
		coinIconList.Clear();
	}
	
	private IEnumerator InterpolateTotalMoneyAmountRoutine(int moneyAmountReceivedFromCurrentLevel, int multiplier)
	{
		float interpolateMoneyAmountTextDuration = 1f;
		float interpolateMoneyAmountTextTimer = 0f;

        int moneyAmountResult = Economy.TotalMoneyAmountBeforeBeatingLevel + moneyAmountReceivedFromCurrentLevel * multiplier;
		
		while (interpolateMoneyAmountTextTimer < interpolateMoneyAmountTextDuration)
		{
			interpolateMoneyAmountTextTimer += Time.deltaTime;
			float t = interpolateMoneyAmountTextTimer / interpolateMoneyAmountTextDuration;
			
			_moneyAmountText.text = $"{Mathf.RoundToInt(Mathf.Lerp(Economy.TotalMoneyAmountBeforeBeatingLevel, moneyAmountResult, t))}";
			
			yield return null;
		}
	}

    private IEnumerator MoveCoinIconRoutine(Transform coinIcon, Vector3 targetPosition)
    {
        float interpolateCoinPositionDuration = .5f;
        float timer = 0f;

        Vector3 startPosition = coinIcon.position;

        while (timer < interpolateCoinPositionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / interpolateCoinPositionDuration;

            coinIcon.position = Vector2.Lerp(startPosition, targetPosition, t);
            coinIcon.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.zero, t);

            yield return null;
        }

        VibrationController.TriggerVibration();
        Destroy(coinIcon.gameObject);
    }
}