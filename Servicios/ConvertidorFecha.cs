namespace Sistema_de_Reserva_de_Hoteles.Servicios
{
    public class ConvertidorFecha
    {
        public static DateTime ConvertirStringAFecha(string fechaString)
        {
            DateTime fechaConvertida;
            bool exitoConversion = DateTime.TryParse(fechaString, out fechaConvertida);
            if (!exitoConversion)
            {
                throw new FormatException("El formato de la fecha es inválido.");
            }
            return fechaConvertida;
        }
    }
}
