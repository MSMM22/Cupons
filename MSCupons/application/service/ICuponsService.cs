using MSCupons.domain.Entity;
using MSCupons.infrastructure.db;
using System.Collections.Generic;
using System.Linq;

namespace MSCupons.application.service
{
    public class CuponService
    {
        private readonly CuponDbContext _context;

        public CuponService(CuponDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cupon> ObtenerTodos() => _context.Cupones.ToList();

        public Cupon? ObtenerPorCodigo(string codigo) =>
            _context.Cupones.FirstOrDefault(c => c.Codigo == codigo);

        public void Agregar(Cupon cupon)
        {
            _context.Cupones.Add(cupon);
            _context.SaveChanges();
        }

        public decimal AplicarDescuento(string codigo, decimal monto)
        {
            var cupon = ObtenerPorCodigo(codigo);
            if (cupon == null || !cupon.EsValido()) return monto;

            cupon.MarcarUso();
            _context.SaveChanges();

            return cupon.AplicarCupon(monto);
        }
    }
}