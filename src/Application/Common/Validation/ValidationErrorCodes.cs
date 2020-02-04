namespace CleanArchitecture.Application.Common.Validation
{
    public class ValidationErrorCodes
    {
        // Todo lists
        public static string TODO_LIST_TITLE_REQUIRED = "Ca.Ve.0001";
        public static string TODO_LIST_TITLE_TOO_LONG = "Ca.Ve.0002";
        public static string TODO_LIST_TITLE_NOT_UNIQUE = "Ca.Ve.0003";
        // Todo items
        public static string TODO_ITEM_TITLE_REQUIRED = "Ca.Ve.0101";
        public static string TODO_ITEM_TITLE_TOO_LONG = "Ca.Ve.0102";
    }
}
