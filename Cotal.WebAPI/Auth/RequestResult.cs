﻿using System;

namespace Cotal.WebAPI.Auth
{
    public class RequestResult
    {
        public RequestState State { get; set; }
        public string Msg { get; set; }
        public Object Data { get; set; }
    }

    public enum RequestState
    {
        Failed = -1,
        NotAuth = 0,
        Success = 1
    }
}