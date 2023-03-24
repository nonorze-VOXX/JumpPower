namespace Player
{
    public enum SaveCase
    {
        AnyWhere,
        onlyTpTrigger,
        no
    }

    public class PlayCase
    {
        public static SaveCase saveCase = SaveCase.AnyWhere;
    }
}