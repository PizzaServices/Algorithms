using System.Diagnostics;

namespace Algorithms.Tries.PatriciaTrie
{
    [DebuggerDisplay("Head: '{CommonHead}', This: '{ThisRest}', Other: '{OtherRest}', Kind: {MatchKind}")]
    public class ZipResult
    {
        public StringPartition OtherRest { get; }
        public StringPartition ThisRest { get; }
        public StringPartition CommonHead { get; }

        public ZipResult(StringPartition commonHead, StringPartition thisRest, StringPartition otherRest)
        {
            CommonHead = commonHead;
            ThisRest = thisRest;
            OtherRest = otherRest;
        }

        public MatchKind MatchKind =>
            ThisRest.Length == 0
                ? (OtherRest.Length == 0
                    ? MatchKind.ExactMatch
                    : MatchKind.IsContained)
                : (OtherRest.Length == 0
                    ? MatchKind.Contains
                    : MatchKind.Partial);


        public bool Equals(ZipResult other)
        {
            return
                CommonHead == other.CommonHead &&
                OtherRest == other.OtherRest &&
                ThisRest == other.ThisRest;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ZipResult result && Equals(result);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CommonHead.GetHashCode();
                hashCode = (hashCode * 397) ^ OtherRest.GetHashCode();
                hashCode = (hashCode * 397) ^ ThisRest.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ZipResult left, ZipResult right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(ZipResult left, ZipResult right)
        {
            return !(left == right);
        }
    }
}
