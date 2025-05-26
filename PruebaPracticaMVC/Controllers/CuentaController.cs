using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaPracticaMVC.Models;
using System.Net.Http.Headers;
using System.Text;

public class CuentaController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CuentaController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var client = _httpClientFactory.CreateClient();

        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7064/api/Auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            //var token = await response.Content.ReadAsStringAsync();

            // Guarda el token en sesión o cookie segura
            var token = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonConvert.DeserializeObject<TokenResponseModel>(token);

            HttpContext.Session.SetString("JWToken", tokenObj.Token);
            return RedirectToAction("Index", "Empleados");
        }

        ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JWToken");
        return RedirectToAction("Login");
    }
}
