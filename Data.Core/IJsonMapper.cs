using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Core
{
    /// <summary>Interface IJsonMapper</summary>
    public interface IJsonMapper
    {
        /// <summary>
        ///   <para>
        /// Gets the collection from json.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List&lt;T&gt;.</returns>
        List<T> GetCollectionFromJson<T>() where T : Model;

        /// <summary>Saves the collection to json.</summary>
        /// <typeparam name="T"></typeparam>
        void SaveCollectionToJson<T>(List<T> collection) where T : Model;
    }
}
