namespace GameManager
{
    public class BombHitEventArgs
    {
        public int ScoreToAdd { get; }

        public BombHitEventArgs(int scoreToAdd)
        {
            this.ScoreToAdd = scoreToAdd;
        }
    }
}