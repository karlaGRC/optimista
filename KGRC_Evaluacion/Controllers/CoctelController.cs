using Azure;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using KGRC_Evaluacion.Repositories;
using KGRC_Evaluacion.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Text.Json;

namespace KGRC_Evaluacion.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CoctelController : Controller
    {
        
        private readonly CoctelServices _CoctelService;
        private readonly IRestClient _restClient;
   
        private string? strResponse;
        private IUnitOfWork UnitOfWork;

        public CoctelController(CoctelServices CoctelService,  IUnitOfWork UnitOfWork, IRestClient _restClient)
        {
            this._CoctelService = CoctelService;
            this.UnitOfWork = UnitOfWork;
            this._restClient = _restClient;

        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            
            List<ViewResponseCocteles> model = new List<ViewResponseCocteles>();
            var cocteles = ObtenerCoctelesFavorito();
            ViewResponseCocteles viewResponseCocteles;
            if (cocteles != null && cocteles.Result != null && cocteles.Result.Count > 0)
            {

                foreach (var coctele in cocteles.Result)
                {
                    viewResponseCocteles = new ViewResponseCocteles
                    {
                        idDrink = coctele.idDrink,
                        strDrink = coctele.strDrink,
                        strCategory = coctele.strCategory,
                        strAlcoholic = coctele.strAlcoholic,
                        strIngredient1 = coctele.strIngredient1,
                        strDrinkThumb = coctele.strDrinkThumb

                    };
                    model.Add(viewResponseCocteles);
                }
            }

            return View(model);
        }

        [HttpGet("ObtenerCoctelesFavorito")]
        public async Task<List<ViewResponseCocteles>> ObtenerCoctelesFavorito()
        {
            try
            {
                int idUsuario = HttpContext.Session.GetInt32("IdUser") ?? -1;
                var _ObtenerCoctelFavorito = await UnitOfWork.Coctel.GetInformationCoctel(idUsuario);

                List<ViewResponseCocteles> lstResponseCocteles = new List<ViewResponseCocteles>();
               
                strResponse = "";
                ViewResponseCocteles responseCocteles;

                foreach (var ResponseCoc in _ObtenerCoctelFavorito)
                {
                    strResponse = await _restClient.SendRequestWithParameters("", "lookup.php?i=", HttpMethodToUse.GET, ResponseCoc.idDrink);

                    if (string.IsNullOrEmpty(strResponse))
                    {
                        
                        ConsolaMensaje("La respuesta del servicio  es nula o vacía.");
                        continue;
                    }
                    try
                    {
                        var result = JsonSerializer.Deserialize<DrinksResponse>(strResponse);
                        if (result != null && result.Drinks != null && result.Drinks.Count > 0)
                        {

                            foreach (var Drink in result.Drinks)
                            {

                                responseCocteles = new ViewResponseCocteles
                                {
                                    idDrink = Drink.idDrink != null ? int.Parse(Drink.idDrink.ToString()):0,
                                    strDrink = Drink.strDrink != null ? Drink.strDrink: "",
                                    strCategory = Drink.strCategory != null ? Drink.strCategory: "",
                                    strAlcoholic = Drink.strAlcoholic != null ? Drink.strAlcoholic: "",
                                    strIngredient1 = Drink.strIngredient1 != null ? Drink.strIngredient1:"",
                                    strDrinkThumb = Drink.strDrinkThumb != null ? Drink.strDrinkThumb:""

                                };
                                lstResponseCocteles.Add(responseCocteles);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                      
                        ConsolaMensaje("Error al ObtenerCoctelFavorito: ", ex);
                        return new List<ViewResponseCocteles>();
                    }

                }
                return lstResponseCocteles;
            }
            catch (Exception ex)
            {
                ConsolaMensaje("Error al ObtenerCoctelFavorito", ex);
                return new List<ViewResponseCocteles>(); 
            }

        }

        [HttpGet("EliminarCoctel")]
        public async Task<IActionResult> EliminarCoctelesFavorito(string idDrink)
        {

            try
            {
                int idUsuario = HttpContext.Session.GetInt32("IdUser") ?? -1;
                var _EliminarCoctelesFavorito = await UnitOfWork.Coctel.DeleteInformationCoctel( int.Parse(idDrink), idUsuario);

            }
            catch (Exception ex)
            {
                ConsolaMensaje("Error al EliminarCoctelesFavorito", ex);

            }
            var cocteles = await ObtenerCoctelesFavorito();

            return View("Index",cocteles);


        }
        [HttpGet("FinderCoctel")]
        public IActionResult FinderCoctel()
        {
            return  RedirectToAction("Index", "BusquedaCoctel");
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("IdUser");

            return RedirectToAction("Index", "Home");
        }

        public void ConsolaMensaje(string mensaje, Exception? ex = null )
        {
            

            if (ex != null)
            {
                Console.WriteLine($"{mensaje}: {ex.Message}");
            }
            else
            {
                Console.WriteLine(mensaje);
            }
        }

    }
}
