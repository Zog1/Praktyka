using AutoMapper;
using System;

namespace CarRental.Services.Formatters
{
    class DateFormatter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime sourceMember, ResolutionContext context)
            => sourceMember.Date.ToString("yyyy-MM-dd");
    }
}
