namespace PROSITE.Core
{
    public class Match
    {
        public Match(string matchedText, int position)
        {
            Position = position;
            MatchedText = matchedText;
        }

        public string MatchedText { get; private set; }
        public int Position { get; private set; }
        public int Length { get { return MatchedText != null ? MatchedText.Length : 0; } }
    }
}