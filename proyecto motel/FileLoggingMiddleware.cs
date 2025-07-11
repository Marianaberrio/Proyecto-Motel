using System.Text;

namespace proyecto_motel
{
    public class FileLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public FileLoggingMiddleware(RequestDelegate next)
        {
            _next = next;

            // Ruta para guardar logs (se guarda en la raíz del proyecto)
            _logFilePath = Path.Combine(AppContext.BaseDirectory, "api-log.txt");

            // Si no existe, crea el archivo de log en la raíz del proyecto
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Dispose();
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Habilitar el buffer para poder leer el body varias veces
            context.Request.EnableBuffering();

            // Leer el cuerpo de la petición
            string requestBody = "";
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Capturar el cuerpo de la respuesta
            MemoryStream responseBody = new MemoryStream();
            var originalBodyStream = context.Response.Body;
            context.Response.Body = responseBody;

            // Continuar con el siguiente middleware
            await _next(context);

            // Leer el cuerpo de la respuesta
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Construir el texto del log
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                $"Usuario: {(context.User.Identity?.Name ?? "Anónimo")} | " +
                $"Método: {context.Request.Method} | " +
                $"Ruta: {context.Request.Path} | " +
                $"Query: {context.Request.QueryString} | " +
                $"Request Body: {requestBody} | " +
                $"Estado: {context.Response.StatusCode} | " +
                $"Response Body: {responseBodyText}\n";

            // Guardar el log en el archivo
            File.AppendAllText(_logFilePath, logEntry);

            // Devolver la respuesta original al pipeline
            await responseBody.CopyToAsync(originalBodyStream);

            // Dispose del MemoryStream
            responseBody.Dispose();
        }
    }
}
