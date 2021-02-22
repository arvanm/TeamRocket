using System;
using System.Globalization;
using Xamarin.Forms;
using Game.Models;

namespace Game.Helpers
{ 
    // This converter is changing ItemLocationEnum to a boolean representing whether it has Range
    // Only primary hand and Pokeball have damaes

    // Converts a ItemLocationEnum to Boolean. If ItemLocation is PrimaryHand or Pokeball, return True. Otherwise False.
    public class ItemLocationToHasRangeBoolConverterHelper : IValueConverter
    {
        /// <summary>
        /// Converts a ItemLocationEnum to Boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return ((ItemLocationEnum)value == ItemLocationEnum.PrimaryHand);
            }

            return 0;
        }

        /// <summary>
        /// Convert Back, throw not implemented error
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}