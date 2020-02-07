using CleanArch.Application.Common.Interfaces;
using System;

namespace CleanArch.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
