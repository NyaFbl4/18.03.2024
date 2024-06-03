using UnityEngine;
using System.Collections;
using TMPro;

namespace ShootEmUp
{
    public class GameStarter : MonoBehaviour, 
        IGameStartListener, IGameFinishListener
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _buttonsStart;
        [SerializeField] private TextMeshProUGUI _timerStart;

        private void Start()
        {
            IGameListener.Register(this);
        }
        
        public void OnStartGame()
        {
            _gameManager.OnStartGame += StartGame;
        }

        public void OnFinishGame()
        {
            _gameManager.OnStartGame -= StartGame;
        }

        private void StartGame()
        {
            //Debug.Log("1");
            _buttonsStart.SetActive(false);
            StartCoroutine(CountdownRoutine());
        }
        
        private IEnumerator CountdownRoutine()
        {
            for (int count = 3; count >= 1; count--)
            {
                _timerStart.text = count.ToString();
                //Debug.Log(count.ToString() + "...");
                yield return new WaitForSeconds(1f);
            }
            _timerStart.text = " ";
            //Debug.Log("Go!");
        }
    }
}
