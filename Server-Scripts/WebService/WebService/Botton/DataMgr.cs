using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using WebService.Logic;
using WebService.Middle;
namespace WebService.Botton
{
    class DataMgr
    {
        MySqlConnection sqlConn;
        public static DataMgr instance;
        public DataMgr()
        {

            instance = this;
            Connect();

        }
        private void Connect()
        {
            string connectStr = "server=127.0.0.1;port=3306;user=root;password=17071079;database=game";
            sqlConn = new MySqlConnection(connectStr);
            try
            {
                sqlConn.Open();
                WbeService.instance.WriteLine("数据库打开成功");
            }
            catch (Exception ex)
            {
                WbeService.instance.WriteLine(ex.Message);
                return;
            }

        }

        /// <summary>
        /// 防sql注入
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsSafeStr(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        private bool CanRegister(string id)
        {
            //防sql注入
            if (!IsSafeStr(id))
                return false;
            //查询id是否存在
            string cmdStr = string.Format("select * from userinfo where userid='{0}';", id);
            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                bool hasRows = dataReader.HasRows;
                dataReader.Close();
                return !hasRows;
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]CanRegister fail " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 注册 
        /// </summary>
        public bool Register(string id, string pw)
        {
            //防sql注入
            if (!IsSafeStr(id) || !IsSafeStr(pw))
            {
                WbeService.instance.WriteLine("[DataMgr]Register 使用非法字符");
                return false;
            }
            //能否注册
            if (!CanRegister(id))
            {
                WbeService.instance.WriteLine("[DataMgr]Register !CanRegister");
                return false;
            }
            //写入数据库User表
            string cmdStr = string.Format("insert into userinfo set userid ='{0}' ,password ='{1}';", id, pw);
            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]Register " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 创建PlayerData存入
        /// </summary>
        public bool CreatePlayer(string id)
        {
            //防sql注入
            if (!IsSafeStr(id))
                return false;
            //序列化
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            PlayerData playerData = new PlayerData();
            try
            {
                // playerData.name = "新手";

                formatter.Serialize(stream, playerData);
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]CreatePlayer 序列化 " + e.Message);
                return false;
            }
            byte[] byteArr = stream.ToArray();
            //写入数据库
            string cmdStr = string.Format("insert into player set id ='{0}' ,data =@data;", id);
            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            cmd.Parameters.Add("@data", MySqlDbType.Blob);
            cmd.Parameters[0].Value = byteArr;
            try
            {
                WbeService.instance.WriteLine("创建角色成功 ");
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]CreatePlayer 写入 " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        public PlayerData GetPlayerData(string id)
        {
            PlayerData playerData = null;
            //防sql注入
            if (!IsSafeStr(id))
                return playerData;
            //查询
            string cmdStr = string.Format("select * from player where id ='{0}';", id);
            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            byte[] buffer;
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    dataReader.Close();
                    return playerData;
                }
                dataReader.Read();

                long len = dataReader.GetBytes(1, 0, null, 0, 0);//1是data  
                buffer = new byte[len];
                dataReader.GetBytes(1, 0, buffer, 0, (int)len);
                dataReader.Close();
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]GetPlayerData 查询 " + e.Message);
                return playerData;
            }
            //反序列化
            MemoryStream stream = new MemoryStream(buffer);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                playerData = (PlayerData)formatter.Deserialize(stream);
                return playerData;
            }
            catch (SerializationException e)
            {
                WbeService.instance.WriteLine("[DataMgr]GetPlayerData 反序列化 " + e.Message);
                return playerData;
            }
        }

        public bool SavePlayer(Player player)
        {
            string id = player.id;
            PlayerData playerData = player.data;
            //序列化
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            try
            {
                formatter.Serialize(stream, playerData);
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]SavePlayer 序列化 " + e.Message);
                return false;
            }
            byte[] byteArr = stream.ToArray();
            //写入数据库
            string formatStr = "update player set data =@data where id = '{0}';";
            string cmdStr = string.Format(formatStr, player.id);
            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            cmd.Parameters.Add("@data", MySqlDbType.Blob);
            cmd.Parameters[0].Value = byteArr;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]CreatePlayer 写入 " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckPassword(string id, string pwd)
        {
            //防sql注入
            if (!IsSafeStr(id) || !IsSafeStr(pwd))
                return false;
            //查询
            string cmdStr = string.Format("select * from userinfo where userid='{0}' and password='{1}';", id, pwd);
            WbeService.instance.WriteLine("id="+ id+" pwd=" + pwd);

            MySqlCommand cmd = new MySqlCommand(cmdStr, sqlConn);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                bool hasRows = dataReader.HasRows;
                dataReader.Close();
                return hasRows;

            }
            catch (Exception e)
            {
                WbeService.instance.WriteLine("[DataMgr]CheckPassWord " + e.Message);
                return false;
            }
        }


    }
}
