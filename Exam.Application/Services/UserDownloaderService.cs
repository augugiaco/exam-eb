using Exam.Application.Dtos;
using Exam.Application.Dtos.UserDownloader;
using Exam.Application.Interfaces;
using System.Linq;
using System.Net.Http;

namespace Exam.Application.Services
{
    public class UserDownloaderService: IUserDownloaderService
    {

        private readonly IUserService _userService;

        public UserDownloaderService(IUserService userService)
        {
            _userService = userService;
        }


        public void DownloadAndSaveUsersFromEndPoint(string endpointUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(endpointUrl).Result;

                var responseToParse = response.Content.ReadAsStringAsync().Result;

                var usersAsJson = Newtonsoft.Json.JsonConvert.DeserializeObject<UserListDownloadDto>(responseToParse);

                var userListDto =  usersAsJson.results.Select(res => new UserDto
                {
                    Uuid = res.login.uuid,
                    Email = res.email,
                    Gender = res.gender,
                    UserName = res.login.username,
                    BirthDate = res.dob.date,
                    Name = res.name.FullName,
                    Location = new LocationDto
                    {
                        City = res.location.city,
                        PostCode = res.location.postcode,
                        Street = res.location.street,
                        State = res.location.state
                    }
                })
                .ToList();

                _userService.CreateMassive(userListDto);

            }
        }
    }
}
