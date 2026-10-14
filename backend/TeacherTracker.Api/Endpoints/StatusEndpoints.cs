using System.Net.NetworkInformation;

namespace TeacherTracker.Api.Endpoints
{
    public static class StatusEndpoints
    {
        public static void MapStatusEndpoints(this WebApplication app)
        {
            app.MapGet("/api/status", () =>
            {
                return new
                {
                    Application = "TeacherTracker",
                    Status = "Running"
                };
            });
        }
    }
}
