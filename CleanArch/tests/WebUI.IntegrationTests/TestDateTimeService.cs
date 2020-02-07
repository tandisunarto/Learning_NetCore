using CleanArch.Application.Common.Interfaces;
using System;

namespace CleanArch.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
