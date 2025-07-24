using System.Collections;
using System.Collections.Generic;
using broccoli.Controller;
using broccoli.Manager;
using broccoli.Manager.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class LevelManager : MonoBehaviour
{
    [Header("Button Settings")]
    public GameObject buttonPrefab; 
    public Transform buttonParent;  
    Dictionary<Vector2Int, int> grid = new Dictionary<Vector2Int, int>();

    [SerializeField] GridLayoutGroup gridLayoutGroup;

    [Inject] GameManger _gameManager;
    [Inject] CardController _cardConroller;
    [Inject] SoundManager soundManager;

    // Struct to hold button and its X/Y values
    private struct ButtonData
    {
        public Button button;
        public int x;
        public int y;
    }

    void Start()
    {
        CreateButtons();
    }

    // Assigns specific Vector2Int keys to the grid dictionary
    void AssignGridValues()
    {
        grid[new Vector2Int(2, 2)] = 0;
        grid[new Vector2Int(2, 3)] = 1;
        grid[new Vector2Int(5, 6)] = 2;
    }
    public void CreateButtons()
    {
        AssignGridValues();
        foreach (var kvp in grid)
        {
            Vector2Int coords = kvp.Key;
            GameObject btnObj = Instantiate(buttonPrefab, buttonParent);
            Button btn = btnObj.GetComponent<Button>();

            // Set button text to show X/Y
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = $"{coords.x} X {coords.y}";

            btn.onClick.AddListener(() => OnButtonClicked(coords));

        }
    }

    void OnButtonClicked(Vector2Int coords)
    {
        soundManager.PlaySfx("Button");
        gridLayoutGroup.constraintCount = coords.y;
        _gameManager.InitalizeGameScreen();

        int maxPair = (coords.x * coords.y)/ 2;
        _cardConroller.PrepareSprites(maxPair);
        
    }
    
}
