using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIU.Tangtu.Web.App_Code
{
    /// <summary>
    /// 返回
    /// </summary>
    public class Result
    {

        public ResultStatus Code { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result OK(object data = null, string message = null)
        {
            return new Result() { Code = ResultStatus.OK, Data = data, Message = message };
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Result Fail(string message = null, ResultStatus code = ResultStatus.Fail, object data = null)
        {
            return new Result() { Code = code, Data = data, Message = message };
        }


        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<Result> OKAsync(object data = null, string message = null)
        {
            return await Task<Result>.Run(() =>
            {
                return OK(data, message);
            });
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<Result> FailAsync(string message = null, ResultStatus code = ResultStatus.Fail, object data = null)
        {
            return await Task<Result>.Run(() =>
            {
                return Fail(message, code, data);
            });
        }

    }

    /// <summary>
    /// 返回状态结果
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = -1,
        /// <summary>
        /// 系统内部错误
        /// </summary>
        SysError = 500,

        /// <summary>
        /// 验证权限失败
        /// </summary>
        ValidateAuthorityFail = 401
    }
}
