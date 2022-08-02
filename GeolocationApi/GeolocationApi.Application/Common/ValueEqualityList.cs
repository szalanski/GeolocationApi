namespace GeolocationApi.Application.Common
{
    public class ValueEqualityList<T> : List<T>
    {
        public override bool Equals(object other)
        {
            if (!(other is IEnumerable<T> enumerable)) return false;
            return enumerable.SequenceEqual(this);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            foreach (var item in this)
            {
                hashCode ^= item.GetHashCode();
            }

            return hashCode;
        }
    }
}
