using System;
using System.Runtime.Serialization;
using System.Text;

namespace Application.Common.MethodExtensions
{
    public class StringBuilderExtension
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public override string ToString() => _builder.ToString();

        //StringBuilderExtension teste = "first,";
        public static implicit operator StringBuilderExtension(string s)
        {
            var builder = new StringBuilderExtension();
            builder._builder.Append(s);
            return builder;
        }
        //teste += "second";
        public static StringBuilderExtension operator +(StringBuilderExtension builder, string s)
        {
            builder.Append(s);
            return builder;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (_builder is ISerializable b)
            {
                b?.GetObjectData(info, context);
            }
        }
        public int EnsureCapacity(int capacity) => _builder.EnsureCapacity(capacity);

        public string ToString(int startIndex, int length) => _builder.ToString(startIndex, length);

        public StringBuilderExtension Clear()
        {
            _builder.Clear();
            return this;
        }
        public StringBuilderExtension Append(char value, int repeatCount)
        {
            _builder.Append(value, repeatCount);
            return this;
        }
        public StringBuilderExtension Append(char[] value, int startIndex, int charCount)
        {
            _builder.Append(value, startIndex, charCount);
            return this;
        }
        public StringBuilderExtension Append(string value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(string value, int startIndex, int count)
        {
            _builder.Append(value, startIndex, count);
            return this;
        }
        public StringBuilderExtension AppendLine()
        {
            _builder.AppendLine();
            return this;
        }
        public StringBuilderExtension AppendLine(string value)
        {
            _builder.AppendLine(value);
            return this;
        }
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            _builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }
        public StringBuilderExtension Insert(int index, string value, int count)
        {
            _builder.Insert(index, value, count);
            return this;
        }
        public StringBuilderExtension Remove(int startIndex, int length)
        {
            _builder.Remove(startIndex, length);
            return this;
        }
        public StringBuilderExtension Append(bool value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(sbyte value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(byte value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(char value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(short value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(int value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(long value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(float value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(double value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(decimal value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(ushort value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(uint value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(ulong value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(object value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Append(char[] value)
        {
            _builder.Append(value);
            return this;
        }
        public StringBuilderExtension Insert(int index, string value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, bool value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, sbyte value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, byte value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, short value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, char value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, char[] value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, char[] value, int startIndex, int charCount)
        {
            _builder.Insert(index, value, startIndex, charCount);
            return this;
        }
        public StringBuilderExtension Insert(int index, int value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, long value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, float value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, double value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, decimal value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, ushort value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, uint value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, ulong value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension Insert(int index, object value)
        {
            _builder.Insert(index, value);
            return this;
        }
        public StringBuilderExtension AppendFormat(string format, object arg0)
        {
            _builder.AppendFormat(format, arg0);
            return this;
        }
        public StringBuilderExtension AppendFormat(string format, object arg0, object arg1)
        {
            _builder.AppendFormat(format, arg0, arg1);
            return this;
        }
        public StringBuilderExtension AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            _builder.AppendFormat(format, arg0, arg1, arg2);
            return this;
        }
        public StringBuilderExtension AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }
        public StringBuilderExtension AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            _builder.AppendFormat(provider, format, args);
            return this;
        }
        public StringBuilderExtension Replace(string oldValue, string newValue)
        {
            _builder.Replace(oldValue, newValue);
            return this;
        }
        public bool Equals(StringBuilderExtension sb) => _builder.Equals(sb);

        public StringBuilderExtension Replace(string oldValue, string newValue, int startIndex, int count)
        {
            _builder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }
        public StringBuilderExtension Replace(char oldChar, char newChar)
        {
            _builder.Replace(oldChar, newChar);
            return this;
        }
        public StringBuilderExtension Replace(char oldChar, char newChar, int startIndex, int count)
        {
            _builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }
        public int Capacity
        {
            get => _builder.Capacity;
            set => _builder.Capacity = value;
        }
        public int MaxCapacity => _builder.MaxCapacity;
        public int Length
        {
            get => _builder.Length;
            set => _builder.Length = value;
        }
        public char this[int index]
        {
            get => _builder[index];
            set => _builder[index] = value;
        }

    }
}
