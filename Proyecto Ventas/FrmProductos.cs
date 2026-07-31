using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Ventas.Entities;
using Proyecto_Ventas.Negocio;

namespace Proyecto_Ventas
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoBLL bll = new ProductoBLL();

        private int idSeleccionado = 0;

        public FrmProductos()
        {
            InitializeComponent();
        }
        //metodo para cargar los productos en el datagridview
        private void CargarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = bll.Listar();
        }
        //metodo para limpiar los textbox y resetear el idSeleccionado
        private void Limpiar()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            idSeleccionado = 0;

            txtNombre.Focus();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.");
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar la descripción del producto.");
                txtDescripcion.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.");
                txtPrecio.Focus();
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que cero.");
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad))
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                txtCantidad.Focus();
                return;
            }

            if (cantidad < 0)
            {
                MessageBox.Show("La cantidad no puede ser negativa.");
                txtCantidad.Focus();
                return;
            }

            //validacion de precio
            if (decimal.Round(precio, 2) != precio)
            {
                MessageBox.Show("El precio solo puede tener hasta dos decimales.");
                txtPrecio.Focus();
                return;
            }

            Producto producto = new Producto();

            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = precio;
            producto.Cantidad = cantidad;

            bll.Insertar(producto);

            MessageBox.Show("Producto guardado correctamente.");

            CargarProductos();

            Limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.");
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar la descripción del producto.");
                txtDescripcion.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.");
                txtPrecio.Focus();
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que cero.");
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad))
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                txtCantidad.Focus();
                return;
            }

            if (cantidad < 0)
            {
                MessageBox.Show("La cantidad no puede ser negativa.");
                txtCantidad.Focus();
                return;
            }

            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            Producto producto = new Producto();

            producto.Id = idSeleccionado;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = precio;
            producto.Cantidad = cantidad;

            bll.Actualizar(producto);

            MessageBox.Show("Producto actualizado correctamente.");

            CargarProductos();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar este producto?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                bll.Eliminar(idSeleccionado);

                MessageBox.Show("Producto eliminado correctamente.");

                CargarProductos();
                Limpiar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            List<Producto> resultados = bll.Buscar(txtNombre.Text);

            if (resultados.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron productos.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarProductos();
                txtNombre.Clear();
                txtNombre.Focus();
                return;
            }

            dgvProductos.DataSource = resultados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarProductos();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);

                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
                txtCantidad.Text = fila.Cells["Cantidad"].Value.ToString();
            }
        }
    }
}
