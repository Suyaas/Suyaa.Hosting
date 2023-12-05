using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Results;
using Suyaa.Hosting.Kernel.ActionFilters;

namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// 切片供应商
    /// </summary>
    public interface IActionFilterProvider: IActionFilter
    {
    }
}
