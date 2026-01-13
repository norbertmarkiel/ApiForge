using Microsoft.VisualBasic.FileIO;

namespace ApiForge.Models
{
    public class SchemaField
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
