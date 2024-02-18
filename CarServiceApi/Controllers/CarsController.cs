using Application.DTOs;
using Infrastructure.Database.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApi.Controllers;

[ApiController]
[Route("api/cars")]
public class CarsController : ControllerBase
{
    private readonly IGenericRepository<Car> _carRepository;

    public CarsController(IGenericRepository<Car> carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Car>?>> GetAll(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 100)
    {
        var dbResponse = await _carRepository.GetAllPaged(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return new NotFoundObjectResult("There were no cars found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car?>> GetById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _carRepository.GetById(id);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with the specified id: {id} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Car>> Add(
        [Required][FromBody] CarAddDto carDto)
    {
        var car = new Car
        {
            Id = 0,
            Brand = carDto.Brand,
            Model = carDto.Model,
            Year = carDto.Year,
        };

        var dbResponse = await _carRepository.Add(car);
        return new OkObjectResult(dbResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car?>> Update(
        [Required][FromRoute] int id,
        [Required][FromBody] CarUpdateDto carDto)
    {
        var carFromDb = await _carRepository.GetById(id);
        if (carFromDb is null)
        {
            return new NotFoundObjectResult(
                $"The car with the specified id: {id} was not found!");
        }

        await _carRepository.Detach(carFromDb);
        var car = new Car
        {
            Id = id,
            Brand = carDto.Brand ?? carFromDb.Brand,
            Model = carDto.Model ?? carFromDb.Model,
            Year = carDto.Year ?? carFromDb.Year,
        };

        var dbResponse = await _carRepository.Update(car);
        return new OkObjectResult(dbResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Car?>> Delete(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _carRepository.Delete(id);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The car with the specified id: {id} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }
}
