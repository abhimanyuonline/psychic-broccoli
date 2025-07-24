using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using broccoli.Manager;
using Zenject;
using broccoli.Manager.Audio;
using TMPro;

namespace broccoli.Presenter
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] public TMP_Text scoreText;
        [SerializeField] public TMP_Text highScoreText;
        [Inject] private GameManager gameManager;
        [Inject] private SoundManager soundManager;

        void Start()
        {
            // Initialize UI and button listeners
            ResetScreen();
            if (pauseButton != null) pauseButton.onClick.AddListener(OnPauseClicked);
            if (resumeButton != null) resumeButton.onClick.AddListener(OnResumeClicked);
            if (menuButton != null) menuButton.onClick.AddListener(OnMainMenuClicked);
            if (quitButton != null) quitButton.onClick.AddListener(OnQuitClicked);
        }

        /// <summary>
        /// Hides pause and end game panels.
        /// </summary>
        void ResetScreen()
        {
            if (pausePanel != null) pausePanel.SetActive(false);
            if (endGamePanel != null) endGamePanel.SetActive(false);
        }

        void OnPauseClicked()
        {
            if (pausePanel != null) pausePanel.SetActive(true);
            if (endGamePanel != null) endGamePanel.SetActive(false);
            PlayButtonSound();
        }

        void OnResumeClicked()
        {
            if (pausePanel != null) pausePanel.SetActive(false);
            if (endGamePanel != null) endGamePanel.SetActive(false);
            PlayButtonSound();
        }

        void OnQuitClicked()
        {
            OnMainMenuClicked();
            PlayButtonSound();
        }

        void OnMainMenuClicked()
        {
            ResetScreen();
            gameManager?.InitialScreenSetUp();
        }

        /// <summary>
        /// Shows the end game panel and updates the high score if needed.
        /// </summary>
        public void EndGameScreen()
        {
            if (pausePanel != null) pausePanel.SetActive(false);
            if (endGamePanel != null) endGamePanel.SetActive(true);
            CheckAndUpdateHighScore();
        }

        /// <summary>
        /// Checks and updates the high score if the current score is higher.
        /// </summary>
        public void CheckAndUpdateHighScore()
        {
            const string ScoreKey = "Score";
            int savedScore = PlayerPrefs.GetInt(ScoreKey, 0);
            int currentScore = 0;
            if (scoreText != null && int.TryParse(scoreText.text, out currentScore))
            {
                if (savedScore < currentScore)
                {
                    PlayerPrefs.SetInt(ScoreKey, currentScore);
                    if (highScoreText != null) highScoreText.text = currentScore.ToString();
                }
                else
                {
                    if (highScoreText != null) highScoreText.text = savedScore.ToString();
                }
            }
        }

        /// <summary>
        /// Plays the button click sound effect.
        /// </summary>
        void PlayButtonSound()
        {
            soundManager?.PlaySfx("Button");
        }
    }
}

