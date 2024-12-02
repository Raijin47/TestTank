using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    #region Sub-Classes
    [System.Serializable]
    public class UIButtonEvent : UnityEvent<PointerEventData.InputButton> { }
    #endregion

    public bool Interactable = true;
    public UnityEvent OnClick;

    private RectTransform _rectTransform;
    private readonly Vector2 PressedSize = new(0.9f, 0.9f);
    private const float _resizeDuration = 0.2f;
    private Coroutine _resizeCoroutine;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();
    private void OnEnable() => _rectTransform.localScale = Vector2.one;
    private void OnDisable() => Release();

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!Interactable) return;

        Release();
        _resizeCoroutine = StartCoroutine(ResizeButton(PressedSize));
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!Interactable) return;

        Release();
        _resizeCoroutine = StartCoroutine(ResizeButton(Vector2.one));
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (!Interactable) return;

        OnClick?.Invoke();
    }

    private IEnumerator ResizeButton(Vector2 targetSize)
    {
        Vector2 initialSize = _rectTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < _resizeDuration)
        {
            _rectTransform.localScale = Vector2.Lerp(initialSize, targetSize, elapsedTime / _resizeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rectTransform.localScale = targetSize;
    }

    private void Release()
    {
        if (_resizeCoroutine != null)
        {
            StopCoroutine(_resizeCoroutine);
            _resizeCoroutine = null;
        }
    }
}