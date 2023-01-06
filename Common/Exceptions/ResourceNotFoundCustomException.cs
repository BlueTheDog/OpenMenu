using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions;
public class ResourceNotFoundCustomException : Exception
{
    public string Type { get; set; } = "resource-not-found-exception";
    public string Detail { get; set; } = "Could not find resource.";
    public string Title { get; set; } = "Resource Not Found Exception";
    public ResourceNotFoundCustomException(string v)
    {
        Detail = v;
    }

    public ResourceNotFoundCustomException()
    {
    }
}
