namespace Navigator.Actions.Model
{
    public struct ActionStatus
    {
        private readonly bool _isSuccess;

        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        public ActionStatus(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }
    }
}