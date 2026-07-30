
using Proyecto_Ventas.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Ventas.Data
{
    public class ProductoDAO
    {
        private readonly Conexion conexion = new Conexion();

        //metodo insertar
        public void Insertar(Producto producto)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //metodo listar 
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Producto p = new Producto();

                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Descripcion = dr["Descripcion"].ToString();
                    p.Precio = Convert.ToDecimal(dr["Precio"]);
                    p.Cantidad = Convert.ToInt32(dr["Cantidad"]);

                    lista.Add(p);
                }
            }

            return lista;
        }

        //metodo actualizar
        public void Actualizar(Producto producto)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", producto.Id);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        //metodo eliminar
        public void Eliminar(int id)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        //metodo buscar
        public List<Producto> Buscar(string nombre)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_BuscarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Producto p = new Producto();

                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Descripcion = dr["Descripcion"].ToString();
                    p.Precio = Convert.ToDecimal(dr["Precio"]);
                    p.Cantidad = Convert.ToInt32(dr["Cantidad"]);

                    lista.Add(p);
                }
            }

            return lista;
        }


    }


}