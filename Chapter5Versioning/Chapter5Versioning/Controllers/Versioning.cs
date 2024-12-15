using Chapter5Versioning.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chapter5Versioning.Controllers;
[Route($"{Endpoints.Version}/{Endpoints.HomeApiEndpoint}")]
public class HomeController : Controller
{
    [HttpGet]
    public string Get() => "Version 1";
}

[Route($"{Endpoints.VersionNext}/{Endpoints.HomeApiEndpoint}")]
public class HomeV2Controller : Controller
{
    [HttpGet]
    public string Get() => "Version 2";
}
