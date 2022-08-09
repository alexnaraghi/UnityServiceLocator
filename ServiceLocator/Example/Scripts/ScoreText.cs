using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocatorExamples
{
    public class ScoreText : MonoBehaviour
    {
        public Text score;

        private GameState _gameState;

        private void Awake()
        {
            _gameState = Services.Get<GameState>();
            _gameState.ScoreChanged.AddListener(OnScoreChanged);
        }

        private void OnDestroy()
        {
            _gameState.ScoreChanged.RemoveListener(OnScoreChanged);
        }

        private void OnScoreChanged()
        {
            score.text = _gameState.score.ToString();
        }
    }
}
