namespace NextTrain.Client
{
    public readonly struct Crs
    {
        internal Crs(string code, string? friendlyName = null)
        {
            Code = code.ToUpper();
            FriendlyName = friendlyName;
        }

        public string? FriendlyName { get; }

        public string Code { get; }

        public override string ToString()
            => FriendlyName ?? Code;

        public static implicit operator string(Crs crs)
            => crs.Code;

        public static implicit operator Crs(string code)
            => new(code);

        public static Crs Nottingham => new("NOT", "Nottingham");
        public static Crs Sheffield => new("SHF", "Sheffield");
        public static Crs Beeston => new("BEE", "Beeston");
        public static Crs Derby => new("DBY", "Derby");
        public static Crs EastMidlandsParkway => new("EMD", "East Midlands Parkway");

        public override int GetHashCode()
            => Code.GetHashCode();

        public override bool Equals(object? obj)
            => obj is Crs crs && Equals(crs) ||
               obj is string code && Equals(code);

        public bool Equals(Crs other)
            => Equals(other.Code);

        public bool Equals(string code)
            => StringComparer.OrdinalIgnoreCase.Equals(Code, code);

        public static bool operator ==(Crs left, Crs right)
            => left.Equals(right);

        public static bool operator !=(Crs left, Crs right)
            => !left.Equals(right);
    }
}