using Best_Practices.Infraestructure.Factories;
using Best_Practices.Models;
using Best_Practices.Models.Exceptions;
using Best_Practices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Best_Practices.Controllers
{
    /// <summary>
    /// Controller for managing vehicle operations and views.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for data access.</param>
        /// <param name="logger">The logger instance.</param>
        public HomeController(IVehicleRepository vehicleRepository, ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Displays the home page with the list of vehicles.
        /// </summary>
        /// <returns>The index view with vehicle list.</returns>
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Vehicles = _vehicleRepository.GetVehicles()
            };

            // Display error message from TempData if present
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(model);
        }

        /// <summary>
        /// Adds a Ford Mustang to the vehicle collection.
        /// </summary>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddMustang()
        {
            try
            {
                var factory = new FordMustangCreator();
                var vehicle = factory.Create();
                _vehicleRepository.AddVehicle(vehicle);
                _logger.LogInformation("Added Ford Mustang with ID {VehicleId}", vehicle.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding Ford Mustang");
                TempData["ErrorMessage"] = $"Error adding vehicle: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Adds a Ford Explorer to the vehicle collection.
        /// </summary>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddExplorer()
        {
            try
            {
                var factory = new FordExplorerCreator();
                var vehicle = factory.Create();
                _vehicleRepository.AddVehicle(vehicle);
                _logger.LogInformation("Added Ford Explorer with ID {VehicleId}", vehicle.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding Ford Explorer");
                TempData["ErrorMessage"] = $"Error adding vehicle: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Starts the engine of a specific vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    return RedirectToAction(nameof(Index));
                }

                vehicle.StartEngine();
                _logger.LogInformation("Started engine for vehicle {VehicleId}", vehicleId);
                return RedirectToAction(nameof(Index));
            }
            catch (EngineFaultException ex)
            {
                _logger.LogWarning(ex, "Engine fault for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (FuelException ex)
            {
                _logger.LogWarning(ex, "Fuel issue for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error starting engine for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Adds fuel to a specific vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddGas(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    return RedirectToAction(nameof(Index));
                }

                vehicle.AddGas();
                _logger.LogInformation("Added fuel to vehicle {VehicleId}", vehicleId);
                return RedirectToAction(nameof(Index));
            }
            catch (FuelException ex)
            {
                _logger.LogWarning(ex, "Fuel issue for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error adding fuel to vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Stops the engine of a specific vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    return RedirectToAction(nameof(Index));
                }

                vehicle.StopEngine();
                _logger.LogInformation("Stopped engine for vehicle {VehicleId}", vehicleId);
                return RedirectToAction(nameof(Index));
            }
            catch (EngineFaultException ex)
            {
                _logger.LogWarning(ex, "Engine fault for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error stopping engine for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
