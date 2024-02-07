using AutoFixture.Kernel;
using System.Reflection;

namespace IntegrationTests.Mocks;

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
