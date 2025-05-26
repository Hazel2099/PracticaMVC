using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaPracticaMVC.Models;
using System.Net.Http.Headers;

public class EmpleadosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EmpleadosController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Cuenta");

        // Mostrar el token para depurar (en consola de backend)
        Console.WriteLine("Token actual: " + token);

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("https://localhost:7064/api/Empleado");

        if (!response.IsSuccessStatusCode)
        {
            // También puedes imprimir aquí el código de error para depurar
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            return View("Error");
        }

        var json = await response.Content.ReadAsStringAsync();
        var empleados = JsonConvert.DeserializeObject<List<EmpleadoViewModel>>(json);

        return View(empleados);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> DetalleEmpleado(int id)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"https://localhost:7064/api/Empleado/{id}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var json = await response.Content.ReadAsStringAsync();
        var empleado = JsonConvert.DeserializeObject<EmpleadoViewModel>(json);

        return View(empleado); 
    }

    public IActionResult AgregarEmpleados() => View("AgregarEmpleados"); // Crear vista Crear.cshtml

    [HttpPost]
    public async Task<IActionResult> AgregarEmpleados(EmpleadoViewModel model)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("https://localhost:7064/api/Empleado", model);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> EditarEmpleado(int id)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"https://localhost:7064/api/Empleado/{id}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var json = await response.Content.ReadAsStringAsync();
        var empleado = JsonConvert.DeserializeObject<EmpleadoViewModel>(json);

        return View(empleado);
    }


    [HttpPost]
    public async Task<IActionResult> EditarEmpleado(EmpleadoViewModel model)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("https://localhost:7064/api/Empleado", model);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View("Error");
    }


    [HttpGet]
    public async Task<IActionResult> EliminarEmpleado(int id)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"https://localhost:7064/api/Empleado/{id}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var json = await response.Content.ReadAsStringAsync();
        var empleado = JsonConvert.DeserializeObject<EmpleadoViewModel>(json);

        return View(empleado); 
    }

    [HttpPost, ActionName("EliminarConfirmado")]
    public async Task<IActionResult> EliminarConfirmado(int id)
    {
        var token = HttpContext.Session.GetString("JWToken");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync($"https://localhost:7064/api/Empleado/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View("Error");
    }


}
