using Application.DTOs;
using Infrastructure.Database.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApi.Controllers;

[ApiController]
[Route("api/carsAssignedToServices")]
public class CarsServicesController : ControllerBase
{
    private readonly IGenericRepository<CarAssignedToService> _carsServicesRepository;

    public CarsServicesController(
        IGenericRepository<CarAssignedToService> carsServicesRepository)
    {
        _carsServicesRepository = carsServicesRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarAssignedToService>?>> GetAll(
        [FromQuery] int offset,
        [FromQuery] int takeCount)
    {
        var dbResponse = await _carsServicesRepository.GetAllPaged(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return new NotFoundObjectResult("There were no cars and services found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpGet("car/{carId}/service/{serviceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car?>> GetByCombinedId(
        [Required][FromRoute] int carId,
        [Required][FromRoute] int serviceId)
    {
        var dbResponse = await _carsServicesRepository.GetByCombinedId(carId, serviceId);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with id: {carId} and service with id: {serviceId} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Car>> Add(
        [Required][FromBody] CarServiceAddDto carServiceDto)
    {
        var carAssignedToService = new CarAssignedToService
        {
            CarId = carServiceDto.CarId,
            ServiceId = carServiceDto.ServiceId,
        };

        var dbResponse = await _carsServicesRepository.Add(carAssignedToService);
        return new OkObjectResult(dbResponse);
    }

    [HttpDelete("car/{carId}/service/{serviceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car?>> Delete(
        [Required][FromRoute] int carId,
        [Required][FromRoute] int serviceId)
    {
        var dbResponse = await _carsServicesRepository.DeleteByCombinedId(carId, serviceId);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with id: {carId} and service with id: {serviceId} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }
}
