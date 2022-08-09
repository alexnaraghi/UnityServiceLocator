using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Some game values that may need to be accessed from multiple places.
/// </summary>
public class GameState : MonoBehaviour
{
    public UnityEvent ScoreChanged = new UnityEvent();
    
    public bool isGameStarted;

    [SerializeField] private int _score;

    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            ScoreChanged?.Invoke();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        score = 0;
    }
}