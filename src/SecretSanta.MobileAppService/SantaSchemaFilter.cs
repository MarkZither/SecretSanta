using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using SecretSanta.MobileAppService.Controllers;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SecretSanta.MobileAppService {
    public class SantaSchemaFilter : ISchemaFilter {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
            if (context.Type == typeof(MatchGeneratorModel)) {
                schema.Example = new OpenApiObject() {
                    ["firstName"] = new OpenApiString("John"),
                    ["lastName"] = new OpenApiString("Doe"),
                };
            }
        }
    }
}
