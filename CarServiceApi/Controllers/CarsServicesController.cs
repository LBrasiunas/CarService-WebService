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
    private readonly IGenericRepository<Car> _carRepository;
    private readonly IGenericRepository<Service> _serviceRepository;

    public CarsServicesController(
        IGenericRepository<CarAssignedToService> carsServicesRepository,
        IGenericRepository<Car> carRepository,
        IGenericRepository<Service> serviceRepository)
    {
        _carsServicesRepository = carsServicesRepository;
        _carRepository = carRepository;
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarAssignedToService>?>> GetAll(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 100)
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
        var dbResponse = await _carsServicesRepository.GetByCombinedId(serviceId, carId);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with id: {carId} and service with id: {serviceId} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car>> Add(
        [Required][FromBody] CarServiceAddDto carServiceDto)
    {
        var carFromDb = await _carRepository.GetById(carServiceDto.CarId);
        var serviceFromDb = await _serviceRepository.GetById(carServiceDto.ServiceId);
        if (carFromDb is null || serviceFromDb is null)
        {
            return new NotFoundObjectResult(
                $"The car with id: {carServiceDto.CarId} or service " +
                $"with id: {carServiceDto.ServiceId} was not found!");
        }

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
        var dbResponse = await _carsServicesRepository.DeleteByCombinedId(serviceId, carId);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with id: {carId} and service with id: {serviceId} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }
}
