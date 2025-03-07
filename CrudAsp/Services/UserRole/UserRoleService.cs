// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using CrudAsp.Repository;
// using CrudAsp.Models;

// namespace CrudAsp.Services.UserRole;

// public class UserRoleService
// {
//     private readonly IRepository<Users> repository;

//     public UserRoleService(IRepository<Users> repository)
//     {
//         this.repository = repository;
//     }

//     public async Task<IEnumerable<Users>> GetAllAsync()
//     {
//         var user = await this.repository.GetAllAsync();
//         return user;
//     }
// }
