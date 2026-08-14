using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_05.Models;

namespace TP_05.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger){
        _logger = logger;
    }
    public IActionResult Privacy(){
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public IActionResult Index(){
        return View();
    }
    public IActionResult Login(){
        return View();
    }
    public IActionResult Registrarse(){
        return View();
    }
    public IActionResult Bienvenida(){
        return View();
    }

    public IActionResult PaginaPrivada(int id = 1)
    {
        var bd = new BD();
        var usuario = bd.ObtenerUsuarioPorId(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

}
