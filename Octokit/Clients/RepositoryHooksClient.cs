﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Webhooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Webhooks API documentation</a> for more information.
    /// </remarks>
    public class RepositoryHooksClient : ApiClient, IRepositoryHooksClient
    {
        /// <summary>
        /// Initializes a new GitHub Webhooks API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryHooksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IReadOnlyList{RepositoryHook}"/> of <see cref="RepositoryHook"/>s representing hooks for specified repository</returns>
        public Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IReadOnlyList{RepositoryHook}"/> of <see cref="RepositoryHook"/>s representing hooks for specified repository</returns>
        public Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryHook>(ApiUrls.RepositoryHooks(owner, name), options);
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="RepositoryHook"/> representing hook for specified hook id</returns>
        public Task<RepositoryHook> Get(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<RepositoryHook>(ApiUrls.RepositoryHookById(owner, name, hookId));
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="RepositoryHook"/> representing created hook for specified repository</returns>
        public Task<RepositoryHook> Create(string owner, string name, NewRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Post<RepositoryHook>(ApiUrls.RepositoryHooks(owner, name), hook.ToRequest());
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="RepositoryHook"/> representing modified hook for specified repository</returns>
        public Task<RepositoryHook> Edit(string owner, string name, int hookId, EditRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Patch<RepositoryHook>(ApiUrls.RepositoryHookById(owner, name, hookId), hook);
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        /// <returns></returns>
        public Task Test(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Post(ApiUrls.RepositoryHookTest(owner, name, hookId));
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Ping(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Post(ApiUrls.RepositoryHookPing(owner, name, hookId));
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Delete(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.RepositoryHookById(owner, name, hookId));
        }
    }
}