using System.Collections.Generic;
using UnityEngine;

// UnitSelection
/// <summary>
/// MonoBehaviour that handles selection of units and projection of selection box 
/// Input should be coming from InputController.
/// </summary>
public class UnitSelection : MonoBehaviour
{
    /// <value>A list of selectable units</value>
    [SerializeField] List<Selectable> unitSelections;

    /// <value>Is user dragging the mouse</value>
    [SerializeField] private bool userIsDragging;

    /// <value>Projector for showing which units are selected</value>
    [SerializeField] Projector selectionProjector;

    void Update()
    {
        // TODO - Get Mouse ups and downs from MouseController
        if (Input.GetMouseButton(0))
        {
            selectionProjector.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectionProjector.enabled = false;
        }
    }

    /// <summary>
    /// Update selection projector and units selections based on mouse input
    /// </summary>
    /// <param name="box">Box containing world coordinates for selection through mouse input</param>
    public void UpdateSelectionBox(SelectionBox box)
    {
        UpdateSelectionProjector(box);
        UpdateUnitSelections(box);
    }

    /// <summary>
    /// Select colliders inside provided box
    /// </summary>
    /// <param name="box">Box provides attributes for Physics OverlapBox to determine overlapping colliders</param>
    private void UpdateUnitSelections(SelectionBox box)
    {
        Collider[] overlappingObjects = Physics.OverlapBox(box.Center, box.Extents, Quaternion.identity);
        foreach (Collider overlappingObject in overlappingObjects)
        {
            Select(overlappingObject.gameObject);
        }
    }

    /// <summary>
    /// Update selection projector position, aspect ratio and ortographic size based on box provided
    /// </summary>
    /// <param name="box">Box provides projector position, aspect ratio and ortographic size</param>
    private void UpdateSelectionProjector(SelectionBox box)
    {
        selectionProjector.aspectRatio = box.Size.x / box.Size.z;
        selectionProjector.orthographicSize = box.Size.z * 0.5f;
        selectionProjector.transform.position = box.Center;
    }

    /// <summary>
    /// Unselects all selections and tries to select provided gameobjct
    /// </summary>
    /// <param name="clickedGO">Check if gameobject is selectable and select if possible</param>
    public void TrySelect(GameObject clickedGO)
    {
        UnselectAll();
        Select(clickedGO);
    }

    /// <summary>
    /// Make sure unitSelections exists and iterate through all selectables
    /// Set all selectables to false
    /// Clear unitSelections
    /// </summary>
    private void UnselectAll()
    {
        // Unselect selected objects
        if (unitSelections != null)
        {
            foreach (Selectable _selectable in unitSelections)
            {
                _selectable.IsSelected = false;
            }
        }
        unitSelections.Clear();
    }

    /// <summary>
    /// Add gameobject to selections and highlight it, only if it is selectable
    /// </summary>
    /// <param name="GO">Gameobject is supposed to contain Selectable and not exist in unitSelections</param>
    private void Select(GameObject GO)
    {
        // Add selectable object to be selected 
        Selectable selectable = GO.GetComponent<Selectable>();

        // Guard for selectable and make sure we dont add same object twice
        if (selectable == null) return;
        if (unitSelections.Contains(selectable)) return;

        unitSelections.Add(selectable);
        selectable.IsSelected = true;
    }

    /// <summary>
    /// Make sure gameobject is selectable and check is it selected
    /// </summary>
    /// <param name="_gameObject">Gameobject containing Selectable</param>
    /// <returns>Return false if not selectable and if gameobject is not selected.</returns>
    public static bool IsSelected(GameObject _gameObject)
    {
        Selectable selectable = _gameObject.GetComponent<Selectable>();
        if (selectable != null)
        {
            return IsSelected(selectable);
        }
        return false;
    }

    /// <summary>
    /// Check whether selectable object is selected
    /// </summary>
    /// <param name="_selectable">A selectable object</param>
    /// <returns>Return false if not selected, true if it is selected</returns>
    public static bool IsSelected(Selectable _selectable)
    {
        return _selectable.IsSelected;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.Center, box.Size);
    }
    */
}
