using System.Collections;
using System.Collections.Generic;
using broccoli.Controller;
using broccoli.Manager.Audio;
using broccoli.Presenster;
using UnityEngine;
using Zenject;

namespace broccoli.Manager
{

    public class GameManger : MonoBehaviour
    {
        [SerializeField] GameObject MenuScreen;
        [SerializeField] GameObject GameScreen;

        [Inject] CardController _cardConroller;
        [Inject] SoundManager soundManager;
        [Inject] GamePresenter gamePresenter;

        void Start()
        {
            soundManager.PlayMusic("BG", true);
            InitialScreenSetUp();
        }

        public void InitialScreenSetUp()
        {

            _cardConroller.ClearOldGrids();
            gamePresenter.scoreText.text = "0";
            MenuScreen.SetActive(true);
            GameScreen.SetActive(false);
        }

        public void InitalizeGameScreen()
        {
            MenuScreen.SetActive(false);
            GameScreen.SetActive(true);
        }


    }
}

