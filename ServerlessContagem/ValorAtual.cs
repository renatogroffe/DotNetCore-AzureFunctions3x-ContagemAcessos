using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ServerlessContagem
{
    public static class ValorAtual
    {
        private static readonly Contador _CONTADOR = new Contador();

        [FunctionName("ValorAtual")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            lock (_CONTADOR)
            {
                _CONTADOR.Incrementar();
                log.LogInformation($"Contador - Valor atual: {_CONTADOR.ValorAtual}");

                return new OkObjectResult(new
                {
                    _CONTADOR.ValorAtual,
                    _CONTADOR.Local,
                    _CONTADOR.Kernel,
                    _CONTADOR.TargetFramework,
                    MensagemFixa = "Teste",
                    MensagemVariavel = Environment.GetEnvironmentVariable("MensagemVariavel")
                });
            }
        }
    }
}