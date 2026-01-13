using ApiForge.Models;

namespace ApiForge.Runtime
{
    public class EndpointRegistry
    {
        private readonly Dictionary<string, EndpointDefinition> _byPath = new();
        private readonly Dictionary<Guid, EndpointDefinition> _byId = new();

        public void Register(EndpointDefinition endpoint)
        {
            _byPath[endpoint.Path] = endpoint;
            _byId[endpoint.Id] = endpoint;
        }

        public EndpointDefinition? GetByPath(string path)
            => _byPath.TryGetValue(path, out var e) ? e : null;

        public EndpointDefinition? GetById(Guid id)
            => _byId.TryGetValue(id, out var e) ? e : null;

        public IReadOnlyCollection<EndpointDefinition> GetAll()
            => _byId.Values.ToList();

        public bool Update(Guid id, EndpointDefinition updated)
        {
            if (!_byId.TryGetValue(id, out var existing))
                return false;

            // jeśli zmienił się path → aktualizujemy lookup
            if (!string.Equals(existing.Path, updated.Path, StringComparison.OrdinalIgnoreCase))
            {
                _byPath.Remove(existing.Path);
                _byPath[updated.Path] = updated;
            }

            _byId[id] = updated;
            return true;
        }
        public bool Remove(Guid id)
        {
            if (!_byId.TryGetValue(id, out var existing))
                return false;

            _byId.Remove(id);
            _byPath.Remove(existing.Path);

            return true;
        }
    }
}