using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private PlaceableObject obj;
    public int quantity = 1;
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
        objectImage.sprite = obj.sprite;
        ChangeObjectQuantityBy(0);
    }

    public void ChangeObjectQuantityBy(int changeValueBy) {
        if (quantity == 0 && changeValueBy > 0) {
            objectSelection.AddUsedObject(gameObject);
        }

        quantity += changeValueBy;

        if (quantity > 1) {
            gameObject.SetActive(true);

            quantityText.enabled = true;
            quantityText.SetText(quantity.ToString());
        } else if (quantity == 1) {
            gameObject.SetActive(true);

            quantityText.enabled = false;
            quantityText.SetText("1");
        } else {
            quantity = 0;

            gameObject.SetActive(false);
            objectSelection.RemoveUsedObject(gameObject);
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
