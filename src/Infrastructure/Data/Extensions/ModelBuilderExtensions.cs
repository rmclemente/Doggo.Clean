using Domain.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Using Fluent API command IsUnicode(false), will turn the SQL type from NVARCHAR to VARCHAR.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SetStringsAsNonUnicodeGlobally(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var entityProperty in entityType.GetProperties())
                    if (entityProperty.ClrType == typeof(string))
                        entityProperty.SetIsUnicode(false);
        }

        /// <summary>
        /// Set Max Length for string properties globally
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="propertyName"></param>
        /// <param name="maxLength"></param>
        public static void SetStringPropertyMaxLengthGlobally(this ModelBuilder modelBuilder, string propertyName, int maxLength)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var entityProperty in entityType.GetProperties())
                    if (entityProperty.ClrType == typeof(string) && entityProperty.Name == propertyName)
                        entityProperty.SetMaxLength(maxLength);
        }

        /// <summary>
        /// Using Fluent API command to set the SQL type to DECIMAL(precision, scale).
        /// Ex.: 
        /// Precision: 9
        /// Scale: 2
        /// Type to DECIMAL(9,2)
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public static void SetDecimalPrecisionGlobally(this ModelBuilder modelBuilder, int precision, int scale)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var entityProperty in entityType.GetProperties())
                    if (entityProperty.ClrType == typeof(decimal))
                    {
                        entityProperty.SetPrecision(precision);
                        entityProperty.SetScale(scale);
                    }
        }
    }
}
