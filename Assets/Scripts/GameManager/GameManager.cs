using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShootEmUp
{
    public enum GameState
    {
        Start,
        Finish,
        Pause,
        Resume,
        Off
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private GameState gameState;
        
        private List<IGameListener> gameListeners = new List<IGameListener>();
        
        private void Awake()
        {
            gameState = GameState.Off;
        }
        
        [Button]
        public void StartGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }

            gameState = GameState.Start;
        }
        
        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }
            
            gameState = GameState.Finish;
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
            
            gameState = GameState.Pause;
        }   
        
        [Button]
        public void ResumeGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
            gameState = GameState.Resume;
        }
        
        public void FFinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}