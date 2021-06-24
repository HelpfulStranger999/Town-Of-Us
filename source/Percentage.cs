using System;

namespace TownOfUs
{
    public struct Percentage
    {
        public float Value { get; }

        public Percentage(float value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be normalized between 0 and 1");

            Value = value;
        }

        public Percentage(int value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 100");

            Value = value / 100.0f;
        }

        public override bool Equals(object obj)
        {
            if (obj is Percentage percent)
                return percent.Value == Value;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator float(Percentage percent) => percent.Value;
        public static explicit operator Percentage(float value) => new Percentage(value);
        public static explicit operator Percentage(int value) => new Percentage(value);

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !(left == right);
        }
    }
}