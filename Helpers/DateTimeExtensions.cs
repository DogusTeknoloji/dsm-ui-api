using System;

namespace DSM.UI.Api.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime DefaultSqlDateTime(this DateTime dateTime)
        {
            return new DateTime(1900, 01, 01);
        }
    }
}
