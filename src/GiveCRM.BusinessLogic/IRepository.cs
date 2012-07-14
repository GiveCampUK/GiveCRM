namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the basic interactions for a repository with some form of back-end persistence, 
    /// such as a database.  Exposes basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The model type the repository handles.</typeparam>
    public interface IRepository<T> where T: new()
    {
        /// <summary>
        /// Gets all the items of type <typeparamref name="T"/> persisted in the backing store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all items</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets a specific item of type <typeparamref name="T"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to return.</param>
        /// <returns>The item of type <typeparamref name="T"/> with the specified identifier</returns>
        T GetById(int id);

        /// <summary>
        /// Updates the existing item of type <typeparamref name="T"/> using the values from the instance provided.
        /// </summary>
        /// <param name="item">An item of type <typeparamref name="T"/> specifying the item's new values.</param>
        void Update(T item);

        /// <summary>
        /// Creates a new item of type <typeparamref name="T"/> using the values from the instance provided.
        /// </summary>
        /// <param name="item">The item of type <typeparamref name="T"/> to create.</param>
        /// <returns>The created item, with its identifier set.</returns>
        T Insert(T item);

        /// <summary>
        /// Deletes the item of type <typeparamref name="T"/> identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the item to delete.</param>
        void DeleteById(int id);
    }
}