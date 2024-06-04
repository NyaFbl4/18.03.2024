using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour 
        ,IGameUpdateListener, IGameFixedUpdateListener
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField] private GameObject character;
        [SerializeField] private CharacterController characterController;

        private void Start()
        {
            IGameListener.Register(this);
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterController._fireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(new Vector2(this.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}