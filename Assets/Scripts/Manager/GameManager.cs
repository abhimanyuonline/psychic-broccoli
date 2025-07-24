using System.Collections;
using System.Collections.Generic;
using broccoli.Controller;
using broccoli.Manager.Audio;
using broccoli.Presenter;
using UnityEngine;
using Zenject;

namespace broccoli.Manager
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuScreen;
        [SerializeField] private GameObject gameScreen;

        [Inject] private CardController cardController;
        [Inject] private SoundManager soundManager;
        [Inject] private GamePresenter gamePresenter;

        void Start()
        {
            // Play background music and set up the initial screen
            soundManager?.PlayMusic("BG", true);
            InitialScreenSetUp();
        }

        /// <summary>
        /// Sets up the initial screen: shows menu, hides game, resets score, and clears old grids.
        /// </summary>
        public void InitialScreenSetUp()
        {
            cardController?.ClearOldGrids();
            if (gamePresenter?.scoreText != null)
                gamePresenter.scoreText.text = "0";
            if (menuScreen != null) menuScreen.SetActive(true);
            if (gameScreen != null) gameScreen.SetActive(false);
        }

        /// <summary>
        /// Switches to the game screen and hides the menu.
        /// </summary>
        public void InitializeGameScreen()
        {
            if (menuScreen != null) menuScreen.SetActive(false);
            if (gameScreen != null) gameScreen.SetActive(true);
        }
    }
}

