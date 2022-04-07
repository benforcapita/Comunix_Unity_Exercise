using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                UniRx.MessageBroker.Default.Publish(new PlayerAttackEventArgs());
            }

            UniRx.MessageBroker.Default.Publish(Input.GetAxisRaw("Horizontal") != 0
                ? new HorizontalPlayerMoveEventArgs(Input.GetAxisRaw("Horizontal"))
                : new HorizontalPlayerMoveEventArgs(0));

            UniRx.MessageBroker.Default.Publish(Input.GetAxisRaw("Vertical") != 0
                ? new VerticalPlayerMoveEventArgs(Input.GetAxisRaw("Vertical"))
                : new VerticalPlayerMoveEventArgs(0));
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
