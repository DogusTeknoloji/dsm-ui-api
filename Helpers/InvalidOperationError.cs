namespace DSM.UI.Api.Helpers
{
    public class InvalidOperationError
    {
        private static InvalidOperationError _instance;
        protected InvalidOperationError()
        {

        }

        public static InvalidOperationError GetInstance()
        {
            if (_instance == null) _instance = new InvalidOperationError();
            return _instance;
        }

        public string Message { get { return "Invalid Operation"; } }
    }
}
