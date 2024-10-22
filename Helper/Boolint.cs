using System.Diagnostics.CodeAnalysis;

namespace Helper
{
	public struct Boolint : IComparable, IComparable<Boolint>, IEquatable<Boolint>, IParsable<Boolint>, ISpanParsable<Boolint>, IConvertible
	{
		private int value;

		public Boolint(int initialValue)
		{
			if (initialValue == 1 || initialValue == -1)
				value = initialValue;
			else
				throw new ArgumentException("Boolint only accepts 1 or -1");
		}

		public static implicit operator Boolint(bool initialValue)
		{
			return new Boolint(initialValue ? 1 : -1);
		}
		public static implicit operator Boolint(int initialValue)
		{
			return new Boolint(initialValue);
		}

		public static bool operator ==(Boolint left, Boolint right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Boolint left, Boolint right)
		{
			return !(left == right);
		}

		public static implicit operator int(Boolint boolint)
		{
			return boolint.value;
		}

		public int ToInt()
		{
			return value;
		}
		public bool ToBool()
		{
			return value == 1;
		}

		public string ToString(string format)
		{
			if (format == "YN")
			{
				return value == 1 ? "YES" : "NO";
			}
			else
			{
				return value.ToString(); // atau format lainnya yang Anda inginkan
			}
		}

		public override string ToString()
		{
			return value.ToString();
		}

		public int CompareTo(Boolint other)
		{
			return value.CompareTo(other.value);
		}

		public int CompareTo(object? obj)
		{
			if (obj == null) return 1;

			if (obj is Boolint otherBoolint)
			{
				return value.CompareTo(otherBoolint.value);
			}
			else
			{
				throw new ArgumentException("Object is not a Boolint");
			}
		}


		// Implementasi IEquatable<Boolint>
		public override bool Equals(object? other)
		{
			return other is Boolint otherBoolint && Equals(otherBoolint);
		}
		public bool Equals(Boolint other)
		{
			return value == other.value;
		}

		// Implementasi IParsable<Boolint>
		public static Boolint Parse(int? s, IFormatProvider? provider)
		{
			if (s == 1)
				return new Boolint(1);
			if (s == -1)
				return new Boolint(-1);
			throw new FormatException("Input string was not in a correct format.");
		}
		public static Boolint Parse(string s, IFormatProvider? provider)
		{
			if (s == "YES" || s == "1")
				return new Boolint(1);
			if (s == "NO" || s == "-1")
				return new Boolint(-1);
			throw new FormatException("Input string was not in a correct format.");
		}

		public static bool TryParse(string? s, IFormatProvider? provider, out Boolint result)
		{
			result = default;
			if (string.IsNullOrEmpty(s))
				return false;

			if (s == "YES" || s == "1")
			{
				result = new Boolint(1);
				return true;
			}
			if (s == "NO" || s == "-1")
			{
				result = new Boolint(-1);
				return true;
			}
			return false;
		}

		// Implementasi ISpanParsable<Boolint>
		public static Boolint Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
		{
			return Parse(s.ToString(), provider);
		}

		public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Boolint result)
		{
			return TryParse(s.ToString(), provider, out result);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		public bool ToBoolean(IFormatProvider? provider)
		{
			return value == 1;
		}

		public byte ToByte(IFormatProvider? provider)
		{
			return Convert.ToByte(value);
		}

		public char ToChar(IFormatProvider? provider)
		{
			throw new InvalidCastException("Boolint cannot be converted to Char.");
		}

		public DateTime ToDateTime(IFormatProvider? provider)
		{
			throw new InvalidCastException("Boolint cannot be converted to DateTime.");
		}

		public decimal ToDecimal(IFormatProvider? provider)
		{
			return Convert.ToDecimal(value);
		}

		public double ToDouble(IFormatProvider? provider)
		{
			return Convert.ToDouble(value);
		}

		public short ToInt16(IFormatProvider? provider)
		{
			return Convert.ToInt16(value);
		}

		public int ToInt32(IFormatProvider? provider)
		{
			return value;
		}

		public long ToInt64(IFormatProvider? provider)
		{
			return Convert.ToInt64(value);
		}

		public sbyte ToSByte(IFormatProvider? provider)
		{
			return Convert.ToSByte(value);
		}

		public float ToSingle(IFormatProvider? provider)
		{
			return Convert.ToSingle(value);
		}

		public string ToString(IFormatProvider? provider)
		{
			return ToString();
		}

		public object ToType(Type conversionType, IFormatProvider? provider)
		{
			if (conversionType == typeof(Boolint))
				return this;
			else if (conversionType == typeof(string))
				return ToString(provider);
			else if (conversionType == typeof(bool))
				return ToBoolean(provider);
			else
				throw new InvalidCastException($"Conversion to {conversionType.Name} is not supported.");
		}

		public ushort ToUInt16(IFormatProvider? provider)
		{
			return Convert.ToUInt16(value);
		}

		public uint ToUInt32(IFormatProvider? provider)
		{
			return Convert.ToUInt32(value);
		}

		public ulong ToUInt64(IFormatProvider? provider)
		{
			return Convert.ToUInt64(value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator <(Boolint left, Boolint right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(Boolint left, Boolint right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >(Boolint left, Boolint right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(Boolint left, Boolint right)
		{
			return left.CompareTo(right) >= 0;
		}
	}

}