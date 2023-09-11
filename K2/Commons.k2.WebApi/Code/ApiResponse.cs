// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiResponse.cs" company="SURE International Technology">
//   Copyright © 2017 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace k2.API.Code
{
    /// <summary>
    /// The api response.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the TotalItemCount.
        /// </summary>
        public int? TotalItemCount { get; set; }

        /// <summary>
        /// Gets or sets the pageSize.
        /// </summary>
        public int? pageSize { get; set; }

        /// <summary>
        /// Gets or sets the PageNumber.
        /// </summary>
        public int? PageNumber { get; set; }

    }

    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }

        public ApiResponse(T tobject)
        {
            Value = tobject;
        }
        /// <summary>
        /// Gets or sets a value indicating whether confirm.
        /// </summary>
        public bool Confirm { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the model state errors.
        /// </summary>
        public ICollection<Item> ModelStateErrors { get; set; } = new List<Item>();

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; set; }
    }

    public class Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public Item(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}