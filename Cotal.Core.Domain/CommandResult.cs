using System;
using System.Collections.Generic;
using Cotal.Core.Domain.Interfaces;

namespace Cotal.Core.Domain
{
    public class CommandResult : IResult
    {
        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }

        public IList<string> ErrorResults { get; set; }

        public static IResult ErrorResult(params string[] messages)
        {
            return new CommandResult(false)
            {
                ErrorResults = messages
            };
        }
    }
}