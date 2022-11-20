namespace LMSApi2.Helpers
{
    public class AppSettings
    {
        public string? Secret { get; set; }
        public string? TeacherSecret { get; set; }

        public string? SaveFolderPath { get; set; }
        public string? AzureWebJobsStorage { get; set; }
        public string? ContainerName { get; set; }

        public string? FUNCTIONS_WORKER_RUNTIME { get; set; }
    }
}
