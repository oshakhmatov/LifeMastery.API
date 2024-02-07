using AutoFixture.Kernel;
using System.Reflection;

namespace UnitTests.Common;

public class DateOnlySpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var pi = request as PropertyInfo;
        if (pi != null && pi.PropertyType == typeof(DateOnly))
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        return new NoSpecimen();
    }
}
