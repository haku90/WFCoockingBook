using System;
using System.ComponentModel;
using System.Globalization;

namespace UsingSwitchActivityInSequenceWorkflow
{
    public class ProductConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;
            if (value is string)
            {
                return new Product
                {
                    ProductName = (string) value,
                    ProductId = Guid.NewGuid()
                };
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof (string))
            {
                return value != null ? ((Product) value).ProductName : null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}