using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PrevisaoController : ControllerBase
{
    private readonly MLModelBuilder _ml;

    public PrevisaoController()
    {
        _ml = new MLModelBuilder();
    }

    [HttpPost("prever")]
    public IActionResult Prever([FromBody] ModelInput input)
    {
        var resultado = _ml.Prever(input);
        return Ok(new { PrecoPrevisto = resultado });
    }
}
