using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private PlaceableObject obj;
    [SerializeField] private int quantity = 1;
    private TextMeshProUGUI quantityText;
    private Image objectImage;

    private SelectObject objectSelection;

    private void Awake() {
        objectSelection = transform.parent.GetComponentInParent<SelectObject>();

        objectImage = GetComponent<Image>();
        quantityText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        InitializeObject();
    }

    private void InitializeObject() {
        UpdateObjectSprite();
        UpdateObjectQuantity();
    }

    private void UpdateObjectSprite() {
        objectImage.sprite = obj.sprite;
    }

    private void UpdateObjectQuantity() {
        if (quantity > 1) {
            quantityText.enabled = true;
            quantityText.SetText(quantity.ToString());
        } else if (quantity == 1) {
            quantityText.enabled = false;
            quantityText.SetText("1");
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            objectSelection.SelectCurrentObject(gameObject);
            objectSelection.DeselectOtherObjects(gameObject);
        }
    }

    public PlaceableObject GetObject() {
        return obj;
    }
}
