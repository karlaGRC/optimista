using Azure;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using KGRC_Evaluacion.Repositories;
using KGRC_Evaluacion.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using System.Text.Json;
namespace KGRC_Evaluacion.Controllers
{
    [Route("[controller]")]

    public class BusquedaCoctelController : Controller
    {
        private readonly IRestClient _restClient;
        private readonly IUnitOfWork UnitOfWork;
        private readonly CoctelServices _CoctelService;
        private string? strResponse;
        public BusquedaCoctelController(CoctelServices CoctelService, IUnitOfWork UnitOfWork, IRestClient _restClient)
        {
            this._CoctelService = CoctelService;
            this.UnitOfWork = UnitOfWork;
            this._restClient = _restClient;

        }
       
        public IActionResult Index()
        {

            List<ViewResponseFind> model = new List<ViewResponseFind>();
            return View(model);

        }
        [HttpGet("CoctelFinder")]
        public async Task<IActionResult> CoctelFinder(string? CoctelFind)
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUser") ?? default(int);

            AgregarBitacora(CoctelFind, idUsuario);
            var cocteles = await GetCoctelFinder(CoctelFind);

            return View("Index",cocteles);
        }

        [HttpGet("GetCoctelFinder")]
        public async Task<List<ViewResponseFind>> GetCoctelFinder(string? CoctelFind)
        {

            int idUsuario = HttpContext.Session.GetInt32("IdUser") ?? -1;
            strResponse = "";
            List<ViewResponseFind> viewResponseFinds = new List<ViewResponseFind>();
            ViewResponseFind viewResponseFind;

            if (string.IsNullOrEmpty(CoctelFind))
            {
                return viewResponseFinds;
            }
            else
            {
                try
                {

                    strResponse = await _restClient.SendRequestWithParameters("", "search.php?s=", HttpMethodToUse.GET, CoctelFind);
                    var result = JsonSerializer.Deserialize<DrinksResponse>(strResponse);
                    if (string.IsNullOrEmpty(strResponse))
                    {
                        try
                        {
                            strResponse = await _restClient.SendRequestWithParameters("", "filter.php?i=", HttpMethodToUse.GET, CoctelFind);
                            var result2 = JsonSerializer.Deserialize<DrinksResponseFind>(strResponse);
                            if (result2 != null && result2.Drinks != null && result2.Drinks.Count > 0)
                            {

                                foreach (var Drink in result2.Drinks)
                                {

                                    viewResponseFind = new ViewResponseFind()
                                    {
                                        idDrink = Drink.idDrink != null ? Drink.idDrink : 0,
                                        strDrink = Drink.strDrink != null ? Drink.strDrink : "",
                                        strDrinkThumb = Drink.strDrinkThumb != null ? Drink.strDrinkThumb : ""

                                    };
                                    viewResponseFinds.Add(viewResponseFind);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            
                            ConsolaMensaje("Error al Consumir el servicio de busqueda por ingrediente", ex);
                            return new List<ViewResponseFind>();
                        }
                    }
                    else
                    {
                        if (result != null && result.Drinks != null && result.Drinks.Count > 0)
                        {

                            foreach (var Drink in result.Drinks)
                            {

                                viewResponseFind = new ViewResponseFind()
                                {
                                    idDrink = Drink.idDrink != null ? int.Parse(Drink.idDrink) : 0,
                                    strDrink = Drink.strDrink != null ? Drink.strDrink : "",
                                    strDrinkThumb = Drink.strDrinkThumb != null ? Drink.strDrinkThumb : ""

                                };
                                viewResponseFinds.Add(viewResponseFind);
                            }
                            return viewResponseFinds;
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                    ConsolaMensaje("Error al ObtenerCoctelFavorito", ex);
                    return viewResponseFinds;
                }
                return viewResponseFinds;
            }

        }

        [HttpPost("AgregarBitacora")]
        public IActionResult AgregarBitacora(string? CoctelFind, int idUsuario)
        {
            UnitOfWork.Bitacora.AgregarBitacora(CoctelFind, idUsuario);
            return View();
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("IdUser");

            return RedirectToAction("Index", "Home");
        }
        [HttpPost("CoctelFavorito")]
        public IActionResult CoctelFavorito()
        {

            return RedirectToAction("Index", "Coctel");
        }

        [HttpPost("AddCoctelesFavorito")]
        public async Task<IActionResult> AddCoctelesFavorito(List<string> chkAgregar)
        {

            try
            {
                foreach (string c in chkAgregar)
                {

                    int idUsuario = HttpContext.Session.GetInt32("IdUser") ?? -1;
                    int iDrink = int.Parse(c);
                    var result = await UnitOfWork.Coctel.GetInformationCoctelUser(idUsuario, iDrink);

                    if (result.Count == 0)
                    {
                        await UnitOfWork.Coctel.SaveCoctel(int.Parse(c), idUsuario);
                    }

                }
                TempData["SuccessMessage"] = "Los cócteles han sido agregados a favoritos.";
            }
            catch (Exception ex)
            {
                ConsolaMensaje("Error al AgregarCoctelesFavorito:", ex);

            }

            return RedirectToAction("Index", "BusquedaCoctel");


        }
        [HttpPost]
        public void ConsolaMensaje(string mensaje, Exception ex)
        {
            Console.WriteLine($"{mensaje}: {ex.Message}");
        }

    }
}
