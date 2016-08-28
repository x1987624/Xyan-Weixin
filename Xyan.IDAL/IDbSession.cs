using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Xyan.IDAL
{
    public partial interface IDbSession
    {
        /// <summary>
        /// 将当前应用程序跟数据库的会话内所有实体的变化更新会数据库
        /// </summary>
        /// <returns>返回一个int类型的执行成功与否</returns>
        int SaveChanges();

        /// <summary>
        /// 执行Sql语句的方法  EF5.0的写法
        /// EF4.0的写法   int ExcuteSql(string strSql, ObjectParameter[] parameters);
        /// </summary>
        /// <param name="strSql">执行的SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回int类型</returns>
        int ExcuteSql(string strSql, DbParameter[] parameters);
    }
}
