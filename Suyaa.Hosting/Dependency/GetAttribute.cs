﻿using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// Get方法特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GetAttribute : HttpGetAttribute
    {
        /// <summary>
        /// Get方法特性
        /// </summary>
        public GetAttribute() : base("[action]") { }

        /// <summary>
        /// Get方法特性
        /// </summary>
        public GetAttribute(string name) : base(name) { }
    }
}