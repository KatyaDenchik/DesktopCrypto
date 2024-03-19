using DesktopCrypto.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace DesktopCrypto.Convertors
{
    [ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum))
                throw new InvalidCastException("The input value must be of type Enum.");

            return EnumHelper.GetAllValuesAndDescriptions(value.GetType());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ValueDescription selectedValueDescription))
                throw new InvalidCastException("The input value must be of type ValueDescription.");

            if (Enum.IsDefined(targetType, selectedValueDescription.Value))
            {
                return Enum.Parse(targetType, selectedValueDescription.Value.ToString());
            }

            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
