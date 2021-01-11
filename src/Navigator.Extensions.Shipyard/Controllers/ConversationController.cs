// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Navigator.Extensions.Store.Abstractions;
// using Navigator.Extensions.Store.Abstractions.Entity;
//
// namespace Navigator.Extensions.Shipyard.Controllers
// {
//     [ApiController]
//     [ApiVersion("1")]
//     [Microsoft.AspNetCore.Components.Route("shipyard/api/v{v:apiVersion}/[controller]")]
//     public class ConversationController<TUser, TChat> : ControllerBase 
//         where TUser : User 
//         where TChat : Chat
//     {
//         private readonly IEntityManager<TUser, TChat> _entityManager;
//
//         public ConversationController(IEntityManager<TUser, TChat> entityManager)
//         {
//             _entityManager = entityManager;
//         }
//
//         public Task<IActionResult> GetUsers()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }