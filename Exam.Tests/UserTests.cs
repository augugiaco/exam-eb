using Exam.Application.Dtos;
using Exam.Application.Interfaces;
using Exam.Application.Services;
using Exam.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exam.Tests
{
    public class UserTests
    {

        [Fact]
        public void MakeOldestUser()
        {
            //arrange
            var users = new List<UserDto>
            {
                new UserDto
                {
                    BirthDate = new DateTime(1967,11,02),
                    Uuid = "1111111111111"
                },
                new UserDto
                {
                    BirthDate = new DateTime(2000,02,02),
                    Uuid = "22222222222"
                },
                new UserDto
                {
                    BirthDate = new DateTime(1985,04,12),
                    Uuid = "3333333"
                },
                new UserDto
                {
                    BirthDate = new DateTime(2012,01,20),
                    Uuid = "444444444444"
                },
                new UserDto
                {
                    BirthDate = new DateTime(1965,09,25),
                    Uuid = "55555555555555"
                },
                 new UserDto
                {
                    BirthDate = new DateTime(1965,09,25),
                    Uuid = "666666666666666"
                }

            };

            //act
            users.MarkOldestUser();

            //get oldest birthday calculated with the custom algorithm
            var oldestUserBirthDate = users.FirstOrDefault(u => u.IsOldest).BirthDate;

            //get oldest birthday with linq min
            var oldesUserLinqBirthDatye = users.Min(c => c.BirthDate);

            //assert
            Assert.Equal(oldestUserBirthDate, oldesUserLinqBirthDatye);

        }
    }
}
