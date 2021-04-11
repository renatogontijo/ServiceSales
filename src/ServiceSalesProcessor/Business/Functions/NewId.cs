using System;

namespace ServiceSalesProcessor.Business.Functions
{
    public static class NewId
    {
        private static Random _random = null;

        private static Random RandomObj()
        {
            if (_random == null)
                _random = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            return _random;
        }

        public static long Get()
        {
            return RandomObj().Next(1, 9999898);
        }
    }
}
