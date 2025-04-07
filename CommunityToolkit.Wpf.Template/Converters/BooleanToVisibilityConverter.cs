using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace CommunityToolkit.Wpf.Template.Converters;
/****************************************************************************
   Purpose      : 
       다양한 데이터 변환기(Converter) 클래스들을 제공합니다.
       주로 XAML 데이터 바인딩에서 Boolean 값을 Visibility 혹은 반전된 Boolean으로
       변환할 때 사용되며, UI 제어를 간편하게 만들어 줍니다.
       
       예:
       - BooleanToVisibilityConverter : true/false → Visible/Collapsed
       - InverseBooleanConverter      : true → false, false → true
       
   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                    
   Company      : $company$                                       
   Email        : $email$                                         
****************************************************************************/

/// <summary>
/// Boolean 값을 Visibility로 변환합니다.
/// true → Visible, false → Collapsed
/// parameter가 "Invert"인 경우 반대 동작 수행 (true → Collapsed)
/// </summary>
public class BooleanToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// bool → Visibility로 변환합니다. 필요 시 "Invert" 파라미터를 사용하여 반전 처리도 지원합니다.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            // Invert 파라미터가 전달된 경우 반전된 Visibility 반환
            if (parameter is string param && param == "Invert")
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }

            // 기본: true → Visible, false → Collapsed
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed; // 기본 fallback
    }

    /// <summary>
    /// Visibility → bool로 변환합니다. "Invert" 파라미터 사용 시 반대로 해석합니다.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            if (parameter is string param && param == "Invert")
            {
                return visibility != Visibility.Visible;
            }

            return visibility == Visibility.Visible;
        }

        return false; // 기본 fallback
    }
}

/// <summary>
/// Boolean 값을 반전된 Boolean 값으로 변환합니다.
/// true → false, false → true
/// </summary>
public class InverseBooleanConverter : IValueConverter
{
    /// <summary>
    /// bool 값을 반전시켜 반환합니다.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return true; // 기본 fallback
    }

    /// <summary>
    /// ConvertBack도 동일하게 반전된 bool 값을 반환합니다.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return true; // 기본 fallback
    }
}
