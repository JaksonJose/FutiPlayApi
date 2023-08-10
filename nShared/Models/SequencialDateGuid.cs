
namespace xShared.Models
{
    /// <summary>
    /// Copied from https://www.siepman.nl/blog/post/2015/06/20/SequentialGuid-Comb-Sql-Server-With-Creation-Date-Time-.aspx
    /// This can be used to generate a GUID and as a bonus the value will include a data-time stamp down to the millisecond.
    /// Define the property as a regular GUID, but when you create the actual value use this SequentialDateGuid structure.
    /// The ToString() will print the GUID and the Date-time stamp when the GUID was created.
    /// </summary>
    public struct SequentialDateGuid : IComparable<SequentialDateGuid>, IComparable<Guid>, IComparable
    {
        private const int NumberOfSequenceBytes = 6;
        private const int PermutationsOfAByte = 256;
        private static readonly long MaximumPermutations = (long)Math.Pow(PermutationsOfAByte, NumberOfSequenceBytes);
        private static long _lastSequence;

        private static readonly DateTime SequencePeriodStart =
            new(2011, 11, 15, 0, 0, 0, DateTimeKind.Utc); // Start = 000000

        private static readonly DateTime SequencePeriodeEnd =
            new(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc);   // End   = FFFFFF

        private readonly Guid _guidValue;

        /// <summary>
        /// Constructor requiring Guid type parameter. 
        /// </summary>
        /// <param name="guidValue"></param>
        public SequentialDateGuid(Guid guidValue)
        {
            _guidValue = guidValue;
        }

        /// <summary>
        /// Constructor requiring string version of a Guid.
        /// </summary>
        /// <param name="guidValue"></param>
        public SequentialDateGuid(string guidValue) : this(new Guid(guidValue))
        {
        }

        /// <summary>
        /// Static method used to get a new instance.
        /// </summary>
        /// <returns></returns>
        [System.Security.SecuritySafeCritical]
        public static SequentialDateGuid NewSequentialDateGuid()
        {
            // You might want to inject DateTime.Now in production code
            return new SequentialDateGuid(GetGuidValue(DateTime.Now));
        }

        /// <summary>
        /// Static property
        /// </summary>
        public static TimeSpan TimePerSequence
        {
            get
            {
                var ticksPerSequence = TotalPeriod.Ticks / MaximumPermutations;
                var result = new TimeSpan(ticksPerSequence);
                return result;
            }
        }

        /// <summary>
        /// Static property
        /// </summary>
        public static TimeSpan TotalPeriod
        {
            get
            {
                var result = SequencePeriodeEnd - SequencePeriodStart;
                return result;
            }
        }

        #region FromDateTimeToGuid

        // Internal for testing
        internal static Guid GetGuidValue(DateTime now)
        {
            if (now < SequencePeriodStart || now >= SequencePeriodeEnd)
            {
                return Guid.NewGuid(); // Outside the range, use regular Guid
            }

            var sequence = GetCurrentSequence(now);
            return GetGuid(sequence);
        }

        private static long GetCurrentSequence(DateTime now)
        {
            var ticksUntilNow = now.Ticks - SequencePeriodStart.Ticks;
            var factor = (decimal)ticksUntilNow / TotalPeriod.Ticks;
            var resultDecimal = factor * MaximumPermutations;
            var resultLong = (long)resultDecimal;
            return resultLong;
        }

        private static readonly object SynchronizationObject = new();
        private static Guid GetGuid(long sequence)
        {
            lock (SynchronizationObject)
            {
                if (sequence <= _lastSequence)
                {
                    // Prevent double sequence on same server
                    sequence = _lastSequence + 1;
                }
                _lastSequence = sequence;
            }

            var sequenceBytes = GetSequenceBytes(sequence);
            var guidBytes = GetGuidBytes();
            var totalBytes = guidBytes.Concat(sequenceBytes).ToArray();
            var result = new Guid(totalBytes);
            return result;
        }

        private static IEnumerable<byte> GetSequenceBytes(long sequence)
        {
            var sequenceBytes = BitConverter.GetBytes(sequence);
            var sequenceBytesLongEnough = sequenceBytes.Concat(new byte[NumberOfSequenceBytes]);
            var result = sequenceBytesLongEnough.Take(NumberOfSequenceBytes).Reverse();
            return result;
        }

        private static IEnumerable<byte> GetGuidBytes()
        {
            return Guid.NewGuid().ToByteArray().Take(10);
        }

        #endregion

        #region FromGuidToDateTime

        /// <summary>
        /// The date/time the Guid was created
        /// </summary>
        public DateTime CreatedDateTime
        {
            get
            {
                return GetCreatedDateTime(_guidValue);
            }
        }

        internal static DateTime GetCreatedDateTime(Guid value)
        {
            var sequenceBytes = GetSequenceLongBytes(value).ToArray();
            var sequenceLong = BitConverter.ToInt64(sequenceBytes, 0);
            var sequenceDecimal = (decimal)sequenceLong;
            var factor = sequenceDecimal / MaximumPermutations;
            var ticksUntilNow = factor * TotalPeriod.Ticks;
            var nowTicksDecimal = ticksUntilNow + SequencePeriodStart.Ticks;
            var nowTicks = (long)nowTicksDecimal;
            var result = new DateTime(nowTicks);
            return result;
        }

        private static IEnumerable<byte> GetSequenceLongBytes(Guid value)
        {
            const int numberOfBytesOfLong = 8;
            var sequenceBytes = value.ToByteArray().Skip(10).Reverse().ToArray();
            var additionalBytesCount = numberOfBytesOfLong - sequenceBytes.Length;
            return sequenceBytes.Concat(new byte[additionalBytesCount]);
        }

        #endregion

        #region Relational Operators

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <(Guid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >(Guid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <(SequentialDateGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >(SequentialDateGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <=(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >=(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <=(Guid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >=(Guid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator <=(SequentialDateGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator >=(SequentialDateGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        #endregion

        #region Equality Operators

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(SequentialDateGuid value1, SequentialDateGuid value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(Guid value1, SequentialDateGuid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(Guid value1, SequentialDateGuid value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(SequentialDateGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        /// <summary>
        /// Operator definition to enable comparisons of instances.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(SequentialDateGuid value1, Guid value2)
        {
            return !(value1 == value2);
        }

        #endregion

        #region CompareTo

        /// <summary>
        /// Helper method for comparisons
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "<Pending>")]
        public int CompareTo(object? obj)
        {
            switch (obj)
            {
                case SequentialDateGuid:
                    return CompareTo((SequentialDateGuid)obj);
                case Guid:
                    return CompareTo((Guid)obj);
                default:
                    throw new ArgumentException("Parameter is not of the rigt type");
            }
        }

        /// <summary>
        /// Helper method for comparisons
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(SequentialDateGuid other)
        {
            return CompareTo(other._guidValue);
        }

        /// <summary>
        /// Helper method for comparisons
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Guid other)
        {
            return CompareImplementation(_guidValue, other);
        }

        private static readonly int[] IndexOrderingHighLow =
                                      { 10, 11, 12, 13, 14, 15, 8, 9, 7, 6, 5, 4, 3, 2, 1, 0 };

        private static int CompareImplementation(Guid left, Guid right)
        {
            var leftBytes = left.ToByteArray();
            var rightBytes = right.ToByteArray();

            return IndexOrderingHighLow.Select(i => leftBytes[i].CompareTo(rightBytes[i]))
                                       .FirstOrDefault(r => r != 0);
        }

        #endregion

        #region Equals

        /// <summary>
        /// Helper method related to comparisons.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is SequentialDateGuid || obj is Guid)
            {
                return CompareTo(obj) == 0;
            }

            return false;
        }

        /// <summary>
        /// Helper method related to comparisons.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SequentialDateGuid other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Helper method related to comparisons.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Guid other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Helper method related to comparisons.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _guidValue.GetHashCode();
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Helper method for implicit operators
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Guid(SequentialDateGuid value)
        {
            return value._guidValue;
        }

        /// <summary>
        /// Helper method for explicit operators
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator SequentialDateGuid(Guid value)
        {
            return new SequentialDateGuid(value);
        }

        #endregion

        #region ToString

        /// <summary>
        /// Helper method for ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var roundedCreatedDateTime = Round(CreatedDateTime, TimeSpan.FromMilliseconds(1));
            return string.Format("{0} ({1:yyyy-MM-dd HH:mm:ss.fff})",
                                 _guidValue, roundedCreatedDateTime);
        }

        private static DateTime Round(DateTime dateTime, TimeSpan interval)
        {
            var halfIntervalTicks = (interval.Ticks + 1) >> 1;

            return dateTime.AddTicks(halfIntervalTicks -
                   ((dateTime.Ticks + halfIntervalTicks) % interval.Ticks));
        }

        #endregion
    }
}
