using System.Collections;
using System.Collections.Generic;
using broccoli.Cards;
using UnityEngine;

namespace broccoli.Controller
{
    public class CardController : MonoBehaviour
    {
        [Header("Card Game Settings")]
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform gridTransform;
        [SerializeField] private Sprite[] sprites;

        private List<Sprite> _spritePairs;
        private Card _firstSelected;
        private Card _secondSelected;
        private int _matchCount = 0;

        #region Unity Methods
        private void Start()
        {
            
        }
        #endregion

        /// <summary>
        /// Prepares the sprite pairs for the card game by duplicating and shuffling them.
        /// </summary>
        public void PrepareSprites(int pairsCount)
        {
            _spritePairs = new List<Sprite>();
            for (int i = 0; i < pairsCount; i++)
            {
                var sprite = sprites[i];
                // Add each sprite twice to create pairs
                _spritePairs.Add(sprite);
                _spritePairs.Add(sprite);
            } 
            ShuffleSprites(_spritePairs);
            CreateCards();
        }

        /// <summary>
        /// Instantiates card objects and assigns their sprites.
        /// </summary>
        private void CreateCards()
        {
            for (int i = 0; i < _spritePairs.Count; i++)
            {
                Card card = Instantiate(cardPrefab, gridTransform);
                card.SetIconSprite(_spritePairs[i]);
                card.Controller = this;
            }
        }

        /// <summary>
        /// Handles card selection logic.
        /// </summary>
        /// <param name="card">The card that was selected.</param>
        public void SetSelected(Card card)
        {
            if (card == null || card.IsSelected)
                return;

            card.Show();

            if (_firstSelected == null)
            {
                _firstSelected = card;
                return;
            }

            if (_secondSelected == null)
            {
                _secondSelected = card;
                StartCoroutine(CheckMatching(_firstSelected, _secondSelected));
                _firstSelected = _secondSelected = null;
            }
        }

        /// <summary>
        /// Coroutine to check if two selected cards match.
        /// </summary>
        private IEnumerator CheckMatching(Card a, Card b)
        {
            yield return new WaitForSeconds(0.3f);
            if (a.IconSprite == b.IconSprite)
            {
                _matchCount++;
                if (_matchCount >= _spritePairs.Count / 2)
                {
                    Debug.Log("All pairs matched! Game complete.");
                }
                // Optionally: Add logic for matched cards (e.g., disable interaction)
            }
            else
            {
                a.Hide();
                b.Hide();
            }
        }

        /// <summary>
        /// Shuffles a list of sprites using Fisher-Yates algorithm.
        /// </summary>
        private void ShuffleSprites(List<Sprite> spriteList)
        {
            for (int i = spriteList.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Sprite temp = spriteList[i];
                spriteList[i] = spriteList[randomIndex];
                spriteList[randomIndex] = temp;
            }
        }
    }
}

