using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace KGRC_Evaluacion.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public HomeController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ViewModelLogin(); 
            return View(model);
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
       
        
        [HttpPost]
        public async Task<IActionResult> Login(ViewModelLogin model, string submitButton)
        {
            if (ModelState.IsValid && submitButton == "Login")
            {
                var result = await _unitOfWork.Usuario.GetUserLoggin(model);

                if (result != null && result.Id!= 0 )
                {
                    HttpContext.Session.SetInt32("IdUser", result.Id);

                    return RedirectToAction("Index", "BusquedaCoctel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
                return RedirectToAction("Index", "Home");
            }
            else if (submitButton == "Register")
            {
                await Register(model);
            }

            return View("Index", "BusquedaCoctel");
        }

        [HttpPost]
        public async Task<IActionResult> Register(ViewModelLogin model)
        {
            if (ModelState.IsValid)
            {
                ResponseUser registro;
                registro = await _unitOfWork.Usuario.GetUserLoggin(model);
                if (registro.Id == 0)
                {
                    bool result = await _unitOfWork.Usuario.SaveInformationUsuario(model);
                    if (result)
                    {
                        
                        registro = await _unitOfWork.Usuario.GetUserLoggin(model);

                        HttpContext.Session.SetInt32("IdUser", registro.Id);

                        return RedirectToAction("Index", "BusquedaCoctel");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }

                    return RedirectToAction("Index", "Home");
                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Usuario registrado.");
                    return View("Index", model);
                }
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Error al registrar el usuario.");
                return View("Index", model);
            }
            
        }
        
    }
}
