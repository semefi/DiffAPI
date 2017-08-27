using DiffAPI.Filters;
using DiffAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiffAPI.Controllers
{
    [RoutePrefix("v1/diff")]
    [HandleExceptionFilter]
    public class DiffController : ApiController
    {
        private readonly IDiffService _diffService;
        private readonly IComparisonService _comparisonService;


        public DiffController()
        {
            _diffService = new Services.DiffService();
            _comparisonService = new ComparisonService();
        }

        public DiffController(IDiffService service, IComparisonService comparisonService)
        {
            _diffService = service;
            _comparisonService = comparisonService;
        }

        /// <summary>
        /// Method to assign the left value to an Id
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <param name="base64String">Base64 value to save on the left property in Comparison</param>
        /// <returns></returns>
        [Route("{id}/left")]
        [HttpPost]
        public HttpResponseMessage Left(string id, [FromBody] string base64String)
        {

            _comparisonService.AddLeft(id, base64String);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("Saved Left Side", System.Text.Encoding.UTF8, "text/plain");
            return response;
        }

        /// <summary>
        /// Method to assign the right value to an Id
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <param name="base64String">Base64 value to save on the right property in Comparison</param>
        /// <returns></returns>
        [Route("{id}/right")]
        [HttpPost]
        public HttpResponseMessage Right(string id, [FromBody] string base64String)
        {

            _comparisonService.AddRight(id, base64String);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("Saved Right Side", System.Text.Encoding.UTF8, "text/plain");
            return response;
        }

        /// <summary>
        /// Method to Diffed a Comparison
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <returns>A result with the diff information.</returns>
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage Diff(string id)
        {

            var comparison = _comparisonService.Get(id);
            //We verify we got both values
            if (!string.IsNullOrWhiteSpace(comparison.Left) && !string.IsNullOrWhiteSpace(comparison.Right))
            {
                return Request.CreateResponse(HttpStatusCode.OK, _diffService.GetDifferences(comparison.Left, comparison.Right));
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                "The id requested has not left data and right data to be compared, please provide both side before call this endpoint.");
        }

    }
}
