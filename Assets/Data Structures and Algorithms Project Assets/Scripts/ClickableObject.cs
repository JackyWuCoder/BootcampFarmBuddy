using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _clickColor;

    public UnityEvent OnClicked;
    public UnityEvent OnRightClicked;
    public UnityEvent OnHovered;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer= GetComponent<SpriteRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.color = _selectColor;
        TileOutline._instance.MoveOutline(transform.position);

        OnHovered?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.color = _defaultColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _renderer.color = _clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _renderer.color = _selectColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            OnClicked?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClicked?.Invoke();

    }





}
