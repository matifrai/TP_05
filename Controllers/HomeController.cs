using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_05.Models;

namespace TP_05.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string usuario, string contrasena)
    {
        BD bd = new BD();
        Usuarios usuarioEncontrado = bd.ObtenerUsuario(usuario, contrasena);

        if (usuarioEncontrado == null)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        HttpContext.Session.SetString("usuario", usuarioEncontrado.NombreUsuario);
        HttpContext.Session.SetString("nombre", usuarioEncontrado.Nombre);
        HttpContext.Session.SetString("apellido", usuarioEncontrado.Apellido);
        HttpContext.Session.SetString("tipoUsuario", usuarioEncontrado.TipoUsuario);

        return RedirectToAction("Bienvenida");
    }

    public IActionResult Registrarse()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrarse(string nombre, string apellido, string usuario, string contrasena, string tipoUsuario)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(usuario)
            || string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(tipoUsuario))
        {
            ViewBag.Error = "Todos los campos son obligatorios.";
            return View();
        }

        BD bd = new BD();
        Usuarios usuarioExistente = bd.ObtenerUsuarioPorNombre(usuario);

        if (usuarioExistente != null)
        {
            ViewBag.Error = "El nombre de usuario ya existe.";
            return View();
        }

        Usuarios nuevoUsuario = new Usuarios
        {
            Nombre = nombre,
            Apellido = apellido,
            NombreUsuario = usuario,
            Contraseña = contrasena,
            TipoUsuario = tipoUsuario
        };

        bd.AgregarUsuario(nuevoUsuario);
        return RedirectToAction("Login");
    }

    public IActionResult Bienvenida()
    {
        string usuario = HttpContext.Session.GetString("usuario");

        if (usuario == null || usuario == "")
        {
            return RedirectToAction("Login");
        }

        ViewBag.Usuario = usuario;
        return View();
    }

    public IActionResult PaginaPrivada()
    {
        string usuario = HttpContext.Session.GetString("usuario");

        if (usuario == null || usuario == "")
        {
            return RedirectToAction("Login");
        }

        BD bd = new BD();
        Usuarios usuarioActual = bd.ObtenerUsuarioPorNombre(usuario);

        if (usuarioActual == null)
        {
            return RedirectToAction("Login");
        }

        return View(usuarioActual);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
