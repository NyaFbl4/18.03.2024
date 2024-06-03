using System;
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
        private GameState _gameState;
        
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _gameLateUpdateListeners = new();

        public event Action OnStartGame;
        public event Action OnCountdownStart;
        
        private void Awake()
        {
            _gameState = GameState.Off;
            
            IGameListener.onRegister += AddListener;
        }
        
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
            IGameListener.onRegister -= AddListener;
        }
        
        private void Update()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }
        
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                _gameUpdateListeners[i].OnUpdate(deltaTime);
            }
        }
        private void FixedUpdate()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                _gameFixedUpdateListeners[i].OnFixedUpdate(deltaTime);
            }
        }
        private void LateUpdate()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameLateUpdateListeners.Count; i++)
            {
                _gameLateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }
        
        private void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);
            
            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }   
            
            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }
            
            if (gameListener is IGameLateUpdateListener gameLateUpdateListener)
            {
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
            }
        }
        
        [Button]
        public void StartGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }
            
            OnStartGame?.Invoke(); 

            Invoke(nameof(StartGameAfterCountdown), 3f);
            
            /*
            OnStartGame?.Invoke();
            
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }

            _gameState = GameState.Start;
            Debug.Log("OnStartGame");
            */
        }

        private void StartGameAfterCountdown()
        {
            /*
             OnStartGame?.Invoke();

            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }
            */

            _gameState = GameState.Start;
            Debug.Log("OnStartGame");
        }

        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }
            
            _gameState = GameState.Finish;
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
            
            _gameState = GameState.Pause;
        }   
        
        [Button]
        public void ResumeGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
            _gameState = GameState.Start;
        }
        
        public void FFinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}