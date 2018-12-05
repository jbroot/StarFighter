using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpaceFighterWeb.Controllers
{
    public class ScoresController : ApiController
    {
        // GET: api/Scores
        public IEnumerable<string> Get()
        {
            var db = new DatabaseController();
            return db.GetTopTenScores();
        }

        // PUT: api/Scores
        [HttpPut]
        public async Task<string> Put()
        {
            var data = await Request.Content.ReadAsStringAsync();
            var db = new DatabaseController();
            var ret = db.PutScore(data);
            return ret.ToString();
        }

        // DELETE: api/Scores
        [HttpDelete]
        public string Delete()
        {
            var db = new DatabaseController();
            var ret = db.ClearScores();
            return ret.ToString();
        }
    }
}
