using System.Collections.Generic;

namespace Cotal.Core.Domain.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }
        IList<string> ErrorResults { get; }
    }
}