using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EvalCore.Entities
{
    [DataContract] //se usa para poner decorators en el header de la request y que asi el front pueda leer lo que nuestro contract posee (trabaja con la serializacion de json)
    public abstract class Response
    {
        [DataMember(Name = "status")] //cambia el nombre de la propiedad al momento de la serializacion
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageCode")]
        public string MessageCode { get; set; }

        public static class StatusNames
        {
            public const string Success = "success"; //success,error,fatal, validationError, access denied, mantainance
            public const string Error = "error";
            public const string Fatal = "fatal";
            public const string AccessDenied = "acceso-denegado";
            public const string SuccessPost = "Registro agregado con éxito";
            public const string Maintenance = "mantenimiento";
            public const string ValidationError = "errorDeValidacion";
            public const string NotFound = "NotFound";
        }
    }

    public class Response<T> : Response
    {
        private const string AccessDenied = "No dispones de Acceso para este modulo";

        [DataMember(Name = "data")]
        public T Data { get; set; }

        public static Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>
            {
                Status = StatusNames.Success,
                Data = data
            };
        }
        public static Response<T> CreateNotFoundResponse(T data)
        {
            return new Response<T>
            {
                Status = StatusNames.NotFound,
                Data = data,
                MessageCode = "404",
                Message = "Data nula"
            };
        }
        public static Response<T> CreateErrorResponse(Exception ex)
        {
            return new Response<T>
            {
                Status = StatusNames.Error,
                MessageCode = ex.HResult.ToString(),
                Message = ex.Message
            };
        }
        public static Response<T> CreateFatalResponse(string errorMessage)
        {
            return new Response<T>
            {
                Status = StatusNames.Fatal,
                MessageCode = "500",
                Message = errorMessage
            };
        }
    }
}
