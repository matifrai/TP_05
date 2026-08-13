using Dapper;
using Microsoft.Data.SqlClient;
using TP_05.Models;

namespace TP_05.Models;

public class BD{
    //Conexion a la base de datos
    private string _connectionString = @"Server=localhost;Database=TP_05;
    Integrated Security=True;TrustServerCertificate=True;";


    public void AgregarUsuario(Usuarios usuario){
        string query = "INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombre, Apellido, TipoUsuario) VALUES (@pNombreUsuario, @pContraseña, @pNombre, @pApellido, @pTipoUsuario)";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            connection.Execute(query, new { pNombreUsuario = usuario.NombreUsuario, pContraseña = usuario.Contraseña, pNombre = usuario.Nombre, pApellido = usuario.Apellido, pTipoUsuario = usuario.TipoUsuario });
        }
    }

    public List<Usuarios> ObtenerUsuarios(){
        List<Usuarios> usuarios = new List<Usuarios>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Usuarios";
            usuarios = connection.Query<Usuarios>(query).ToList();
        }
        return usuarios;
    }

    public Usuarios ObtenerUsuarioPorId(int Id){
        Usuarios usuario = null;
        string query = "SELECT * FROM Usuarios WHERE Id = @pId";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { pId = Id });
        }
        return usuario;
    }

}