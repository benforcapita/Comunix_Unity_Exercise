namespace Player
{
    public class PlayerAttackEventArgs
    {
        public float fireAxis;
        
        public PlayerAttackEventArgs(float fireAxis)
        {
            this.fireAxis = fireAxis;
        }

    }
}