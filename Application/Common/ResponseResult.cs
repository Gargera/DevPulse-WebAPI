using System.Collections.Generic;

namespace Application.Common
{
    public record ResponseResult<T>(
        bool IsSuccess,
        string? Message,
        T? Data
    );
}
