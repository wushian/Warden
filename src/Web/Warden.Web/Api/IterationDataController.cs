﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Mvc;
using Warden.Web.Core.Dto;
using Warden.Web.Core.Queries;
using Warden.Web.Core.Services;

namespace Warden.Web.Api
{
    [Route("api/data/iterations")]
    public class IterationDataController : ApiController
    {
        private readonly IWardenIterationService _wardenIterationService;

        public IterationDataController(IWardenIterationService wardenIterationService, IApiKeyService apiKeyService)
            : base(apiKeyService)
        {
            _wardenIterationService = wardenIterationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WardenIterationDto iteration)
        {
            await _wardenIterationService.SaveIterationAsync(iteration, OrganizationId);

            return new HttpStatusCodeResult(204);
        }

        [HttpGet]
        public async Task<IEnumerable<WardenIterationDto>> GetAll([FromUri] BrowseWardenIterations query)
        {
            query.OrganizationId = OrganizationId;
            var pagedResult = await _wardenIterationService.GetIterationsAsync(query);

            return pagedResult.Items;
        }
    }
}