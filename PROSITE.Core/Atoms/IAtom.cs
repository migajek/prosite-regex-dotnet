namespace PROSITE.Core.Atoms
{
    public interface IAtom
    {
        bool Matches(string data, ref int cp);
        int ExpectedMatchLength { get; }
    }
}
