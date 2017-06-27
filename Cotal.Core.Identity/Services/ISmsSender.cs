﻿using System.Threading.Tasks;

namespace Cotal.Core.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
