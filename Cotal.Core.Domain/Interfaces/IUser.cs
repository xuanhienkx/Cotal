using System.Collections.Generic;
using System.Security.Claims;

namespace Cotal.Core.Domain.Interfaces
{
    public interface IUser
    {            
        int Id { get; }
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}