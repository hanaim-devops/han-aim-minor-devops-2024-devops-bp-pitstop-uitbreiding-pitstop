using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RentalCarManagementAPI.Models;

namespace RentalCarManagementAPI.Controller;

[ApiController]
[Route("/api/[controller]")]
public class RentalCarController : ControllerBase
{
    // Todo: implement methods for retrieving rental cars.
    [HttpGet]
    public List<RentalCar> GetRentalCars()
    {
        return [];
    }
}