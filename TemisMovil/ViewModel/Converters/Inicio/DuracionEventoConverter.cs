using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TemisMovil.Model;
using Xamarin.Forms;

namespace TemisMovil.ViewModel.Converters.Inicio
{
    public class DuracionEventoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var evento = (Evento)value;
                TimeSpan interval = evento.FechaFin - evento.FechaInicio;

                if (interval.TotalDays > 1)
                    return $"{interval.TotalDays:0} días";
                else
                {
                    if (interval.TotalSeconds < 60)
                        return $"{interval.TotalSeconds:0} segundos";

                    if (interval.TotalMinutes < 60)
                        return $"{interval.TotalMinutes:0} minutos";

                    if (interval.TotalHours < 60)
                        return $"{interval.TotalHours:0} horas";

                    return " ayer";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
