using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class FormStateExtensions
    {
        public static bool IsCopy(this FormState formState)
        {
            return formState == FormState.Copy;
        }
        public static bool IsCreate(this FormState formState)
        {
            return formState == FormState.Create;
        }
        public static bool IsDelete(this FormState formState)
        {
            return formState == FormState.Delete;
        }
        public static bool IsDeleted(this FormState formState)
        {
            return formState == FormState.Deleted;
        }
        public static bool IsDetails(this FormState formState)
        {
            return formState == FormState.Details;
        }
        public static bool IsEdit(this FormState formState)
        {
            return formState == FormState.Edit;
        }
        public static bool IsReadOnly(this FormState formState)
        {
            return formState == FormState.ReadOnly;
        }
    }
}
