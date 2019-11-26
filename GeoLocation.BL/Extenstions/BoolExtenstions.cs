namespace GeoLocation.BL.Extenstions
{
    public static class BoolExtenstions
    {
        public static int ConvertToInt(this bool value) => value == true ? 1 : 0;
    }
}
