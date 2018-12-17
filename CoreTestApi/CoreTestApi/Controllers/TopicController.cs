using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CoreTestApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CoreTestApi.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        /// <summary>
        /// get all topics
        /// </summary>
        /// <returns>all Topics</returns>
        /// <response code="200">Returns all item</response>
        [HttpGet]
        public ActionResult<IEnumerable<Topic>> Get()
        {
            return TopicSource.All;
        }

        /// <summary>
        /// get topic 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return item from Topic</returns>
        /// <response code="200">Returns the selected item</response>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                Topic selectedTopic = TopicSource.All.Where(x => x.Id == id).FirstOrDefault();
                if (selectedTopic == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(selectedTopic);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }

            //return String.Format("Topic {0}", id);
        }


        /// <summary>
        /// Создание topic 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>edited Topic</returns>
        /// <response code="200">Returns the edited item</response>
        /// <response code="404">If the item is null</response>
        /// <response code="500">Other error</response>
        [HttpPut]
        [ActionName("Edit")]
        public IActionResult EditTopic(int id, [FromBody]Topic model)
        {
            try
            {
                Topic oldTopic = TopicSource.All.Where(x => x.Id == id).FirstOrDefault();
                if (oldTopic == null)
                {
                    return NoContent();//NotFound("Element not found");
                }
                else
                {
                    oldTopic.Title = model.Title;
                    oldTopic.Enabled = model.Enabled;

                    return Ok(oldTopic);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Создание topic 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created Topic</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="204">If the item is null</response>
        /// <response code="500">Other error</response>
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateTopic([FromBody]Topic model)
        {
            if (String.IsNullOrEmpty(model.Title))
                   return StatusCode((int)HttpStatusCode.NoContent, "1000.13 нет данных");
            else
            {
                try
                {
                    int id = TopicSource.All.Max(x => x.Id);
                    model.Id = id + 1;
                    TopicSource.All.Add(model);
                    return StatusCode((int)HttpStatusCode.Created, model);
                }
                catch (Exception e)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, e);
                }
                
            }
        }


        /// <summary>
        /// Удаление topic с указанным идентифкатором
        /// </summary>
        /// <param name="id"></param> 
        [HttpDelete]
        [ActionName("Delete")]
        public IActionResult DeleteTopic(int id)
        {
            try
            {
                Topic toDelete = TopicSource.All.Where(x => x.Id == id).FirstOrDefault();
                if (toDelete == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, "1000.13 нет данных");
                    //Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    TopicSource.All.Remove(toDelete);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}