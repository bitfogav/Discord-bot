﻿using DiscordPlayerCountBot.Http.QueryParams.Base;

namespace DiscordPlayerCountBot.Http;

public interface IHttpExecuter
{
    Task<TResponse?> DELETE<TRequest, TResponse>(string endPoint, IQueryParameterBuilder queryBuilder, TRequest? body, Tuple<string, string>? authToken = null);
    Task<TResponse?> GET<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null, Dictionary<string, string>? additionalHeaders = null);
    Task<TResponse?> PATCH<TRequest, TResponse>(string endPoint, IQueryParameterBuilder queryBuilder, TRequest? body, Tuple<string, string>? authToken = null);
    Task<TResponse?> POST<TRequest, TResponse>(string endPoint, IQueryParameterBuilder queryBuilder, TRequest? body, Tuple<string, string>? authToken = null);
    Task<TResponse?> PUT<TRequest, TResponse>(string endPoint, IQueryParameterBuilder queryBuilder, TRequest? body, Tuple<string, string>? authToken = null);
}