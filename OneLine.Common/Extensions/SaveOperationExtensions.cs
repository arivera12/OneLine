using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class SaveOperationExtensions
    {
        public static bool IsAdd(this SaveOperation saveOperation)
        {
            return saveOperation == SaveOperation.Add;
        }
        public static bool IsUpdate(this SaveOperation saveOperation)
        {
            return saveOperation == SaveOperation.Update;
        }
    }
}
