using AutoMapper;
using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Application.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationApi.Application.Tests.Geolocations.Commands
{
    public abstract partial class CommandTestBase
    {
        protected const string TestJson = @"{
                                            ""ip"": ""185.21.87.139"",
                                            ""type"": ""ipv4"",
                                            ""continent_code"": ""EU"",
                                            ""continent_name"": ""Europe"",
                                            ""country_code"": ""PL"",
                                            ""country_name"": ""Poland"",
                                            ""region_code"": ""ZP"",
                                            ""region_name"": ""West Pomerania"",
                                            ""city"": ""Koszalin"",
                                            ""zip"": ""76-024"",
                                            ""latitude"": 54.121421813964844,
                                            ""longitude"": 16.168630599975586,
                                            ""location"": {
                                                ""geoname_id"": 3095049,
                                                ""capital"": ""Warsaw"",
                                                ""languages"": [
                                                    {
                                                        ""code"": ""pl"",
                                                        ""name"": ""Polish"",
                                                        ""native"": ""Polski""
                                                    }
                                                ],
                                                ""country_flag"": ""https://assets.ipstack.com/flags/pl.svg"",
                                                ""country_flag_emoji"": ""🇵🇱"",
                                                ""country_flag_emoji_unicode"": ""U+1F1F5 U+1F1F1"",
                                                ""calling_code"": ""48"",
                                                ""is_eu"": true
                                            }
                                        }";

        protected readonly Mock<IGeolocationRepository> _repository;
        protected readonly IMapper _mapper;

        public CommandTestBase()
        {
            _repository = RepositoryMocks.GetGeolocationRepository();

            var cfgProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = cfgProvider.CreateMapper();
        }
    }
}
