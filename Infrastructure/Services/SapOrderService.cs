using Domain.Entities.UNS;
using Infrastructure.SAPDM;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Reflection;
using System.Text;

namespace Infrastructure.Services
{
    public class SapOrderService
    {
        private ApiService apiService { get; set; }

        private string originalOrder;
        private string clientUrl;
        public SapOrderService(ApiService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            originalOrder = LoadEmbeddedResource("Infrastructure.Resources.SapProductionOrderTemplate.xml");
            clientUrl = configuration["ApiSettings:ClientUrl"];
        }
        public string GetSapOrderXML(UnsOrder unsOrder)
        {
            var replacements = new Dictionary<string, string>
                                        {
                                            { "ProductionOrderNumber", unsOrder.ID },
                                            { "TotalOrderQuantity", unsOrder.OrderQuantity.ToString("N3", System.Globalization.CultureInfo.InvariantCulture) },
                                            { "ConfirmedOrderFinishDate", unsOrder.EndTime.ToString("yyyyMMdd") },
                                            { "ActualStartDate", unsOrder.StartTime.ToString("yyyyMMdd") },
                                            { "MaterialNumber", unsOrder.MaterialId }

                                        };

            return ReplaceWords(replacements);
        }

        private string LoadEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException("Resource not found: " + resourceName);

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string ReplaceWords(Dictionary<string, string> replacements)
        {
            var sb = new StringBuilder(originalOrder);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }
            return sb.ToString();
        }
        
        public async Task<HttpStatusCode> SendOrderToSapAsync(UnsOrder order)
        {
            var errorCode = HttpStatusCode.BadRequest;
            try
            {
                var orderpayload = GetSapOrderXML(order);
                var response = await apiService.PostDataAsync(clientUrl, orderpayload);
                var responseMessage = await response.Content.ReadAsStringAsync();
                errorCode = response.StatusCode;
            }
            catch (Exception ex) 
            {
                return HttpStatusCode.InternalServerError;
            }

            return errorCode;
        }
    }
}
