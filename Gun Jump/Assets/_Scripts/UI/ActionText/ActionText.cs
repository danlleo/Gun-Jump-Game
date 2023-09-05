using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class ActionText : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProGUI;
    private CanvasGroup _canvasGroup;
    private Vector3 _worldTextPosition;

    private float _actionTextFadeOutTimeInSeconds = 1.25f;

    private void Awake()
    {
        _textMeshProGUI = GetComponent<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        StartCoroutine(ActionTextFadeOutRoutine());
    }

    private void Update()
    {
        ShowActionText();
    }

    public void Initialize(string actionText, Vector3 worldTextPosition)
    {
        _textMeshProGUI.text = actionText;
        _worldTextPosition = worldTextPosition;
    }

    private void ShowActionText()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_worldTextPosition);
        transform.position = screenPosition;
    }

    private IEnumerator ActionTextFadeOutRoutine()
    {
        float timer = _actionTextFadeOutTimeInSeconds;

        while (timer >= 0)
        {
            timer -= Time.deltaTime / _actionTextFadeOutTimeInSeconds;

            _canvasGroup.alpha = timer;

            yield return null;
        }

        Destroy(gameObject);
    }
}
