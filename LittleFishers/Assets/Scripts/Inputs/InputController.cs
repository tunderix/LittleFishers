﻿using UnityEngine;
using System;
using LittleFishers.Fishing;

public class InputController : MonoBehaviour
{
    [SerializeField] private MouseControls mouseControls;
    [SerializeField] private FishingController fishingController;
    [SerializeField] private UnitSelection unitSelection;
    [SerializeField] private LittleFishersUI littleFishersUI;

    public static Action<GameObject> OnMouseLeftButtonClick;
    public static Action<GameObject, Vector3> OnMouseRightButtonClick;
    public static Action<SelectionBox> OnMouseDrag;

    public static void MouseLeftButtonClick(GameObject clickedGO)
    {
        if (OnMouseLeftButtonClick != null) OnMouseLeftButtonClick(clickedGO);
    }

    public static void MouseRightButtonClick(GameObject clickedGO, Vector3 point)
    {
        if (OnMouseRightButtonClick != null) OnMouseRightButtonClick(clickedGO, point);
    }

    public static void MouseDrag(SelectionBox box)
    {
        if (OnMouseDrag != null)
        {
            OnMouseDrag(box);
        }
    }

    void Awake()
    {
        this.fishingController = this.gameObject.GetComponent<FishingController>();
    }
    void Start()
    {
        OnMouseLeftButtonClick = _onLeftButtonClick;
        OnMouseRightButtonClick = _onRightButtonClick;
        OnMouseDrag = _onMouseDrag;
    }

    private void _onLeftButtonClick(GameObject clickedGO)
    {
        unitSelection.userIsDragging = mouseControls._userIsDragging;
        unitSelection.TrySelect(clickedGO);
    }

    //TODO - awfull
    private void _onRightButtonClick(GameObject clickedGO, Vector3 point)
    {
        unitSelection.userIsDragging = mouseControls._userIsDragging;
        GameObject player = GameObject.FindGameObjectWithTag("PlayerSelf");
        /*
        FishPool fishPool = clickedGO.GetComponent<FishPool>();
        FishingTarget fishingTarget = clickedGO.GetComponent<FishingTarget>();
        if (FishingHelper.canStartFishing(fishingController.throwDistance, player.transform.position, point, clickedGO) && fishPool)
        {
            // TODO - Fix This
            fishingController.StartFishing(point, fishPool, player.GetComponent<Player>());
        }
        else if (fishingTarget != null)
        {
            fishingTarget.TriggerFishOn();
        }
        else*/
        if (UnitSelection.IsSelected(player))
        {
            player.GetComponent<Player>().MoveTo(point);
        }

    }

    private void _onMouseDrag(SelectionBox box)
    {
        unitSelection.userIsDragging = mouseControls._userIsDragging;
        unitSelection.UpdateSelectionBox(box);
    }

    void Update()
    {
        Player player = GameObject.FindGameObjectWithTag("PlayerSelf").GetComponent<Player>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                fishingController.SetFishingIndicator(player, true);
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                fishingController.SetFishingIndicator(player, false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            littleFishersUI.TogglePlayerList();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            littleFishersUI.ToggleBackpack();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            littleFishersUI.ToggleMainMenu();
            littleFishersUI.HideKeybindLayout();
            littleFishersUI.HideShopLayout();
        }
    }
}
