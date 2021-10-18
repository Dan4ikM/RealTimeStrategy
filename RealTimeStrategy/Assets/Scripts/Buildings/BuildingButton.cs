using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Building building = null;
    [SerializeField] private Image iconImage = null;
    [SerializeField] private TMP_Text priceText = null;
    [SerializeField] private LayerMask floorMask = new LayerMask();

    private Camera mainCamera;
    private RTSPlayer player;
    private GameObject buildingPrieviewInstance;
    private Renderer buildingRendererInstance;

    private void Start()
    {
        mainCamera = Camera.main;

        iconImage.sprite = building.GetIcon();
        priceText.text = building.GetPrice().ToString();
    }

    private void Update()
    {
        if (buildingPrieviewInstance == null) { return; }

        UpdateBuildingPreview();
    }

    private void LateUpdate()
    {
        if (player == null && NetworkClient.connection.identity != null)
        {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left) { return; }

        buildingPrieviewInstance = Instantiate(building.GetBuildingPreview());
        buildingRendererInstance = buildingPrieviewInstance.GetComponentInChildren<Renderer>();

        buildingPrieviewInstance.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(buildingPrieviewInstance == null) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            //Place building
        }

        Destroy(buildingPrieviewInstance);
    }

    private void UpdateBuildingPreview()
    {
        Debug.Log(1);
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask)){ return; }

        buildingPrieviewInstance.transform.position = hit.point;

        if (!buildingPrieviewInstance.activeSelf)
        {
            buildingPrieviewInstance.SetActive(true);
        }
    }
}
