namespace Navigator.Actions
{
    public struct Status
    {
        private readonly bool _isSuccess;

        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        public Status(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }
    }
}