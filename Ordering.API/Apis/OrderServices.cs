﻿using Ordering.API.Application.Queries;
using Ordering.API.Infrastructure.Services;

namespace Ordering.API.Apis
{
    public class OrderServices(
        IMediator mediator,
        IOrderQueries queries,
        IIdentityService identityService,
        ILogger<OrderServices> logger)
    {
        public IMediator Mediator { get; set; } = mediator;
        public ILogger<OrderServices> Logger { get; } = logger;
        public IOrderQueries Queries { get; } = queries;
        public IIdentityService IdentityService { get; } = identityService;
    }
}
