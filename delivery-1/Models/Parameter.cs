namespace Zadatak.Models
{
    class Parameter
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string DataType { get; set; }
        public override string ToString() => $"{Mode} {Name} ({DataType})";
    }
}
