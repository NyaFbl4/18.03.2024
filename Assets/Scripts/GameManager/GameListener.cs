using UnityEngine;

namespace ShootEmUp
{
    public interface IGameListener
    {
        void Method()
        {
            Debug.Log("Method");
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
}