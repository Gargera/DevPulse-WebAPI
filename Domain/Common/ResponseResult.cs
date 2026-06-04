namespace Domain.Common
{
    public record ResponseResult<T>(
    bool IsSuccess,
    string? Message,
    T? Data
);
}
