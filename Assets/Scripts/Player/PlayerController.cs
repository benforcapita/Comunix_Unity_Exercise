using System;
using Mobile;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
      
        
        [SerializeField]private bool useJoystick = false;
        [ShowIf("useJoystick")]
        [SerializeField] private float _speed = 5f;
        [ShowIf("useJoystick")]
        [SerializeField] private bl_Joystick _blJoystick;
        [ShowIf("useJoystick")]
        [SerializeField] private Vector3 joystickMovement;

        [ShowIf("useJoystick")][SerializeField] private Button attackButton;

        private IDisposable mobileControlssubscription = null;

        private void OnEnable()
        {
            mobileControlssubscription = MessageBroker.Default.Receive<MobileControllersEventArgs>()
                .ObserveOnMainThread().Subscribe(GetMobileControls);
        }

        private void GetMobileControls(MobileControllersEventArgs obj)
        {
            useJoystick = true;
            attackButton = obj.AttackButton;
            _blJoystick = obj.Joystick;
            
        }

        private void Update()
        {
            if(Application.platform == RuntimePlatform.WindowsPlayer||Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (!useJoystick)
                {
                    Move();
                }
                else
                {
                    MoveWithJoystick();
                }
                
            }
            else
            {
                MoveWithJoystick();
            }
        }

        private void MoveWithJoystick()
        {
            joystickMovement =  new Vector3(_blJoystick.Horizontal, 0, _blJoystick.Vertical).normalized * _speed;
            UniRx.MessageBroker.Default.Publish(new HorizontalPlayerMoveEventArgs(joystickMovement.x));
            UniRx.MessageBroker.Default.Publish(new VerticalPlayerMoveEventArgs(joystickMovement.y));
            attackButton.onClick.AddListener(() =>
            {
                MessageBroker.Default.Publish(new PlayerAttackEventArgs(1));
                //wait for 0.1 seconds before publishing another message to reset the attack 
                Observable.Timer(TimeSpan.FromSeconds(0.1)).Subscribe(_ => MessageBroker.Default.Publish(new PlayerAttackEventArgs(0)));
            });
        }

        private void Move()
        {
            UniRx.MessageBroker.Default.Publish(new PlayerAttackEventArgs(Input.GetAxisRaw("Fire1")));
            UniRx.MessageBroker.Default.Publish(Input.GetAxisRaw("Horizontal") != 0
                ? new HorizontalPlayerMoveEventArgs(Input.GetAxisRaw("Horizontal"))
                : new HorizontalPlayerMoveEventArgs(0));

            UniRx.MessageBroker.Default.Publish(Input.GetAxisRaw("Vertical") != 0
                ? new VerticalPlayerMoveEventArgs(Input.GetAxisRaw("Vertical"))
                : new VerticalPlayerMoveEventArgs(0));
        }

        private void OnDisable()
        {
            mobileControlssubscription.Dispose();
        }
    }

    public class VerticalPlayerMoveEventArgs
    {
        public float AxisValue { get; set; }

        public VerticalPlayerMoveEventArgs(float getAxisRaw)
        {
            AxisValue = getAxisRaw;
        }
    }

    public class HorizontalPlayerMoveEventArgs
    {
        public float AxisValue { get; set; }

        public HorizontalPlayerMoveEventArgs(float getAxisRaw)
        {
            AxisValue = getAxisRaw;
        }
    }
}