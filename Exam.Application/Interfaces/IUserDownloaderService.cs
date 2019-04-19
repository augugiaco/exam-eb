namespace Exam.Application.Interfaces
{
    public interface IUserDownloaderService
    {
        void DownloadAndSaveUsersFromEndPoint(string endpointUrl);
    }
}
