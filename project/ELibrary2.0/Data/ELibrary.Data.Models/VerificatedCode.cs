namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class VerificatedCode
    {
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual string Code { get; set; }

    }
}
