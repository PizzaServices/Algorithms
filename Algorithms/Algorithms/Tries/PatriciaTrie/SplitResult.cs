namespace Algorithms.Tries.PatriciaTrie
{
    public class SplitResult
    {
        public StringPartition Rest { get; }
        public StringPartition Head { get; }

        public SplitResult(StringPartition head, StringPartition rest)
        {
            Head = head;
            Rest = rest;
        }

        public bool Equals(SplitResult other)
        {
            return Head == other.Head && Rest == other.Rest;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SplitResult result && Equals(result);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Head.GetHashCode() * 397) ^ Rest.GetHashCode();
            }
        }

        public static bool operator ==(SplitResult left, SplitResult right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(SplitResult left, SplitResult right)
        {
            return !(left == right);
        }
    }
}
