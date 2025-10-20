
namespace MSCupons.domain.Entity
{
    public enum TipoDescuento { Porcentaje, MontoFijo }
    public enum EstadoCupon { Activo, Inactivo, Expirado }

    public class Cupon
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public TipoDescuento TipoDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int UsoMaximo { get; set; }
        public int UsoActual { get; set; }
        public EstadoCupon Estado { get; set; }

        public bool EsValido()
        {
            return Estado == EstadoCupon.Activo &&
                   FechaInicio <= DateTime.Now &&
                   FechaExpiracion >= DateTime.Now &&
                   UsoActual < UsoMaximo;
        }

        public decimal CalcularDescuento(decimal monto)
        {
            if (TipoDescuento == TipoDescuento.Porcentaje)
                return monto * (Descuento / 100);
            else
                return Descuento;
        }

        public decimal AplicarCupon(decimal monto)
        {
            if (!EsValido()) return monto;
            return monto - CalcularDescuento(monto);
        }

        public void MarcarUso()
        {
            UsoActual++;
            if (UsoActual >= UsoMaximo)
                Estado = EstadoCupon.Inactivo;
        }

        public void Desactivar() => Estado = EstadoCupon.Inactivo;
        public void Reactivar() => Estado = EstadoCupon.Activo;
        public string InfoCupon() => $"{Codigo} - {Descripcion} ({Descuento}{(TipoDescuento == TipoDescuento.Porcentaje ? "%" : "$")})";
    }
}

