using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeneradorContraseñasAleatorias.Models;

namespace GeneradorContraseñasAleatorias.Controllers;

public class HomeController : Controller{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger){
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index(){
        int num = 0;
        string pass = "";
        HttpContext.Session.SetInt32("num", num);
        HttpContext.Session.SetString("pass", pass);
        return View("Index");
    }

    [HttpPost("procesa/password")]
    public IActionResult ProcesaPassword(){
        string pass = "";
        int num = (int)HttpContext.Session.GetInt32("num");
        num++;
        HttpContext.Session.SetInt32("num", num);
        for (int i = 0; i < 14; i++){        
            int valor = new Random().Next(2);
            if (valor > 0){
                int randomNumber = new Random().Next(10);
                pass += randomNumber.ToString();
            }
            else{
                int randomNumber = new Random().Next(65, 91);
                pass += (char)randomNumber;
            }
        }
        HttpContext.Session.SetString("pass", pass);
        return View("Index");
    }

    [HttpPost("reset")]
    public IActionResult Reset(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
