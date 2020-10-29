using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Artemis.Build.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BuildController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{buildDefinition}/{artifactName}")]
        public async Task<RedirectResult> Get(int buildDefinition, string artifactName)
        {
            var apiBaseUrl = _configuration["ApiBaseUrl"];
            var personalAccessToken = _configuration["PersonalAccessToken"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}"))
            );

            // Get the latest build ID
            using var latestBuildResponse = await client.GetAsync($"{apiBaseUrl}build/latest/{buildDefinition}?branchName=master&api-version=6.1-preview.1");
            latestBuildResponse.EnsureSuccessStatusCode();
            var latestBuildBody = await latestBuildResponse.Content.ReadAsStringAsync();

            var latestBuildJson = JObject.Parse(latestBuildBody);
            var id = latestBuildJson["id"].Value<int>();

            // Get the artifacts of the latest ID
            using var artifactResponse = await client.GetAsync($"{apiBaseUrl}build/builds/{id}/artifacts?artifactName={artifactName}&api-version=6.1-preview.5");
            artifactResponse.EnsureSuccessStatusCode();
            var artifactBody = await artifactResponse.Content.ReadAsStringAsync();

            var artifactJson = JObject.Parse(artifactBody);
            return Redirect(artifactJson["resource"]["downloadUrl"].Value<string>());
        }
    }
}