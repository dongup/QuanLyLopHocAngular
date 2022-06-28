//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.OpenApi.Models;
//using System.Reflection;

//namespace QuanLyLopHoc.Utils
//{
//    [AttributeUsage(AttributeTargets.Property)]
//    public class SwaggerExcludeAttribute : Attribute
//    {
//    }

//    public class SwaggerExcludeFilter : ISchemaFilter
//    {
//        #region ISchemaFilter Members

//        public void Apply(Schema schema, SchemaRegistry context, Type type)
//        {
//            if (schema?.properties == null || type == null)
//                return;

//            var excludedProperties = type.GetProperties()
//                                         .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

//            foreach (var excludedProperty in excludedProperties)
//            {
//                if (schema.properties.ContainsKey(excludedProperty.Name))
//                    schema.properties.Remove(excludedProperty.Name);
//            }
//        }
//        #endregion
//    }
//}
