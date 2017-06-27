using System;
using Cotal.App.Data.Services;
using Cotal.App.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cotal.WebAPI.Controllers
{
    public abstract class CotalControllerBase<T> : Controller where T : Controller
    {
        private IErrorService _errorService;

        protected CotalControllerBase(ILoggerFactory loggerFactory, IErrorService errorService)
        {
            _errorService = errorService;
            Logger = loggerFactory.CreateLogger<T>();    
        }
        protected virtual ILogger Logger { get; }

        

        protected virtual void LogError(Exception ex)
        {
            try
            {
                Logger.LogError(ex.Message);
                var error = new Error
                {
                    CreatedDate = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                _errorService.Create(error);
                _errorService.Save();
            }
            catch (Exception xException)
            {
                Logger.LogError(xException.Message);
            }
        }

         

    }
}