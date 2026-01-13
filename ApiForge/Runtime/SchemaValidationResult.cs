namespace ApiForge.Runtime
{
    public class SchemaValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public IReadOnlyList<string> Errors { get; }

        public SchemaValidationResult(IEnumerable<string> errors)
        {
            Errors = errors.ToList();
        }

        public static SchemaValidationResult Success()
            => new SchemaValidationResult(Array.Empty<string>());
    }
}
