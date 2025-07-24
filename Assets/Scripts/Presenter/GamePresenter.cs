using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using broccoli.Manager;
using Zenject;
using broccoli.Manager.Audio;
using TMPro;

namespace broccoli.Presenster
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] Button pauseBuuton;
        [SerializeField] Button menuButton;
        [SerializeField] Button resumeButton;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject endGamePanel;
        public TMP_Text scoreText ;
        public TMP_Text highScoreText;
        [Inject] GameManger _gameManager;
        [Inject] SoundManager soundManager;

        void Start()
        {

            ResetScreen();
            pauseBuuton.onClick.AddListener(() => OnPauseClicked());
            resumeButton.onClick.AddListener(() => OnResumeClicked());
            menuButton.onClick.AddListener(() => OnMainMenuCliked());
            quitButton.onClick.AddListener(() => OnQuitClicked());

        }
        void ResetScreen()
        {
            pausePanel.SetActive(false);
            endGamePanel.SetActive(false);
        }

        void OnPauseClicked()
        {
            pausePanel.SetActive(true);
            endGamePanel.SetActive(false);
            PlayButtonSound();
        }
        void OnResumeClicked()
        {
            pausePanel.SetActive(false);
            endGamePanel.SetActive(false);
            PlayButtonSound();
        }
        void OnQuitClicked()
        {
            OnMainMenuCliked();
            PlayButtonSound();
        }
        void OnMainMenuCliked()
        {
            ResetScreen();
            _gameManager.InitialScreenSetUp();
        }
        public void EndGameScreen()
        {
            pausePanel.SetActive(false);
            endGamePanel.SetActive(true);
            CheckAndUpadateHighScore();
        }

        public void CheckAndUpadateHighScore()
        {
            var score = PlayerPrefs.GetInt("Score");
            var currentScore = int.Parse(scoreText.text);
            if (score < currentScore)
            {
                PlayerPrefs.SetInt("Score", currentScore);
                highScoreText.text = scoreText.text.ToString();
            }
            else
            {
                highScoreText.text = score.ToString();
            }
        }

        void PlayButtonSound(){
            soundManager.PlaySfx("Button");
        }
    }
    

}

