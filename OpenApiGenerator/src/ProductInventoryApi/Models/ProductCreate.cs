/*
 * Product Inventory API
 *
 * API for managing product inventory
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProductInventoryApi.Converters;

namespace ProductInventoryApi.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ProductCreate : IEquatable<ProductCreate>
    {
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        /* <example>Laptop</example> */
        [Required]
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        /* <example>High-performance laptop with 16GB RAM</example> */
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        /* <example>999.99</example> */
        [Required]
        [DataMember(Name="price", EmitDefaultValue=true)]
        public double Price { get; set; }

        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        /* <example>Electronics</example> */
        [Required]
        [DataMember(Name="category", EmitDefaultValue=false)]
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets ImageUrl
        /// </summary>
        /* <example>https://example.com/images/laptop.jpg</example> */
        [DataMember(Name="imageUrl", EmitDefaultValue=false)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProductCreate {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  ImageUrl: ").Append(ImageUrl).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProductCreate)obj);
        }

        /// <summary>
        /// Returns true if ProductCreate instances are equal
        /// </summary>
        /// <param name="other">Instance of ProductCreate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProductCreate other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) && 
                (
                    Price == other.Price ||
                    
                    Price.Equals(other.Price)
                ) && 
                (
                    Category == other.Category ||
                    Category != null &&
                    Category.Equals(other.Category)
                ) && 
                (
                    ImageUrl == other.ImageUrl ||
                    ImageUrl != null &&
                    ImageUrl.Equals(other.ImageUrl)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    
                    hashCode = hashCode * 59 + Price.GetHashCode();
                    if (Category != null)
                    hashCode = hashCode * 59 + Category.GetHashCode();
                    if (ImageUrl != null)
                    hashCode = hashCode * 59 + ImageUrl.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProductCreate left, ProductCreate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProductCreate left, ProductCreate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
