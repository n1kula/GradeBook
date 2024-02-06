using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gradebook.Infrastructure.Config.Convertors
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base (
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d)
            )
        {
        }
    }
}
