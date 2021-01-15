using System;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Navigator.Extensions.Shipyard.Middleware;
using Navigator.Extensions.Store.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Shipyard.Controllers
{
    /// <summary>
    /// Conversation controller.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TChat"></typeparam>
    [Authorize(AuthenticationSchemes = nameof(ShipyardApiKeyAuthenticationHandler))]
    [ApiController]
    [ApiVersion("1")]
    [Route("shipyard/api/v{v:apiVersion}/conversation")]
    public class ConversationController<TUser, TChat> : ControllerBase where TUser : User where TChat : Chat
    {
        private readonly IEntityManager<TUser, TChat> _entityManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="entityManager"></param>
        public ConversationController(IEntityManager<TUser, TChat> entityManager)
        {
            _entityManager = entityManager;
        }

        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("user")]
        public IActionResult GetUsers(CancellationToken cancellationToken)
        {
            try
            {
                var users = _entityManager.FindAllUsersAsync(cancellationToken);
            
                if (users is not null)
                {
                    return Ok(users);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return NotFound();
        }
        
        /// <summary>
        /// Gets all the chats.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("chat")]
        public IActionResult GetChats(CancellationToken cancellationToken)
        {
            try
            {
                var chats = _entityManager.FindAllChatsAsync(cancellationToken);
            
                if (chats is not null)
                {
                    return Ok(chats);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return NotFound();
        }
    }
}