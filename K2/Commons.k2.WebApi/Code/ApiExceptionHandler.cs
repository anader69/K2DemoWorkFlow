//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="ApiExceptionHandler.cs" company="SURE International Technology">
////   Copyright © 2017 All Right Reserved
//// </copyright>
//// --------------------------------------------------------------------------------------------------------------------

//namespace k2.API.Code
//{
//    #region

//    using System.Net;
//    using System.Net.Http;
//    using System.Reflection;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using System.Web.Http;
//    using System.Web.Http.ExceptionHandling;

//    using Commons.Framework.Extensions;
//    using Commons.Framework.Logging;

//    #endregion

//    /// <summary>
//    /// The api global exception handler.
//    /// </summary>
//    public class ApiGlobalExceptionHandler : ExceptionHandler
//    {
//        /// <summary>
//        /// The handle.
//        /// </summary>
//        /// <param name="context">
//        /// The context.
//        /// </param>
//        public override void Handle(ExceptionHandlerContext context)
//        {
//            context.Result = new TextPlainErrorResult
//                                 {
//                                     Request = context.ExceptionContext.Request,
//                                     Content =
//                                         new ApiResponse
//                                             {
//                                                 ErrorMessage =
//                                                     $"UnExpected Api Error Occurred. {context.Exception.Message}",
//                                                 Success = false
//                                             }.ToJson()
//                                 };
//        }

//        /// <summary>
//        /// The text plain error result.
//        /// </summary>
//        private class TextPlainErrorResult : IHttpActionResult
//        {
//            /// <summary>
//            /// Gets or sets the content.
//            /// </summary>
//            public string Content { get; set; }

//            /// <summary>
//            /// Gets or sets the request.
//            /// </summary>
//            public HttpRequestMessage Request { get; set; }

//            /// <summary>
//            /// The execute async.
//            /// </summary>
//            /// <param name="cancellationToken">
//            /// The cancellation token.
//            /// </param>
//            /// <returns>
//            /// The <see cref="Task"/>.
//            /// </returns>
//            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//            {
//                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
//                response.Content = new StringContent(this.Content);
//                response.RequestMessage = this.Request;
//                return Task.FromResult(response);
//            }
//        }
//    }

//    /// <summary>
//    /// The api global exception logger.
//    /// </summary>
//    public class ApiGlobalExceptionLogger : ExceptionLogger
//    {
//        /// <summary>
//        /// The logger.
//        /// </summary>
//        private static readonly Logger Logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

//        /// <summary>
//        /// The log.
//        /// </summary>
//        /// <param name="context">
//        /// The context.
//        /// </param>
//        public override void Log(ExceptionLoggerContext context)
//        {
//            Logger.Error(context.Exception);
//        }


//    }
//}