using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace elastic_searcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{query}")]
        public async Task<IReadOnlyCollection<IHit<Person>>> Get(string query)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            // var response = await client.GetAsync<Person>(1, idx => idx.Index("personindex"));
            // var person = response.Source;

            var response = await client.SearchAsync<Person>(s => s
                .Index("personindex")
                .From(0)
                .Size(10)
                .Query(q => 
                    q.Term(t => t.FirstName, query) || 
                    q.MoreLikeThis(t => t.Name(query)) || 
                    q.Match(mq => mq.Field(f => f.FirstName).Query(query))
                )
            );

            return response.Hits;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            // var tweet = new Person
            // {
            //     Id = 1,
            //     User = "kimchy",
            //     PostDate = new DateTime(2009, 11, 15),
            //     Message = "Trying out NEST, so far so good?"
            // };

            var response = await client.IndexAsync(person, idx => idx.Index("personindex"));

            return Created("", person.Id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birth { get; set; }
    }
}
