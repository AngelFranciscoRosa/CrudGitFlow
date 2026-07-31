using Proyecto_Ventas.Data;
using Proyecto_Ventas.Entities;
using System.Collections.Generic;

namespace Proyecto_Ventas.Negocio
{
    public class ProductoBLL
    {
        private readonly ProductoDAO dao = new ProductoDAO();

        public void Insertar(Producto producto)
        {
            dao.Insertar(producto);
        }

        public void Actualizar(Producto producto)
        {
            dao.Actualizar(producto);
        }

        public void Eliminar(int id)
        {
            dao.Eliminar(id);
        }

        public List<Producto> Listar()
        {
            return dao.Listar();
        }

        public List<Producto> Buscar(string nombre)
        {
            return dao.Buscar(nombre);
        }
    }
}