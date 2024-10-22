using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_LIMPIEZA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Conexion()
        {
            // Cadena de conexión a MySQL
            string connectionString = "Server=localhost;Database=proyecto_limpieza;Uid=root;Pwd=vengeance;";

            // Intentar conectarse
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos MySQL.");

                   
                    string query = "SELECT * FROM administrador";

                    // Crear el comando
                    MySqlCommand command = new MySqlCommand(query, connection);

                 

                    // Cerrar la conexión
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
               MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=proyecto_limpieza;Uid=root;Pwd=vengeance;";

            // Obtener el nombre de usuario y contraseña ingresados
            string nombreUsuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.");
                return;
            }

            // Crear la conexión a MySQL
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Consulta SQL para validar el nombre de usuario y contraseña
                    string query = "SELECT COUNT(*) FROM administrador WHERE UsuarioAdministrador = @UsuarioAdministrador AND UsuarioContrasenia = @UsuarioContrasenia";

                    // Crear el comando con los parámetros
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UsuarioAdministrador", nombreUsuario);
                    command.Parameters.AddWithValue("@UsuarioContrasenia", contraseña);

                    // Ejecutar la consulta
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount > 0)
                    {
                        // Si se encuentra un usuario, abrir el formulario principal
                        MessageBox.Show("Inicio de sesión exitoso.");

                        // Abrir el formulario principal
                        FormPrincipal formPrincip = new FormPrincipal();
                        formPrincip.Show();

                        // Ocultar o cerrar el formulario de inicio de sesión
                        this.Hide(); // Puedes usar this.Close() para cerrar el formulario de login
                    }
                    else
                    {
                        // Si no se encuentra el usuario
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejar cualquier error de conexión
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}