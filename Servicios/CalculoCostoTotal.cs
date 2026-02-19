namespace Sistema_de_Reserva_de_Hoteles.Servicios
{
    public class CalculoCostoTotal
    {
        public static double CalculoCosto(double PrecioHab, int NumHab, int Dias)
        { 
            return PrecioHab * NumHab * Dias;
        }
    }
}
