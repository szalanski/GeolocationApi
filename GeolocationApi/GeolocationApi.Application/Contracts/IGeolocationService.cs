﻿using GeolocationApi.Application.Models.GeolocationData;
using LanguageExt.Common;

namespace GeolocationApi.Application.Contracts
{
    public interface IGeolocationService : IDisposable
    {
        Task<Result<GeolocationModel>> GetAsync(string url, CancellationToken token);
    }
}
