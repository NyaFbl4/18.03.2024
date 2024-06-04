using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, 
        IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        private void Start()
        {
            IGameListener.Register(this);
        }
      
        public void OnStartGame()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        public void OnFixedUpdate(float deltaTime)
        {    
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}