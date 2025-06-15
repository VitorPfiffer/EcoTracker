namespace EcoTracker.Core.Swagger.Data
{
    public static class ProgramInfo
    {
        public static Guid ExecutionId = Guid.NewGuid();
        public static DateTime ExecutionDate = DateTime.UtcNow;
    }
}
