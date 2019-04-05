using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;

namespace FashFuns.Domain.Entities.Identity {
   
    [JsonConverter(typeof(StringEnumConverter<,,>))]
    public enum RoleType
    {
        Administrator = 0,
        Buyer = 1,
        Designer = 2,
        Seller = 3,
        Store = 4,
    }
}
