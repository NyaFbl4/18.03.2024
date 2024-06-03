using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IGameListener
    {
        public static event Action<IGameListener> onRegister;
        
        public static void Register(IGameListener gameListener)
        {
            if (null != onRegister)
            {
                onRegister.Invoke(gameListener);
            }
        }
    }
    
    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }
    
    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    } 
    
    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }
    
    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }
    
    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}