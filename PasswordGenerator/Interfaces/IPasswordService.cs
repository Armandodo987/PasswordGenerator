namespace PasswordGenerator.Interfaces
{
    public interface IPasswordService
    {
        /// <summary>
        /// implements PasswordService
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="timeStamp">DateTime in long type</param>
        /// <returns></returns>
        string GenerateOTP(int userId, long timeStamp);
    }
}
