using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShapeCalculations.Application.Contracts;
using ShapeCalculations.Models;

namespace ShapeCalculations.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CubesController : ControllerBase
    {
        #region Fields

        private readonly ILogger<CubesController> _logger;
        private readonly IIntersectionService _intersectionService;
        private readonly IParserService _parserService;

        #endregion

        #region Constructor

        public CubesController(ILogger<CubesController> logger, IParserService parserService, IIntersectionService intersectionService)
        {
            _logger = logger;
            _parserService = parserService;
            _intersectionService = intersectionService;
        }

        #endregion

        #region Public Methods

        [HttpGet, Route("{strCubeA}/{strCubeB}/intersection")]
        public IActionResult GetIntersection([Required] string strCubeA, [Required] string strCubeB)
        {
            Cube cubeA;
            Cube cubeB;

            try
            {
                cubeA = _parserService.ToCube(strCubeA);
                cubeB = _parserService.ToCube(strCubeB);
            }
            catch(Exception ex)
            {
                string message = 
                    $"At least one of the provided cubes [{strCubeA}, {strCubeB}]" +
                    $" does not correspond to a valid cube.";

                _logger.LogError(ex, message);
                return BadRequest(message);
            }

            try
            {
                var result = _intersectionService.GetIntersection(cubeA, cubeB);
                return Ok(result);
            }
            catch(Exception ex)
            {
                string message =
                    $"There has been an error calculating the intersection of" +
                    $" [{strCubeA}, {strCubeB}].";
                _logger.LogError(ex, message);
                return StatusCode(StatusCodes.Status500InternalServerError, message); ;
            }
        }

        #endregion
    }
}
