﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Cafe;

public class BitmapAssetValueConverter : IValueConverter
{
    public static BitmapAssetValueConverter Instance = new BitmapAssetValueConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        if (value is string rawUri && targetType.IsAssignableFrom(typeof(Bitmap)))
        {
            Uri uri;

            // Allow for assembly overrides
            if (rawUri.StartsWith("avares://"))
            {
                uri = new Uri(rawUri);
            }
            else
            {
                string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                uri = new Uri($"avares://{assemblyName}/{rawUri}");
            }

            try
            {
                var asset = AssetLoader.Open(uri);
                return new Bitmap(asset);
            }
            catch (Exception e)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка", $"{e}", ButtonEnum.Ok, Icon.Error).ShowAsync();
            }
        }

        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}