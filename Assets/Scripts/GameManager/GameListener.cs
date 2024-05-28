namespace ShootEmUp
{
    public interface IGameListener
    {
    }
    
    public interface IGameStartListener
    {
        void OnStartGame();
    }
    
    public interface IGameFinishListener
    {
        void OnFinishGame();
    } 
    
    public interface IGamePauseListener
    {
        void OnPauseGame();
    }
    
    public interface IGameResumeListener
    {
        void OnResumeGame();
    }
}