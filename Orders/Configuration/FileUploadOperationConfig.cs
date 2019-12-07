using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orders.Configuration
{
    public class FileUploadOperationConfig : IOperationFilter
    {
        private readonly string _uploadOrderImageOperationId = "apiorderuploadpost";
        private readonly string _uploadedFileParameterName = "uploadedFile";

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == _uploadOrderImageOperationId)
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = _uploadedFileParameterName,
                    In = "formData",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
