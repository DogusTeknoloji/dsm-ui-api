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
    }
}