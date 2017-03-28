using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/authorscollections")]
    public class AuthorsCollectionsController : Controller
    {
        private ILibraryRepository _libraryRepository;
        public AuthorsCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }


        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null)
            {
                return BadRequest();
            }

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }

            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author collection failed on save");
            }

            //return Ok();

            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorsDto>>(authorEntities);

            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id)); // zwraca id kazdego autora do headera

            return CreatedAtRoute("GetAuthorCollection", new
            {
                ids = idsAsString
            },
            authorCollectionToReturn);
        }

        //zwraca tablice id autorow po dodaniu kilku autorow
        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _libraryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = Mapper.Map<IEnumerable<AuthorsDto>>(authorEntities);
            return Ok(authorsToReturn);
        }


    }
}