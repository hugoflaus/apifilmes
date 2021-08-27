using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody]Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
           return CreatedAtAction(nameof(RecuperarFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme != null)
            {
                return Ok(filme);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme([FromBody]Filme filmeAtualizado, int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeAtualizado.Titulo;
            filme.Genero = filmeAtualizado.Genero;
            filme.Duracao = filmeAtualizado.Duracao;
            filme.Diretor = filmeAtualizado.Diretor;

            _context.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
