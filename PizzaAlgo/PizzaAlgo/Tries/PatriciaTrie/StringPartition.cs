using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PizzaAlgo.Tries.PatriciaTrie
{
    [DebuggerDisplay(
        "{m_Origin.Substring(0,m_StartIndex)} [ {m_Origin.Substring(m_StartIndex,m_PartitionLength)} ] {m_Origin.Substring(m_StartIndex + m_PartitionLength)}"
    )]
    public class StringPartition : IEnumerable<char>
    {
        private readonly string mOrigin;
        private readonly int mStartIndex;

        public int Length { get; }
        public char this[int index] => mOrigin[mStartIndex + index];

        public StringPartition(string origin)
            : this(origin, 0, origin?.Length ?? 0)
        {
        }

        public StringPartition(string origin, int startIndex)
            : this(origin, startIndex, origin?.Length - startIndex ?? 0)
        {
        }

        public StringPartition(string origin, int startIndex, int partitionLength)
        {
            if (origin == null) throw new ArgumentNullException("origin");
            if (startIndex < 0) throw new ArgumentOutOfRangeException("startIndex", "The value must be non negative.");
            if (partitionLength < 0)
                throw new ArgumentOutOfRangeException("partitionLength", "The value must be non negative.");
            mOrigin = string.Intern(origin);
            mStartIndex = startIndex;
            int availableLength = mOrigin.Length - startIndex;
            Length = Math.Min(partitionLength, availableLength);
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(StringPartition other)
        {
            return string.Equals(mOrigin, other.mOrigin) && Length == other.Length &&
                   mStartIndex == other.mStartIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StringPartition partition && Equals(partition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (mOrigin != null ? mOrigin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ mStartIndex;
                return hashCode;
            }
        }

        public bool StartsWith(StringPartition other)
        {
            if (Length < other.Length)
            {
                return false;
            }

            return !other.Where((t, i) => this[i] != t).Any();
        }

        public SplitResult Split(int splitAt)
        {
            var head = new StringPartition(mOrigin, mStartIndex, splitAt);
            var rest = new StringPartition(mOrigin, mStartIndex + splitAt, Length - splitAt);
            return new SplitResult(head, rest);
        }

        public ZipResult ZipWith(StringPartition other)
        {
            var splitIndex = 0;
            using (var thisEnumerator = GetEnumerator())
            using (var otherEnumerator = other.GetEnumerator())
            {
                while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
                {
                    if (thisEnumerator.Current != otherEnumerator.Current)
                    {
                        break;
                    }
                    splitIndex++;
                }
            }

            var thisSplitted = Split(splitIndex);
            var otherSplitted = other.Split(splitIndex);

            var commonHead = thisSplitted.Head;
            var restThis = thisSplitted.Rest;
            var restOther = otherSplitted.Rest;
            return new ZipResult(commonHead, restThis, restOther);
        }

        public override string ToString()
        {
            var result = new string(this.ToArray());
            return string.Intern(result);
        }

        public static bool operator ==(StringPartition left, StringPartition right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(StringPartition left, StringPartition right)
        {
            return !(left == right);
        }
    }
}
