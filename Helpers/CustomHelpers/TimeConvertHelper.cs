using System;

namespace DSM.UI.Api.Helpers.CustomHelpers
{
    public static class TimeConvertHelper
    {
        public static string convertDayToTurkish(this string day)
        {
            return day switch
            {
                "Monday" => "Pazartesi",
                "Tuesday" => "Salı",
                "Wednesday" => "Çarşamba",
                "Thursday" => "Perşembe",
                "Friday" => "Cuma",
                "Saturday" => "Cumartesi",
                "Sunday" => "Pazar",
                _ => "Pazartesi"
            };
        }

        public static string convertMonthToTurkish(this int month)
        {
            return month switch
            {
                1 => "Ocak",
                2 => "Şubat",
                3 => "Mart",
                4 => "Nisan",
                5 => "Mayıs",
                6 => "Haziran",
                7 => "Temmuz",
                8 => "Ağustos",
                9 => "Eylül",
                10 => "Ekim",
                11 => "Kasım",
                12 => "Aralık",
                _ => "Ocak"
            };
        }

        public static int compareMonths(string month1, string month2)
        {
            string[] months =
            {
                "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım",
                "Aralık"
            };
            var month1Index = Array.IndexOf(months, month1);
            var month2Index = Array.IndexOf(months, month2);
            return month1Index - month2Index;
        }

        public static int getMonthNumber(string month)
        {
            return month switch
            {
                "Ocak" => 1,
                "Şubat" => 2,
                "Mart" => 3,
                "Nisan" => 4,
                "Mayıs" => 5,
                "Haziran" => 6,
                "Temmuz" => 7,
                "Ağustos" => 8,
                "Eylül" => 9,
                "Ekim" => 10,
                "Kasım" => 11,
                "Aralık" => 12,
                _ => 0
            };
        }
    }
}