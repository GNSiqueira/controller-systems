using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ControlSystems.Objects.Contracts.Exceptions;
namespace ControlSystems.Objects.Contracts;

public class Response<T>
{
    public int Status { get; private set; }
    public bool Success { get; protected set; }
    public string Message { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<FieldError>? Errors { get; private set; }

    public Response(int status, string message, T? data = default, List<FieldError>? errors = default)
    {
        Status = status;
        Message = message;
        Data = data;
        Success = Success = Status >= 200 && Status <= 299 ? true : false; ;
        Errors = errors;
    }

    public static ObjectResult Ok(T data, string message)
    {
        return new ObjectResult(new Response<T>(200, message, data)) { StatusCode = StatusCodes.Status200OK };
    }

    public static ObjectResult Created(T data, string message)
    {
        return new ObjectResult(new Response<T>(201, message, data)) { StatusCode = StatusCodes.Status201Created };
    }

    public static ObjectResult NoContent()
    {
        return new ObjectResult(204) { StatusCode = StatusCodes.Status204NoContent };
    }

}