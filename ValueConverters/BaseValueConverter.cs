using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ChatApplication
{
    /// <summary>
    /// A base value converter that all other value converters will impliment from, to provide direct XAML acess
    /// </summary>
    /// <typeparam name="T">The type of this value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private members

        //A single static instance of this value converter
        private static T _converter = null;

        #endregion

        /// <summary>
        /// Provides a static instance of the value converter 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        #region Value converters methods

        /// <summary>
        /// The method to convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
