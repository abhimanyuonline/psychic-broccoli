using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Button Settings")]
    public GameObject buttonPrefab; // Assign a UI Button prefab in the inspector
    public Transform buttonParent;  // Assign a UI layout group or RectTransform in the inspector
    Dictionary<Vector2Int, int> grid = new Dictionary<Vector2Int, int>();

    // Struct to hold button and its X/Y values
    private struct ButtonData
    {
        public Button button;
        public int x;
        public int y;
    }

    void Start()
    {
        AssignGridValues();
        CreateButtons();
    }

    // Assigns specific Vector2Int keys to the grid dictionary
    void AssignGridValues()
    {
        grid[new Vector2Int(2, 2)] = 0;
        grid[new Vector2Int(2, 3)] = 1;
        grid[new Vector2Int(5, 6)] = 2;
    }
     void CreateButtons()
    {
        foreach (var kvp in grid)
        {
            Vector2Int coords = kvp.Key;
            GameObject btnObj = Instantiate(buttonPrefab, buttonParent);
            Button btn = btnObj.GetComponent<Button>();

            // Set button text to show X/Y
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = $" {coords.x} X {coords.y}";
                
            btn.onClick.AddListener(() => OnButtonClicked(coords));

        }
    }

    // Example button click handler
    void OnButtonClicked(Vector2Int coords )
    {
        Debug.Log($"Button clicked! X: {coords.x}, Y: {coords.y}");
    }
}
