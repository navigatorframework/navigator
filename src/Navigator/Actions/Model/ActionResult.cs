namespace Navigator.Actions.Model
{
    public struct ActionResult
    {
        private readonly bool _isSuccess;

        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        public ActionResult(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }
    }
}