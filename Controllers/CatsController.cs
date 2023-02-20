// LICENSE: The Unlicense

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryPatternQuickSample.Contracts;
using RepositoryPatternQuickSample.Models;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RepositoryPatternQuickSample.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatsRepository m_catsRepository;
        private readonly ILogger<CatsController> m_logger;

        public CatsController(ICatsRepository catsRepository, ILogger<CatsController> logger)
        {
            m_catsRepository = catsRepository;
            m_logger = logger;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Cat>> CreateCat(
            [Bind(nameof(Cat.Name), nameof(Cat.Color))] Cat cat)
        {
            Cat createdCat;
            try
            {
                createdCat = await m_catsRepository.CreateCatAsync(cat.Name, cat.Color);
                await m_catsRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, "Error occurred during creating a cat.");
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCat), new { id = createdCat.Id }, createdCat);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cat>> GetCat(int id)
        {
            Cat? cat = await m_catsRepository.GetCatAsync(id);
            if (cat is null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        [HttpGet]
        [Route("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Cat>>> GetAllCats()
        {
            return Ok(await m_catsRepository.GetAllCatsAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cat>> DeleteCat(int id)
        {
            Cat? cat = await m_catsRepository.DeleteCatAsync(id);
            if (cat is null)
            {
                return NotFound();
            }

            await m_catsRepository.SaveChangesAsync();

            return Ok(cat);
        }

        [HttpPatch]
        [Route("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cat>> ModifyCat(
            int id,
            [Bind(nameof(Cat.Name), nameof(Cat.Color))] Cat newCatData)
        {
            Cat? cat = await m_catsRepository.GetCatAsync(id);
            if (cat is null)
            {
                return NotFound();
            }

            cat.Name = newCatData.Name;
            cat.Color = newCatData.Color;

            try
            {
                await m_catsRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Here we handle concurrency exceptions!, where the data has been modified
                //  by another party before we being able to save it.
                m_logger.LogError(ex, "Error occurred during modifying a cat.");
                return BadRequest();
            }

            return Ok(cat);
        }
    }
}
