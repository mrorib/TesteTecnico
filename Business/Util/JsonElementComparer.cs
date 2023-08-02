using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace TesteTecnico.Business.Util
{
    public class JsonElementComparer : IEqualityComparer<JsonElement>
    {
        public bool Equals(JsonElement x, JsonElement y)
        {
            return (Convert.ToString(x) + x.ValueKind) == (Convert.ToString(y) + y.ValueKind);
        }

        public int GetHashCode([DisallowNull] JsonElement obj)
        {
            return (Convert.ToString(obj) + obj.ValueKind).GetHashCode();
        }
    }
}
