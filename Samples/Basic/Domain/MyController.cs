// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;

namespace Domain;

public class MyController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello from MyController");
    }
}