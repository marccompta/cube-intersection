using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShapeCalculations.Application.Contracts;
using ShapeCalculations.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ShapeCalculations.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [SwaggerOperation(Summary = "Get the intersection of two cubes", 
            Description = "Retrieves the intersection of two non-rotated cubes, if exists.",
            Tags = new[] { "Intersection, Cube" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Get Intersection successfully", typeof(GetIntersectionResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input data", null)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", null)]
        [HttpGet, Route("{strCubeA}/{strCubeB}/intersection")]
        public IActionResult GetIntersection(
            [Required]
            [SwaggerParameter("Format: centerX__centerY__centerZ__SizeOfX__SizeOfY__SizeOfZ (notice each underscore is double).<br><br>E.g. (feel free to copy paste it :P):<br>-1.5__-1__1__1.1__1.2__2")]
                string strCubeA,
            [Required]
            [SwaggerParameter("Format: centerX__centerY__centerZ__SizeOfX__SizeOfY__SizeOfZ (notice each underscore is double).<br><br>E.g. (feel free to copy paste it :P):<br>-1.5__-1__1__1.5__2__3")]
            string strCubeB)
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
