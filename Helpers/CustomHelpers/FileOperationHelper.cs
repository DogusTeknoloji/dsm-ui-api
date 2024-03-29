﻿using System;

namespace DSM.UI.Api.Helpers.CustomHelpers
{
    public static class FileOperationHelper
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {
            return
                $"{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }

        public static string CalculateFileSize(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            
            if (byteCount == 0)
                return "0" + suf[0];
            
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}