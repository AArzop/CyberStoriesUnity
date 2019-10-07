namespace CyberStories.Shared.Player
{
    public class NoInteractionIA : BaseInteractionIA
    {
        public override bool IsDone()
        {
            return true;
        }

        public override float MinRemainingDistance()
        {
            return 0.5f;
        }
    }
}
