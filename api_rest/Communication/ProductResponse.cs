using api_rest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; private set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product) { }

        public ProductResponse(string Message) : this(false, Message, null) { }

    }
}
